using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : Bullet
{
    public override void BreakBullet(Collision2D other)
    {
        Destroy(gameObject);
    }
}
