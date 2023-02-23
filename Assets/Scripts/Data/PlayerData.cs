using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Water, // 水
    San,
}

public class PlayerData
{
    public static PlayerData instance = new PlayerData();
    public Dictionary<ItemType, int> itemBox = new Dictionary<ItemType, int>();

    public void Init()
    {
        itemBox.Add(ItemType.Water, 10);
        itemBox.Add(ItemType.San, 0);
    }

    #region 添加资源
    public void AddItem(Dictionary<ItemType, int> typeDic)
    {
        foreach (var item in typeDic)
        {
            if (item.Value != 0)
            {
                itemBox[item.Key] += item.Value;
            }
        }
        if (itemBox[ItemType.San] < 0) itemBox[ItemType.San] = 0;
        ChenkFail();
    }

    public void AddItem(ItemType type, int num)
    {
        itemBox[type] += num;
        if (itemBox[ItemType.San] < 0) itemBox[ItemType.San] = 0;
        ChenkFail();
    }
    #endregion

    private void ChenkFail()
    {
        foreach (var item in itemBox)
        {
            if (item.Key == ItemType.San) continue;
            if (item.Value <= 0)
            {
                GameManager.instance.GameFinish(false);
            }
        }
    }
}