using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeValue;
    [SerializeField] TMP_Text timerText;
    public bool dead = false;
    ClickExplosion[] ClickExplosion;
    PlanetCounter[] PlanetCounter;
    [SerializeField] GameObject objectToTurnOnWhenDie;
    bool victory = false;

    private void OnValidate()
    {
        ClickExplosion = FindObjectsOfType<ClickExplosion>();
        PlanetCounter = FindObjectsOfType<PlanetCounter>();
    }


    private void Update()
    {
        if (PlanetCounter != null)
        {
            victory = PlanetCounter[0].GetComponent<PlanetCounter>().victory;
        }
        //-------------------------------------------------------------  visszaszámláló
        if (timeValue > 0 && !victory)
        {
            timeValue -= Time.deltaTime;
        }
        else if(!victory)
        {
            timeValue = 0;
        }
        float minutes = Mathf.FloorToInt(timeValue / 60);
        float seconds = Mathf.FloorToInt(timeValue % 60);
        if (seconds > 9)
        {
            timerText.text = minutes + ":" + seconds;
        }
        else
        {
            timerText.text = minutes + ":0" + seconds;
        }

        if (timeValue < 10)
        {
            timerText.color = Color.red;
        }
        //---------------------------------------------------- ha elfogyott a munició, csökken az idõ

        if (ClickExplosion != null && timeValue > 10 && ClickExplosion[0].GetComponent<ClickExplosion>().ammo == 0)
        {
            timeValue = 10;
        }
        //------------------------------------------------------------ vesztési feltételek

        if (timeValue <= 0 && !victory)
        {
            dead = true;
            Death();
        }

        void Death()
        {

            objectToTurnOnWhenDie?.SetActive(true);

        }


    }

}
