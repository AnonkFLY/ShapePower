using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

[System.Serializable]
public class SceneManager
{
    public SceneData[] scenesData;
    public void SetUnlocked(int index)
    {
        scenesData[index].IsUnlocked = true;
    }
    public void LoadAsync(string name,Action<GameObject> callBack)
    {
        GameManager.Instance.StartCoroutine(LoadScene(Path.Combine("Scenes",name),callBack));
    }
    private IEnumerator LoadScene(string path,Action<GameObject> callBack)
    {
        var request = Resources.LoadAsync(path,typeof(GameObject));
        yield return request;
        yield return new WaitForSeconds(1.1f);
        var obj = request.asset as GameObject;
        obj = GameObject.Instantiate(obj);
        callBack?.Invoke(obj);
    }
    
}
