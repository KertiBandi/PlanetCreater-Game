using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScript : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] bool isDead;

    private void OnValidate()
    {
        timer = FindObjectOfType<Timer>();
    }
    private void FixedUpdate()
    {
        if (timer != null)
        {
            isDead = timer.GetComponent<Timer>().dead;
            if(isDead)
            {
                Destroy(gameObject);
            }
        }
    }
    

}
