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
        if (!other.transform.CompareTag("Player"))
            return;
        other.transform.GetComponent<IHurtable>()?.Hurt(damage);
        other.transform.GetComponent<PlayerController>().AddForce(_transform.up);
        Destroy(gameObject);
    }
}
