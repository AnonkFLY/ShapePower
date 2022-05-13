using System;
using System.IO;
using UnityEngine;

public class ArchiveManager<T> where T : new()
{
    public T archiveObj;
    private readonly string savedPath = Path.Combine(Application.persistentDataPath, "ArchiveSave.saved");

    public void Saved()
    {

    }
    public void Load()
    {
        if (!File.Exists(savedPath))
        {
            using (File.Create(savedPath))
            {
                archiveObj = new T();
                JsonUtility.ToJson(archiveObj);
            }
            LoadData();
            return;
        }
        using(var saved = File.OpenText(savedPath))
        {
            var jsonData = saved.ReadToEnd();
            archiveObj = JsonUtility.FromJson<T>(jsonData);
        }
        LoadData();
    }
    private void LoadData()
    {

    }
}
