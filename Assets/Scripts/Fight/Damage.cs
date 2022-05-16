using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Damage
{
    public int damageValue;
    public OriginDamage originDamage;
}
public enum OriginDamage
{
    Player,
    Enemy,
    Scene
}
