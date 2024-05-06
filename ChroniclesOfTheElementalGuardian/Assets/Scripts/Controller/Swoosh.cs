using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

public class Swoosh : MonoBehaviour
{
    void OnHit()
    {
        IObjectPool.Release(transform);
    }
}
