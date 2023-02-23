using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[Serializable]
public class UserData
{
    public int maxLevelIndex = 0;
    public int coins = 100;
}

//关卡循环数据
[Serializable]
public class LoopLevelData
{
    public int currentIdx;//当前下标
    public List<int> idList = new List<int>();//关卡池
}

public enum ItemChangeFrom
{
    Initial,//初始赠送
    Orders,//订单
    ReviveTimeUp,//时间不足复活
    ReviveBoxFull,//箱子已满复活
    NotEnough,//不足
}

// 数据缓存
public class UserDataManager
{
    public static UserDataManager instance = new UserDataManager();

    [HideInInspector] public UserData data;
    const string localPath = "/userInfo.dat";

    public void Init()
    {
        ReadFromLocal();
    }

    //判断是否新用户
    public bool isNewUser()
    {
        string filePath = Application.persistentDataPath + localPath;
        bool isLocalExist = File.Exists(filePath);
        return !isLocalExist;
    }

    #region 存储
    void ReadFromLocal()
    {
        data = ReadLocalData<UserData>(localPath);
        if (data == null)
        {
            data = new UserData();

            SaveToLocal();
        }
    }

    void SaveToLocal()
    {
        SaveLocalData(data, localPath);
    }
    #endregion



    public int MaxLevelIndex()
    {
        return data.maxLevelIndex;
    }

    public int CurrentLevelID
    {
        get
        {
            return data.maxLevelIndex + 1;
        }
    }

    public void LevelPass()
    {
        data.maxLevelIndex++;
        SaveToLocal();
    }



    public static T ReadLocalData<T>(string path)
    {
        string filePath = Application.persistentDataPath + path;
        bool isLocalExist = File.Exists(filePath);
        if (isLocalExist)
        {
            FileStream f = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                f = File.Open(filePath, FileMode.Open);
                T dataT = (T)bf.Deserialize(f);
                f.Close();

                return dataT;
            }
            catch (Exception ex)
            {
                if (f != null)
                {
                    f.Close();
                }
                Debug.LogError($"读取{typeof(T)}失败:{ex.Message}");
            }
        }

        return default(T);
    }

    public static void SaveLocalData<T>(T dataT, string path)
    {
        string filePath = Application.persistentDataPath + path;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream f = File.Open(filePath, FileMode.OpenOrCreate);
        bf.Serialize(f, dataT);
        f.Close();
    }

    public static void DeleteLocalData(string path)
    {
        string filePath = Application.persistentDataPath + path;
        bool isLocalExist = File.Exists(filePath);
        if (isLocalExist)
        {
            File.Delete(filePath);
        }
    }
}