using UnityEngine;

public class Splash : MonoBehaviour, IPooledObject
{
    [SerializeField] private Vector3 offset;

    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void OnObjectSpawn()
    {
        ps.Clear();
        ps.Play();
        transform.position += offset;
    }
}
