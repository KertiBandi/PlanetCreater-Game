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
    [SerializeField] GravityForceField gravityMultiplier;
    [SerializeField] Collider2D[] objects;
    [SerializeField] MapSize edge;
    [SerializeField] public bool inGravity;   //gravit�ci�s k�tban van-e?
    float gravityForce = 1f;

    [SerializeField] PlanetCounter PlanetCounter;



    private void OnValidate()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        PlanetCounter = FindObjectOfType<PlanetCounter>();
        edge = FindObjectOfType<MapSize>();

    }

    private void Start()
    {
        self = gameObject.transform;
        target = self;
        rigidbody.AddTorque(Random.Range(-20, 20));//forg�s hozz�ad�sa

    }



    private void FixedUpdate()
    {
        if (PlanetCounter != null && PlanetCounter.victory == true)
        {
            transform.position = transform.position;
            rigidbody.Sleep();
            return;
        }

        Collider2D gravityChecker = Physics2D.OverlapCircle(transform.position, 1, gravityLayer);
        if (gravityChecker != null)  // bel�p�s a gravit�ci�s mez�be
        {
            target = gravityChecker.transform;
            inGravity = true;
            gravityMultiplier = gravityChecker.GetComponent<GravityForceField>();
            PlanetCounter.CountPlanets();
        }
        else  // kil�p�s a gravit�cvi�s mez�b�l
        {
            target = self;
            gravityMultiplier = null;
            inGravity = false;
            PlanetCounter.CountPlanets();
        }


        //------------------------------------------------------------------------------�j gravit�ci�s k�t l�trehoz�sa
        float topX = (edge.mapSize.x * 5) - 20;
        float topY = (edge.mapSize.y * 5) - 20;

        if (!inGravity)
        {
            objects = Physics2D.OverlapCircleAll(transform.position, selfGravityRange, asteroidLayer);
            int numberOfAsteroids = objects.Length;
            if (numberOfAsteroids > 1)
            {
                Vector3 center;  //kisz�molja az �j gravit�ci�s k�t k�z�ppontj�t
                Vector3 sumOfPosition = new Vector3(0, 0, 0);
                for (int i = 0; i < numberOfAsteroids; i++)
                {
                    sumOfPosition += objects[i].transform.position;

                }
                center = sumOfPosition / numberOfAsteroids;

                bool notTooCloseToOtherCenter = true;

                foreach (var obj in FindObjectsOfType<GravityForceField>())
                {
                    if (obj != null)
                    {
                        float distance2 = (obj.transform.position - center).magnitude;
                        if (distance2 < 15)
                        {
                            notTooCloseToOtherCenter = false;
                            return;
                        }
                    }

                }
                bool tooCloseToEdge = false;
                if (center.x > topX)
                {
                    tooCloseToEdge = true;
                }
                if (center.x < -topX)
                {
                    tooCloseToEdge = true;
                }
                if (center.y > topY)
                {
                    tooCloseToEdge = true;
                }
                if (center.y < -topY)
                {
                    tooCloseToEdge = true;
                }

                if (notTooCloseToOtherCenter && !tooCloseToEdge)
                {
                    GameObject newPlanet = Instantiate(newPlanetCenter);
                    newPlanet.transform.position = center;

                }


            }

        }

        //------------------------------------------------ mozg�s
        Vector3 targetPosition = target.position;
        Vector3 selfPosition = transform.position;
        Vector3 direction = (targetPosition - selfPosition).normalized;
        float distance = (targetPosition - selfPosition).magnitude;


        if (target != self && gravityMultiplier != null && target != null && distance > 0.05f)
        {

            rigidbody.AddForce(direction / distance * (gravityForce * gravityMultiplier.gravity));
        }

        //------------------------------------------------- teleport�l�s a p�lya sz�l�n




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
