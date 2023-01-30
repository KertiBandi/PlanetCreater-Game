using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{

    [SerializeField] float explosionRange;
    [SerializeField] float explosionForce;
    [SerializeField] LayerMask LayerToHit;
    [SerializeField] Vector3 position;
    [SerializeField] GameObject Explosion;


    private void Update()
    {
        position = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(position, explosionRange, LayerToHit);

        foreach (Collider2D obj in objects)
        {
            Vector3 direction = obj.transform.position - position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);

        }
        GameObject newExplosion = Instantiate(Explosion);
        newExplosion.transform.position = position;





        Destroy(gameObject);

    }


}
