using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiesOnColWithTag : MonoBehaviour
{
    [SerializeField] private string killerTag;
    [SerializeField] private string deathParticlePoolTag;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals(killerTag))
        {
            ObjectPooler.Instance.SpawnFromPool(deathParticlePoolTag, transform.position, 
                Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
