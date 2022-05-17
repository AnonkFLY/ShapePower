using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

[System.Serializable]
public class SceneManager
{
    public SceneData[] scenesData;
    private int index = -1;
    public bool SetUnlocked(int index)
    {
        if (index - this.index != 1)
        {
            return false;
        }
        scenesData[index].IsUnlocked = true;
        return true;
    }
    public void LoadAsync(string name, Action<GameObject> callBack, int indexLevel)
    {
        index = indexLevel;
        GameManager.Instance.StartCoroutine(LoadScene(Path.Combine("Scenes", name), callBack));
    }
    private IEnumerator LoadScene(string path, Action<GameObject> callBack)
    {
        var request = Resources.LoadAsync(path, typeof(GameObject));
        yield return request;
        yield return new WaitForSeconds(1.45f);
        var obj = request.asset as GameObject;
        obj = GameObject.Instantiate(obj);
        callBack?.Invoke(obj);
    }

}
