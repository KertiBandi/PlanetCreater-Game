using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] Transform effect;

    private void Start()
    {
        effect = transform;
        effect.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroyCorutine());
    }

    IEnumerator DestroyCorutine()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    


}
