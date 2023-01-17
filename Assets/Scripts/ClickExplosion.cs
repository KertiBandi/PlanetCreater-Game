using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ClickExplosion : MonoBehaviour
{
    [SerializeField] Transform effect;
    [SerializeField] float ExplosionForce;
    [SerializeField] float explosionRange;
    [SerializeField] LayerMask layerToHit;
    [SerializeField] public int ammo;
    [SerializeField] TMP_Text ammoText;
    public bool dead = false;
    PlanetCounter planetCounter;
    GameManager gameManager;

    private void OnValidate()
    {
        planetCounter = FindObjectOfType<PlanetCounter>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {

        bool click = Input.GetMouseButtonDown(0);
        bool victory1 = planetCounter.GetComponent<PlanetCounter>().victory;
        bool isPaused1 = gameManager.GetComponent<GameManager>().isPaused;
        ammoText.text = "Ammo: " + ammo;

        if (click && ammo > 0 && !victory1 && !isPaused1)
        {
            ammo--;
         

            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRange, layerToHit);

            foreach (Collider2D obj in objects)
            {
                Vector3 direction = obj.transform.position - transform.position;
                obj.GetComponent<Rigidbody2D>().AddForce(direction * ExplosionForce);

            }
            effect.GetComponent<ParticleSystem>().Play();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);

    }


}

