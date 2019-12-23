using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pooled : MonoBehaviour
{
    public abstract GameObject Instantiate();
}
