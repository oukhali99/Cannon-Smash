using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [SerializeField] private List<Pool> pools;

    private Dictionary<string, Queue<PooledObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<PooledObject>>();

        foreach (Pool curPool in pools)
        {
            Queue<PooledObject> objectPool = new Queue<PooledObject>();

            for (int i = 0; i < curPool.size; i++)
            {
                GameObject obj = Instantiate(curPool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(new PooledObject(obj));
            }

            poolDictionary.Add(curPool.tag, objectPool);
        }

        Instance = this;
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " dosen't exist");
            return null;
        }

        PooledObject pooledObjectToSpawn = poolDictionary[tag].Dequeue();
        GameObject objectToSpawn = pooledObjectToSpawn.obj;
        IPooledObject pooledObjectScript = pooledObjectToSpawn.objScript;

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        pooledObjectScript.OnObjectSpawn();

        poolDictionary[tag].Enqueue(pooledObjectToSpawn);

        return objectToSpawn;
    }
    
    [System.Serializable] public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    private class PooledObject
    {
        public GameObject obj { get; set; }
        public IPooledObject objScript { get; set; }

        public PooledObject(GameObject obj)
        {
            this.obj = obj;
            this.objScript = obj.GetComponent<IPooledObject>();
        }
    }
}
