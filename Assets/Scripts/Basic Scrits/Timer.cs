using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeValue;
    [SerializeField] TMP_Text timerText;
    public bool dead = false;
    [SerializeField] ClickExplosion clickExplosion;
    [SerializeField] PlanetCounter planetCounter;
    [SerializeField] GameObject objectToTurnOnWhenDie;
    [SerializeField] AudioSource audioSource;
    bool victory = false;
    bool musicOn = false;

    private void OnValidate()
    {
        clickExplosion = FindObjectOfType<ClickExplosion>();
        planetCounter = FindObjectOfType<PlanetCounter>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {

        victory = planetCounter.GetComponent<PlanetCounter>().victory;

        //-------------------------------------------------------------  visszaszámláló
        if (timeValue > 0 && !victory)
        {
            timeValue -= Time.deltaTime;
        }
        else if (!victory)
        {
            timeValue = 0;
        }
        //----------------------------------------------------------------
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
        //-------------------------------------------------------------------
        if (timeValue < 10 && dead == false)
        {
            timerText.color = Color.red;
            if (!musicOn)
            {
                audioSource.Play();
                musicOn = true;
            }
        }
        //---------------------------------------------------------------
        if (victory)
        {
            audioSource.Stop();
        }

        //---------------------------------------------------- ha elfogyott a munició, csökken az idõ

        if (clickExplosion != null && timeValue > 10 && clickExplosion.GetComponent<ClickExplosion>().ammo == 0)
        {
            timeValue = 10;
        }
        //------------------------------------------------------------ vesztési feltételek

        if (timeValue <= 0 && !victory)
        {
            dead = true;
            Death();
            planetCounter.MusicStop();
            audioSource.Stop();
        }

        void Death()
        {

            objectToTurnOnWhenDie?.SetActive(true);

        }


    }

}
