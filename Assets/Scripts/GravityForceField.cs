using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForceField : MonoBehaviour
{

    [SerializeField, Min(1)] public float gravity;
    [SerializeField, Min(1)] public float sizeMultipler;
    [SerializeField] LayerMask asteroidLayer;
    [SerializeField] LayerMask gravityLayer;
    [SerializeField] public int numberOfAsteroids;
    Vector3 size;
    int randomNumber;
    [SerializeField] Collider2D[] objects;
    int currentNumberOfAsteroids = 0;


    private void Start()
    {
        randomNumber = Random.Range(1, 1000);
        size = new Vector3(1, 1, 0);
    }

    private void Update()
    {
        transform.localScale = size * sizeMultipler;

        //---------------------------------------------dupl�k t�rl�se

        foreach (var obj in FindObjectsOfType<GravityForceField>())
        {

            if ((this.transform.position == obj.transform.position) && this.randomNumber < obj.randomNumber)
            {
                Destroy(gameObject);
            }
        }
        //------------------------------------------------ kissebb gravit�ci�s mez�k elnyel�se
        Collider2D[] tooSmallGravityField = Physics2D.OverlapCircleAll(transform.position, 1, gravityLayer);
        int gr = tooSmallGravityField.Length;
        if (gr > 1)
        {
            Destroy(gameObject);
        }


        //------------------------------------------------ v�ltoz�k l�trehoz�sa

        Vector3 sumOfPositions = new Vector3(0, 0, 0);
        Vector3 center;
        objects = Physics2D.OverlapCircleAll(transform.position, sizeMultipler / 2, asteroidLayer);
        numberOfAsteroids = objects.Length;

        //--------------------------------- az �res gravit�ci�s mez�k t�rl�se
        if (numberOfAsteroids < 2)
        {
            Destroy(gameObject);
        }
        //----------------------- mozg�s
        if (numberOfAsteroids > 1)
        {
            for (int i = 0; i < numberOfAsteroids; i++)
            {
                sumOfPositions += objects[i].transform.position;

            }

            center = sumOfPositions / numberOfAsteroids;
            transform.position = center;
        }

        //------------------------------------------------- m�ret �s gravit�ci�s er� n�vel�se/cs�kkent�se
        if (currentNumberOfAsteroids < numberOfAsteroids)
        {
            currentNumberOfAsteroids++;
            sizeMultipler++;
            gravity += 0.3f;
        }
        else if (currentNumberOfAsteroids > numberOfAsteroids)
        {
            currentNumberOfAsteroids--;
            sizeMultipler--;
            gravity -= 0.3f; ;
        }
        else
        {

        }



    }








}
