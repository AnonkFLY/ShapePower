using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VirusEnemy : Enemy
{
    [SerializeField]
    private Damage damage;
    public override void Behavior()
    {
        MoveToPlayer();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.GetComponent<IHurtable>()?.Hurt(damage);
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerController>().AddForce(_transform.up);
            Destroy(gameObject);
        }
    }
}
