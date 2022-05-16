using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private float force = 1;
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.rigidbody?.AddForce(other.relativeVelocity.normalized * force, ForceMode2D.Impulse);
        //if(other)
    }
}
