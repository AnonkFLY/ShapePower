using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
    [SerializeField]
    private float timer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
    }

}
