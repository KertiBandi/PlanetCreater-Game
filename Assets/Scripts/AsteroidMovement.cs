using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] new Rigidbody2D rigidbody;
    [SerializeField] float selfGravityRange;
    [SerializeField] LayerMask asteroidLayer;
    [SerializeField] LayerMask gravityLayer;
    [SerializeField] GameObject newPlanetCenter;
    Transform self;
    GravityForceField gravityMultiplier;
    [SerializeField] Collider2D[] objects;

    [SerializeField] public bool inGravity;   //gravitációs kútban van-e?
    float gravityForce = 1f;
    Vector3 sumOfPosition = new Vector3(0, 0, 0);
    PlanetCounter[] PlanetCounter;



    private void OnValidate()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        PlanetCounter = FindObjectsOfType<PlanetCounter>(); 
        
    }

    private void Start()
    {
        self = gameObject.transform;
        target = self;
        rigidbody.AddTorque(Random.Range(-20, 20));//forgás hozzáadása

    }



    private void FixedUpdate()
    {
        if(PlanetCounter != null && PlanetCounter[0].GetComponent<PlanetCounter>().winner == true)
        {
            transform.position = transform.position;
            rigidbody.Sleep();
            return;
        }
 
        Collider2D gravityChecker = Physics2D.OverlapCircle(transform.position, 1, gravityLayer);
        if (gravityChecker != null)  // belépés a gravitációs mezõbe
        {
            target = gravityChecker.transform;
            inGravity = true;
            gravityMultiplier = gravityChecker.GetComponent<GravityForceField>();
        }
        else  // kilépés a gravitácviós mezõbõl
        {
            target = self;
            gravityMultiplier = null;
            inGravity = false;
        }


        //------------------------------------------------------------------------------új gravitációs kút létrehozása

        if (!inGravity)
        {
            objects = Physics2D.OverlapCircleAll(transform.position, selfGravityRange, asteroidLayer);
            int numberOfAsteroids = objects.Length;
            if (numberOfAsteroids > 1)
            {
                Vector3 center;  //kiszámolja az új gravitációs kút középpontját

                for (int i = 0; i < numberOfAsteroids; i++)
                {
                    sumOfPosition += objects[i].transform.position;

                }
                center = sumOfPosition / numberOfAsteroids;

                bool notTooClose = true;
                float distance2;
                foreach (var obj in FindObjectsOfType<GravityForceField>())
                {
                    if (obj != null)
                    {
                        distance2 = (center - obj.transform.position).magnitude;
                        if (distance2 < 15)
                        {
                            notTooClose = false;
                        }
                    }
                }

                if (notTooClose)
                {
                    GameObject newPlanet = Instantiate(newPlanetCenter);
                    newPlanet.transform.position = center;
                }


            }

        }

        //------------------------------------------------ mozgás
        Vector3 targetPosition = target.position;
        Vector3 selfPosition = transform.position;
        Vector3 direction = (targetPosition - selfPosition).normalized;
        float distance = (targetPosition - selfPosition).magnitude;


        if (target != self && gravityMultiplier != null && target != null && distance > 0.05f)
        {

            rigidbody.AddForce(direction / distance * (gravityForce * gravityMultiplier.gravity));
        }

        //------------------------------------------------- teleportálás a pálya szélén



        MapSize edge = FindObjectOfType<MapSize>();
        float xPosition = selfPosition.x;
        float yPosition = selfPosition.y;
        float zPosition = selfPosition.z;
        if (xPosition > (edge.mapSize.x * 5))
        {
            transform.position = new Vector3(-xPosition + 5, yPosition, zPosition);
        }
        if (xPosition < (-edge.mapSize.x * 5))
        {
            transform.position = new Vector3(-xPosition - 5, yPosition, zPosition);
        }
        if (yPosition > (edge.mapSize.y * 5))
        {
            transform.position = new Vector3(xPosition, -yPosition + 5, zPosition);
        }
        if (yPosition < (-edge.mapSize.y * 5))
        {
            transform.position = new Vector3(xPosition, -yPosition - 5, zPosition);
        }



    }





    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, selfGravityRange);
    }


}
