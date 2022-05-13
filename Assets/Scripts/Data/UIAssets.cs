using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "UIAssets", menuName = "UI/AssetsList")]
public class UIAssets : ScriptableObject
{
    private Dictionary<UIType, GameObject> _uiAssets = new Dictionary<UIType, GameObject>();
    [SerializeField]
    private GameObject[] _uiList;
    private void OnEnable()
    {
        if (_uiList == null)
            return;
        foreach (var item in _uiList)
        {
            _uiAssets.Add(item.GetComponent<UIBase>().type, item);
        }
    }
    public GameObject GetUIObj(UIType uIType)
    {
        if (!_uiAssets.TryGetValue(uIType, out var getValue))
        {
            DebugLog.Error($"No found {uIType}");
            return null;
        }
        return getValue;
    }
}
