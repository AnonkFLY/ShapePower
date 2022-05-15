using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePowerSave
{
    private const int roleCount = 4;
    public int levelCount = 5;
    public int buyRole = 1;
    public int levelLocked = 1;
    public int money = 0;
    public int choose = 0;
    public RoleBase[] roles = new RoleBase[roleCount];
    public bool GetRoleIsPurchased(int i)
    {
        //DebugLog.Message($"尝试获取角色{i}是否购买--{buyRole}");
        return GetBool(buyRole, i);
    }
    public void SetRoleIsPurchased(int i)
    {
        //DebugLog.Message($"设置角色被购买{i}--{buyRole}");
        buyRole = SetBool(ref buyRole, i);
    }
    public bool GetLevelLocked(int i)
    {
        return GetBool(levelLocked, i);
    }
    public void SetLevelUnlocked(int i)
    {
        levelLocked = SetBool(ref levelLocked, i);
    }
    private bool GetBool(int value, int index)
    {
        return ((value >> index) % 2) == 1;
    }
    private int SetBool(ref int value, int index)
    {
        return (value | (1 << index));
    }
    public bool LevelUpRole(int index)
    {
        if (roles[index].level >= 5)
            return false;
        if (roles[index].levelupPay <= money)
        {
            money -= roles[index].levelupPay;
            ++roles[index].level;
            var level = roles[index].level;
            roles[index].armor += 2 * level;
            roles[index].health += 10 * level;
            roles[index].levelupPay *= 2;
            return true;
        }
        return false;
    }
}
