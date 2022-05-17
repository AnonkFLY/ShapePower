using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBullet : Bullet
{

    public override void BreakBullet(Collision2D collision, Collider2D trigger)
    {
        Destroy(gameObject);
    }
}
