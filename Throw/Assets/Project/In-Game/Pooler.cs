using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public int PoolSize { private get; set; }

    [SerializeField] private Pooled ObjectScript;

    private Pooled[] Pool;

	// Use this for initialization
	void Start()
    {
        Pool = new Pooled[PoolSize];

		for (int i = 0; i < Pool.Length; i++)
        {
            GameObject newObject = ObjectScript.Instantiate();
            newObject.SetActive(false);
            Pool[i] = newObject.GetComponent<Pooled>();
        }
	}
	
	public Pooled GetObject()
    {
        for (int i = 0; i < Pool.Length; i++)
        {
            GameObject curGameObject = Pool[i].gameObject;

            if (!curGameObject.activeInHierarchy)
            {
                curGameObject.SetActive(true);
                return Pool[i];
            }
        }

        return null;
    }
}
