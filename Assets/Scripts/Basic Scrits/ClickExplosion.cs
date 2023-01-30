using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

enum BombType { explosion, implosion, stop }

public class ClickExplosion : MonoBehaviour
{
    [SerializeField] Transform effect;
    [SerializeField] float explosionForce;
    [SerializeField] float explosionRange;
    [SerializeField] LayerMask layerToHit;
    [SerializeField] public int ammo;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] TMP_Text bombTypeText;
    [SerializeField] PlanetCounter planetCounter;
    [SerializeField] GameManager gameManager;
    [SerializeField] BombType bombType;
    [SerializeField] GameObject explosionGameObject;
    [SerializeField] GameObject implosionGameObject;
    [SerializeField] GameObject stopGameObject;

    private void OnValidate()
    {
        planetCounter = FindObjectOfType<PlanetCounter>();
        gameManager = FindObjectOfType<GameManager>();

    }

    private void Start()
    {
        bombType = BombType.explosion;
    }


    private void Update()
    {

        bool click = Input.GetMouseButtonDown(0);
        bool victory1 = planetCounter.GetComponent<PlanetCounter>().victory;
        bool isPaused1 = gameManager.GetComponent<GameManager>().isPaused;
        ammoText.text = "Ammo: " + ammo;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ExplosionButton();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ImplosionButton();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StopButton();
        }

        if (click && ammo > 0 && !victory1 && !isPaused1)
        {
            ammo--;
            Vector3 selfPosition;
            selfPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selfPosition.z = 10;
            transform.position = selfPosition;

            if (bombType == BombType.explosion)
            {
                Explosion();
                GameObject newExplosion = Instantiate(explosionGameObject);
                newExplosion.transform.position = selfPosition;
            }
            if (bombType == BombType.implosion)
            {
                Implosion();
                GameObject newImplosion = Instantiate(implosionGameObject);
                newImplosion.transform.position = selfPosition;
            }
            if (bombType == BombType.stop)
            {
                Stop();
                GameObject newStop = Instantiate(stopGameObject);
                newStop.transform.position = selfPosition;
            }

        }

    }

    void Explosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRange, layerToHit);

        foreach (Collider2D obj in objects)
        {
            Vector3 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);

        }

    }
    void Implosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRange+2, layerToHit);

        foreach (Collider2D obj in objects)
        {
            Vector3 direction = transform.position - obj.transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);

        }
    }
    void Stop()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRange, layerToHit);

        foreach (Collider2D obj in objects)
        {
            obj.GetComponent<Rigidbody2D>().velocity *= 0;

        }


    }
    public void ExplosionButton()
    {
        bombType = BombType.explosion;
        bombTypeText.text = "Bomb type: Explosion";


    }

    public void ImplosionButton()
    {
        bombType = BombType.implosion;
        bombTypeText.text = "Bomb type: Implosion";
    }

    public void StopButton()
    {
        bombType = BombType.stop;
        bombTypeText.text = "Bomb type: Zero Bomb";
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);

    }


}

