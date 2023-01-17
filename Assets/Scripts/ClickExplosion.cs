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
    PlanetCounter[] planetCounter;

    private void OnValidate()
    {
        planetCounter = FindObjectsOfType<PlanetCounter>();
    }

    private void Update()
    {

        bool click = Input.GetMouseButtonDown(0);
        bool victory = planetCounter[0].GetComponent<PlanetCounter>().winner;


        if (click && ammo > 0 && planetCounter != null && !victory)
        {
            ammo--;
            ammoText.text = "Ammo: " + ammo;

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

