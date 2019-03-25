using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{
    /// <summary>
    /// Returns a LinkedList of all the children of a given object
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static LinkedList<GameObject> GetChildrenOf(GameObject obj)
    {
        LinkedList<GameObject> list = new LinkedList<GameObject>();

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            list.AddLast(obj.transform.GetChild(i).gameObject);
        }

        return list;
    }
    
    /// <summary>
    /// Returns true if the velocity^2 of the object is smaller than epsilon
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    public static bool IsImmobile(GameObject obj, float epsilon)
    {
        Rigidbody objrb = obj.GetComponent<Rigidbody>();
        
        if (objrb.velocity.sqrMagnitude > epsilon)
        {
            return false;
        }
        else if (objrb.angularVelocity.sqrMagnitude > epsilon)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Given an array of objects, returns the object with name name
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject SearchObjectArray(GameObject[] arr, string name)
    {
        foreach (GameObject cur in arr)
        {
            if (cur.name.Equals(name))
            {
                return cur;
            }
        }

        return null;
    }
}
