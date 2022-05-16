using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDistance : MonoBehaviour
{
    [Range(1,10)]
    [SerializeField]
    private float distance = 3;
    void Start()
    {
        transform.GetChild(0).transform.position = Vector3.up * distance;
    }

}
