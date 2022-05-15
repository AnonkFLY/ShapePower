using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneManager
{
    public SceneData[] scenesData;
    public void SetUnlocked(int index)
    {
        scenesData[index].IsUnlocked = true;
    }
    public void LoadAsync(int index,Action<GameObject> callBack)
    {

    }
}
