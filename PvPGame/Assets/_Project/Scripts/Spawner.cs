using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private string objectTag;
    /// <summary>
    /// Probability to spawn an object each second
    /// </summary>
    [SerializeField][Range(0 ,1)] private float probability;

    private float timer;
    
    private ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0;
            float hit = Random.Range(0f, 1f);
            if (hit <= probability)
            {
                objectPooler.SpawnFromPool(objectTag, transform.position, transform.rotation);
            }
        }
    }


}
