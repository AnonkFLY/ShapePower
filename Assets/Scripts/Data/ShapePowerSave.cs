using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePowerSave
{
    private readonly int roleCount = 4;
    public byte buyRole = 1;
    public int money = 0;
    public int choose = 0;
    public bool GetRoleIsPurchased(int i)
    {
        //DebugLog.Message($"尝试获取角色{i}是否购买--{buyRole}");
        return GetBool(buyRole, i);
    }
    public void SetRoleIsPurchased(int i)
    {
        //DebugLog.Message($"设置角色被购买{i}--{buyRole}");
        buyRole = SetBool(buyRole, i);
    }
    private bool GetBool(byte value, int index)
    {
        return ((buyRole >> index) % 2) == 1;
    }
    private byte SetBool(byte value, int index)
    {
        return (byte)(buyRole | (1 << index));
    }
}
