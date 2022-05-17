using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RectangleBullet : Bullet
{
    public override void BreakBullet(Collision2D collision, Collider2D trigger)
    {
        Destroy(gameObject);
    }
}
