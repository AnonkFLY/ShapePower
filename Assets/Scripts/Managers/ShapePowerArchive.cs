using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapePowerArchive : ArchiveManager<ShapePowerSave>
{
    public Action<int> onMoneyChange;
    public void SetChoose(int choose)
    {
        archiveObj.choose = choose;
        Saved();
    }
    public bool BuyRole(int index, int payValue)
    {
        DebugLog.Message(payValue);
        if (archiveObj.money >= payValue)
        {
            archiveObj.SetRoleIsPurchased(index);
            archiveObj.money -= payValue;
            MoneyChange();
            Saved();
            return true;
        }
        return false;
    }

    private void MoneyChange()
    {
        onMoneyChange?.Invoke(archiveObj.money);
    }

    public void AddMoney(int value)
    {
        archiveObj.money += value;
        MoneyChange();
        Saved();
    }
    public bool UpdateRoleLevelUp(int index)
    {
        var result = archiveObj.LevelUpRole(index);
        if (result)
        {
            MoneyChange();
            Saved();
        }
        return result;
    }
    public int UnLockNextLevel()
    {
        for (int i = 0; i < archiveObj.levelCount; i++)
        {
            if (!archiveObj.GetLevelLocked(i))
            {
                return i;
            }
        }
        return -1;
    }
    public void UnLockLevel(int i)
    {
        archiveObj.SetLevelUnlocked(i);
        Saved();
    }

    public void InitRoleData(RoleBase[] roleChoices)
    {
        if (archiveObj.roles == null)
            archiveObj.roles = roleChoices;
        Saved();
    }
}
