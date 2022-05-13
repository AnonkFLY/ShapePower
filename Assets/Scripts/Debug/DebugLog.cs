using System.Diagnostics;
using UnityEngine;


public static class DebugLog
{
    [Conditional("DEBUG")]
    public static void Message(object obj, GameObject go = null)
    {
        UnityEngine.Debug.Log(obj, go);
    }
    [Conditional("DEBUG")]
    public static void Warning(object obj, GameObject go = null)
    {
        UnityEngine.Debug.LogWarning(obj, go);
    }
    [Conditional("DEBUG")]
    public static void Error(object obj, GameObject go = null)
    {
        UnityEngine.Debug.LogError(obj, go);
    }
}
