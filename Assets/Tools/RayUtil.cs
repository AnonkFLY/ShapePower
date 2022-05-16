using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RayUtil
{
    public static Transform FindObjOfLate(Vector3 position, float maxRadius, LayerMask layerMask, int layerCount = 3)
    {
        for (int i = 1; i < layerCount + 1; i++)
        {
            var hitObj = Physics2D.CircleCast(position, (maxRadius * i) / layerCount, Vector2.zero, 0, layerMask);
            if (hitObj.collider != null)
                return hitObj.transform;
        }
        return null;
    }
}
