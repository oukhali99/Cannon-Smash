using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBall : Ball
{
    [SerializeField] private float ExplosionMagnitude;
    [SerializeField] private float ExplosionRadius;
    [SerializeField] private float ExplosionUpForce;
    [SerializeField] private AudioSource ExplosionAudio;

    private bool exploded;

    void Awake()
    {
        exploded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Destructible") && !exploded)
        {
            Explode();
        }
    }


    // Helpers
    private void Explode()
    {
        exploded = true;

        Vector3 explosionPosition = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(explosionPosition, ExplosionRadius);

        ExplosionAudio.Play();

        foreach (Collider collider in hitColliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(ExplosionMagnitude, explosionPosition, ExplosionRadius, ExplosionUpForce, ForceMode.Impulse);
            }
        }
    }
}
