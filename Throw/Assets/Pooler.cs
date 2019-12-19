using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public GameObject Object;
    public GameObject[] Pool;

	// Use this for initialization
	void Awake ()
    {
        Pool = new GameObject[Pool.Length];

		for (int i = 0; i < Pool.Length; i++)
        {
            GameObject newObject = Instantiate(Object);
            newObject.SetActive(false);
            Pool[i] = newObject;
        }
	}
	
	public GameObject GetObject()
    {
        for (int i = 0; i < Pool.Length; i++)
        {
            if (!Pool[i].activeInHierarchy)
            {
                Pool[i].SetActive(true);
                return Pool[i];
            }
        }

        return null;
    }

    public int GetObjectIndex()
    {
        for (int i = 0; i < Pool.Length; i++)
        {
            if (!Pool[i].activeInHierarchy)
            {
                Pool[i].SetActive(true);
                return i;
            }
        }

        return -1;
    }
}
