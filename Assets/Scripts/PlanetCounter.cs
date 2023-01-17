using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetCounter : MonoBehaviour
{

    [SerializeField] List<int> asteroidCountList;
    [SerializeField] GravityForceField[] gravityForceField;
    [SerializeField] AsteroidMovement[] asteroidMovementList;
    [SerializeField] int inGravity = 0;
    int num = 0;
    int first;
    int second;
    int third;
    [SerializeField] TMP_Text firstText;
    [SerializeField] TMP_Text secondText;
    [SerializeField] TMP_Text thirdText;
    [SerializeField] GameObject ObjectToTurnOnWhenWin;
    [SerializeField] int winningCondition;
    [SerializeField] public bool winner;

    private void Start()
    {
        firstText.text = "";
        secondText.text = "";
        thirdText.text = "";

    }

    private void FixedUpdate()
    {
        gravityForceField = FindObjectsOfType<GravityForceField>();
        asteroidMovementList = FindObjectsOfType<AsteroidMovement>();

        //---------------------------------------------------------------- gravitációs mezõben lévõ aszteroidák megszámolása 
        for (int i = 0; i < asteroidMovementList.Length; i++)
        {
            if (asteroidMovementList[i].GetComponent<AsteroidMovement>().inGravity == true)
            {
                num++;
            }

        }
        //----------------------------------- az adott gravitációs mezõhöz tartozó aszteroidák megszámolása

        if (gravityForceField != null)
        {
            if (inGravity != num)
            {
                asteroidCountList.Clear();

                for (int i = 0; i < gravityForceField.Length; i++)
                {

                    asteroidCountList.Add(gravityForceField[i].GetComponent<GravityForceField>().numberOfAsteroids);
                }
                asteroidCountList.Sort();
                inGravity = num;
            }
        }

        //------------------------------------------------------ ranglista frissítése

        if (asteroidCountList.Count > 0)
        {
            first = asteroidCountList[asteroidCountList.Count - 1];
            firstText.text = "1st Planet size: " + first;
        }
        if (asteroidCountList.Count > 1)
        {
            second = asteroidCountList[asteroidCountList.Count - 2];
            secondText.text = "2nd Planet Size: " + second;
        }
        if (asteroidCountList.Count > 2)
        {
            third = asteroidCountList[asteroidCountList.Count - 3];
            thirdText.text = "3rd Planet Size: " + third;
        }

        if (asteroidCountList.Count < 1)
        {
            firstText.text = "";

        }
        if (asteroidCountList.Count < 2)
        {
            secondText.text = "";

        }
        if (asteroidCountList.Count < 3)
        {
            thirdText.text = "";
        }

        //-------------------------------------------------------------------------------

        if (first >= winningCondition)
        {
            ObjectToTurnOnWhenWin.SetActive(true);
            winner = true;
        }



        num = 0;
    }


}
