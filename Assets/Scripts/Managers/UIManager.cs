using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private List<UIBase> _uiLiist;
    private UIAssets _uiAssets;
    private Transform _canvesTrans;
    private Dictionary<UIType, UIBase> _instanceUI;

    public UIManager(UIAssets uIAssets)
    {
        _uiAssets = uIAssets;
        _canvesTrans = GameObject.FindObjectOfType<Canvas>().transform;
    }
    public void OpenUI(UIType type,bool closeLast)
    {
        var panel = GetUI(type);
        if(closeLast)
            CloseUI();
        panel.Open();
        _uiLiist.Add(panel);
    }
    public void CloseUI()
    {
        CloseUI(_uiLiist.Count - 1);
    }
    public void CloseUI(int index)
    {
        _uiLiist[index].Close();
        _uiLiist.RemoveAt(index);
    }
    private UIBase GetUI(UIType type)
    {
        UIBase panel;
        if (!_instanceUI.TryGetValue(type, out panel))
        {
            panel = InstanceUIPanel(type);
            _instanceUI.Add(type,panel);
        }
        return panel;
    }
    private T GetUI<T>(UIType type) where T : UIBase
    {
        _instanceUI.TryGetValue(type, out var panel);
        return panel as T;
    }
    private UIBase InstanceUIPanel(UIType uIType)
    {
        //总之就是获取GameObject实例化然后获取UIBase组件
        var result = GameObject.Instantiate(_uiAssets.GetUIObj(uIType), _canvesTrans).GetComponent<UIBase>();
        result.RegisterUI(this);
        return result;
    }
}
public enum UIType
{
    StartGamePanel,
    LevelScene
}