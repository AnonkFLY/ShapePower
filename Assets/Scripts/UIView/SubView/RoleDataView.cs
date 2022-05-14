using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleDataView : MonoBehaviour
{
    [SerializeField] private TMP_Text roleAttribute;
    [SerializeField] private TMP_Text levelupText;
    [SerializeField] private Button levelupButton;
    private ShapePowerArchive archive;
    private RoleBase data;
    public Action onLevel;
    private void Awake()
    {
        levelupButton.onClick.AddListener(() =>
        {
            DebugLog.Message("LevelUp");
            if (data.level > 5)
                return;
            DebugLog.Message("LevelUp");
            onLevel?.Invoke();
        });
    }
    public void UpdateView(RoleBase data)
    {
        this.data = data;
        if (data.level < 5)
            levelupText.text = "LevelUp-" + data.levelupPay;
        else
            levelupText.text = "<color=red>LevelUp-max</color>";
        roleAttribute.text = $"Level: {data.level}\nHealth: {data.health}\nArmor: {data.armor}\n";
    }
}
