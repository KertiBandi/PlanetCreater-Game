using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackGround : MonoBehaviour
{
    [SerializeField] new Rigidbody2D rigidbody;

    private void OnValidate()
    {
        rigidbody = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        Vector2 randomVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rigidbody.AddTorque(Random.Range(-20, 20));
        rigidbody.AddForce(randomVector * Random.Range(40, 200));
    }
    private void Update()
    {
        Vector3 selfPosition = transform.position;
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
}
