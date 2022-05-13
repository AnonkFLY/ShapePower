using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class ColliderEvent : MonoBehaviour
{
    public event UnityAction<Collision2D> onEnter;
    public event UnityAction<Collision2D> onExit;
    private void OnCollisionEnter2D(Collision2D other)
    {
        onEnter?.Invoke(other);
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        onExit?.Invoke(other);
    }
}
