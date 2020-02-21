using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBall : Ball
{
    [SerializeField] private float ExplosionMagnitude;
    [SerializeField] private float ExplosionRadius;
    [SerializeField] private float ExplosionUpForce;
    [SerializeField] private AudioSource ExplosionAudio;
    [SerializeField] private ParticleSystem Particles;
    [SerializeField] private TraumaInducer MyTraumaInducer;

    private bool exploded;
    private float waitToDesactivate;
    private float desactivateTimestamp;

    void Awake()
    {
        exploded = false;
        waitToDesactivate = Mathf.Max(ExplosionAudio.clip.length, Particles.main.duration) - (float)1/5;
        desactivateTimestamp = 0;
    }

    void Update()
    {
         if (exploded && Time.time - desactivateTimestamp > waitToDesactivate)
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Destructible") && !exploded)
        {
            Explode();
        }
    }

    public override void Fired()
    {
    }

    // Helpers
    private void Explode()
    {
        exploded = true;
        desactivateTimestamp = Time.time;

        Vector3 explosionPosition = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(explosionPosition, ExplosionRadius);
        
        ExplosionAudio.Play();
        Particles.Play();
        MyTraumaInducer.Play();

        foreach (Collider collider in hitColliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(ExplosionMagnitude, explosionPosition, ExplosionRadius, ExplosionUpForce, ForceMode.Impulse);
            }
        }

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.isKinematic = true;
    }
}
