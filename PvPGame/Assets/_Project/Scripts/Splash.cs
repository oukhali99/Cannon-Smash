using UnityEngine;

public class Splash : MonoBehaviour, IPooledObject
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void OnObjectSpawn()
    {
        ps.Clear();
        ps.Play();
    }
}
