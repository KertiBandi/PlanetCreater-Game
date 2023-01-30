using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetCounter : MonoBehaviour
{

    [SerializeField] List<int> asteroidCountList;
    [SerializeField] GravityForceField[] gravityForceField;
    int first;
    int second;
    int third;
    [SerializeField] TMP_Text firstText;
    [SerializeField] TMP_Text secondText;
    [SerializeField] TMP_Text thirdText;
    [SerializeField] GameObject ObjectToTurnOnWhenWin;
    [SerializeField] int firstWinningCondition;
    [SerializeField] int secondWinningCondition;
    [SerializeField] int thirdWinningCondition;
    [SerializeField] public bool victory;
    [SerializeField] AudioSource audioSource;

    private void OnValidate()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    private void Start()
    {
        firstText.text = "";
        secondText.text = "";
        thirdText.text = "";

    }

    private void FixedUpdate()
    {
        gravityForceField = FindObjectsOfType<GravityForceField>();


        //-------------------------------------------------------------------------------

        if (first >= firstWinningCondition && second >= secondWinningCondition && third >= thirdWinningCondition)
        {
            ObjectToTurnOnWhenWin.SetActive(true);
            victory = true;
            MusicStop();
        }



    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))//cheat
        {
            ObjectToTurnOnWhenWin.SetActive(true);
            victory = true;
            MusicStop();
        }
    }

    public void MusicStop()
    {
        audioSource.Stop();
    }

    public void CountPlanets()
    {

        //----------------------------------- az adott gravitációs mezõhöz tartozó aszteroidák megszámolása

        if (gravityForceField != null)
        {

            asteroidCountList.Clear();

            for (int i = 0; i < gravityForceField.Length; i++)
            {

                asteroidCountList.Add(gravityForceField[i].numberOfAsteroids);
            }
            asteroidCountList.Sort();


        }

        //------------------------------------------------------ ranglista frissítése

        if (asteroidCountList.Count > 0)
        {
            first = asteroidCountList[asteroidCountList.Count - 1];
            firstText.text = "1st Planet: " + first;
        }
        if (asteroidCountList.Count > 1)
        {
            second = asteroidCountList[asteroidCountList.Count - 2];
            secondText.text = "2nd Planet: " + second;
        }
        if (asteroidCountList.Count > 2)
        {
            third = asteroidCountList[asteroidCountList.Count - 3];
            thirdText.text = "3rd Planet: " + third;
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

    }


}
