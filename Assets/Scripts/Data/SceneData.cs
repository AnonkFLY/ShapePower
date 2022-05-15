using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public string sceneName;
    private bool isUnlocked;
    public Sprite sceneSprite;
    public Action onUnLock;
    public bool IsUnlocked { get => isUnlocked; set {
        isUnlocked = value;
        if(value)
        {
            onUnLock?.Invoke();
        }
    } }
}
