using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RoomBase : MonoBehaviour
{
    public Room3d room3D;
    public RoomType roomType = RoomType.NormalRoom; // 3d房间id
    [ShowInInspector] public Dictionary<ItemType, int> reward = new Dictionary<ItemType, int>(); // 奖励
    public bool lockLeft;
    public bool lockDown;
    public bool lockRight;
    public bool lockTop;

    public int x;
    public int y;

    public bool linkTop;
    public bool linkDown;
    public bool linkLeft;
    public bool linkRight;

    public void SetType(RoomType roomType)
    {
        this.roomType = roomType;

        switch (roomType)
        {
            case RoomType.NormalRoom:
                reward.Add(ItemType.San, 1);
                reward.Add(ItemType.Water, 1);
                break;
            case RoomType.StraightDirRoom:
                reward.Add(ItemType.San, 1);
                reward.Add(ItemType.Water, 1);
                break;
            case RoomType.RandomDirRoom:
                reward.Add(ItemType.San, 1);
                reward.Add(ItemType.Water, 1);
                break;
            case RoomType.StrikeRoom:
                reward.Add(ItemType.San, 1);
                reward.Add(ItemType.Water, 1);
                break;
            case RoomType.StopRoom:
                reward.Add(ItemType.San, 1);
                reward.Add(ItemType.Water, 1);
                break;
            case RoomType.GrassRoom:
                reward.Add(ItemType.San, 1);
                reward.Add(ItemType.Water, 1);
                break;
            case RoomType.ManRoom1:
                reward.Add(ItemType.San, 1);
                reward.Add(ItemType.Water, 1);
                break;
            case RoomType.ManRoom2:
                reward.Add(ItemType.San, 1);
                reward.Add(ItemType.Water, 1);
                break;
            default:
                break;
        }
    }

    public void LinkNextRoom(RoomBase nextRoom)
    {
        if (x + 1 == nextRoom.x) // 往右连接
        {
            linkRight = true;
            nextRoom.linkLeft = true;
        }
        if (x - 1 == nextRoom.x) // 往左连接
        {
            linkLeft = true;
            nextRoom.linkRight = true;
        }
        if (y + 1 == nextRoom.y) // 往下连接
        {
            linkDown = true;
            nextRoom.linkTop = true;
        }
    }

    #region 3d房间
    public void Creat3DRoom() // 创建3d房间
    {
        if (room3D == null)
        {
            //Debug.Log("aaaa" + "Rooms/" + roomType.ToString(), gameObject);
            GameObject obj = Resources.Load<GameObject>("Rooms/" + roomType.ToString());

            room3D = Instantiate(obj, transform).GetComponent<Room3d>();

            PlayerData.instance.AddItem(ItemType.Water, -1);
            AudioManager.instance.PlayOneShout(AudioManager.instance.pobi);
            if (roomType == RoomType.RandomDirRoom)
                AudioManager.instance.PlayOneShout(AudioManager.instance.laugh);
        }
        Refresh3DRoomUI();
    }

    public void Refresh3DRoomUI() // 刷新3d房间UI
    {
        if (room3D != null)
        {
            room3D.RefreshRoots(this);
        }
    }

    public void ChangeRoom3dType(bool first)
    {
        room3D.TypeChange(this, first);
    }
    #endregion

    public void OnClickRoom()
    {
        GameManager.instance.ClickRoom(this);
        Debug.Log("点击" + ToString());
    }

    public override string ToString()
    {
        return $"roadx:{x},roady:{y}";
    }
}
