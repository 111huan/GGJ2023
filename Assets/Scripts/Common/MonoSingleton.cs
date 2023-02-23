using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                    obj.name = typeof(T).Name;
                    DontDestroyOnLoad(obj);
                    Debug.Log($"创建mono单例:<color=#4B7940>{obj.name}</color>", obj);
                }
            }
            return _instance;
        }
    }
}
