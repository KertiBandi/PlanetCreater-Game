using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovment : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float panBorderThickness = 10f;

    Camera camera1;



    private void Update()
    {
        Vector3 pos = transform.position;
        //------------------------------------------------------kamera mozgatása

        if (Input.GetKey(KeyCode.D) || (Input.mousePosition.x >= Screen.width - panBorderThickness && Input.mousePosition.x <= Screen.width))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || (Input.mousePosition.x <= panBorderThickness && Input.mousePosition.x >= 0))
        {
            pos.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) || (Input.mousePosition.y >= Screen.height - panBorderThickness && Input.mousePosition.y <= Screen.height))
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || (Input.mousePosition.y <= panBorderThickness && Input.mousePosition.y >= 0))
        {
            pos.y -= speed * Time.deltaTime;
        }

        //---------------------------------------------------- a pálya szélén megállítja       

        camera1 = Camera.main;
        MapSize edge = FindObjectOfType<MapSize>();
        float realMapSizeX = (edge.mapSize.x * 5) - 5;
        float realMapSizeY = (edge.mapSize.y * 5) - 5;

        Vector3 heightScreenPoint = new Vector3(0, Screen.height, 0);
        Vector3 widhtScreenPoint = new Vector3(Screen.width, 0, 0);


        float upperPoint = camera1.ScreenToWorldPoint(heightScreenPoint).y;
        float lowerPoint = camera1.ScreenToWorldPoint(widhtScreenPoint).y;
        float leftPoint = camera1.ScreenToWorldPoint(heightScreenPoint).x;
        float rightPoint = camera1.ScreenToWorldPoint(widhtScreenPoint).x;

        if (upperPoint > realMapSizeY)
        {
            pos.y = transform.position.y - 1;
        }
        if (lowerPoint < -realMapSizeY)
        {
            pos.y = transform.position.y + 1;
        }
        if (rightPoint > realMapSizeX)
        {
            pos.x = transform.position.x - 1;
        }
        if (leftPoint < -realMapSizeX)
        {
            pos.x = transform.position.x + 1;
        }


        //-----------------------------------------------------------------------------

        transform.position = pos;




    }
}
