using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePowerSave
{
    private const int roleCount = 4;
    public byte buyRole = 1;
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
    public bool LevelUpRole(int index)
    {
        if(roles[index].level>=5)
            return false;
        if(roles[index].levelupPay<=money)
        {
            money -= roles[index].levelupPay;
            ++roles[index].level;
            var level = roles[index].level;
            roles[index].armor+=2*level;
            roles[index].health+=10*level;
            roles[index].levelupPay *=2;
            return true;
        }
        return false;
    }
}
