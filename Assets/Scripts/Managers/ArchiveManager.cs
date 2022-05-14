using System;
using System.IO;
using System.Text;
using UnityEngine;

public class ArchiveManager<T> where T : new()
{
    public Action<T> onDataChange;
    public T archiveObj;
    private readonly string savedPath = Path.Combine(Application.persistentDataPath, "ArchiveSave.saved");

    public void Saved()
    {
        if (archiveObj == null)
        {
            DebugLog.Error("ArchiveObj is null");
            return;
        }
        using (var stream = File.Open(savedPath, FileMode.Create))
        {
            var jsonText = JsonUtility.ToJson(archiveObj);
            var buffer = Encoding.UTF8.GetBytes(jsonText);
            stream.Write(buffer, 0, buffer.Length);
        }
    }
    public void Load()
    {
        DebugLog.Message(savedPath);
        if (!File.Exists(savedPath))
        {
            using (var stream = File.Create(savedPath))
            {
                archiveObj = new T();
            }
            LoadData();
            return;
        }
        using (var saved = File.OpenText(savedPath))
        {
            var jsonData = saved.ReadToEnd();
            archiveObj = JsonUtility.FromJson<T>(jsonData);
            if (archiveObj == null)
                archiveObj = new T();
        }
        LoadData();
    }
    private void LoadData()
    {
        onDataChange?.Invoke(archiveObj);
        Saved();
    }
}
