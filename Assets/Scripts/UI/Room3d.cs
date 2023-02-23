using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

/// <summary>
/// 树根几种可能
/// 上、左、右
/// 上左、上右、上下
/// 左下、左右、右下
///
/// 如果有分支
/// 上左下、上右下、上左右、左右下
/// 上下左右
/// </summary>

public enum RootsType
{
    Top,
    Left,
    Right,
    TopLeft,
    TopRight,
    TopDown,
    LeftDown,
    LeftRight,
    RightDown,

    TopLeftDown,
    TopRightDown,
    TopLeftRight,
    LeftRightDown,
    All
}

public class Room3d : MonoBehaviour
{
    public RoomType roomType;
    public GameObject[] lighta;

    public GameObject leftStrone;
    public GameObject rightStrone;
    public GameObject[] grass;

    private int progressType = 0;
    private void Awake()
    {
        if (transform.Find("通用房间素材/藤曼组") != null)
            transform.Find("通用房间素材/藤曼组").gameObject.SetActive(false);
    }

    public void TypeChange(RoomBase room, bool first)
    {
        if (first && progressType == 0)
        {
            AyiPao();
            progressType = 1;
        }
        else if (!first && progressType == 1)
        {
            if (GameManager.instance.currentRoom == room)
            {
                if (transform.Find("通用房间素材/藤曼组") != null)
                {
                    transform.Find("通用房间素材/藤曼组").gameObject.SetActive(true);
                    foreach (Transform item in transform.Find("通用房间素材/藤曼组"))
                    {
                        item.GetComponent<Animator>().SetBool("藤曼长", true);
                    }
                }
                if (room.roomType == RoomType.GrassRoom)
                {
                    foreach (var item in grass)
                    {
                        DOTween.To((float f) =>
                        {
                            item.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("消散", f);
                        }, 0, 1, 0.2f);

                    }
                }
            }
            AyiSi();
            progressType = 2;
        }
    }

    public void AyiPao()
    {
        Ayi[] ayis = GetComponentsInChildren<Ayi>();
        foreach (var item in ayis)
        {
            item.AyiPao();
        }
    }

    public void AyiSi()
    {
        Ayi[] ayis = GetComponentsInChildren<Ayi>();
        foreach (var item in ayis)
        {
            item.AyiSi();
        }
    }

    public void Through(RoomBase room)
    {
        if (transform.Find("特效 "))
        {
            if (room.linkTop)
            {
                if (!transform.Find("特效 /屋顶穿墙").gameObject.activeSelf)
                {
                    transform.Find("特效 /屋顶穿墙").gameObject.SetActive(true);
                }
            }
            if (room.linkLeft)
            {
                if (!transform.Find("特效 /左边穿墙").gameObject.activeSelf)
                {
                    transform.Find("特效 /左边穿墙").gameObject.SetActive(true);
                }
            }
            if (room.linkRight)
            {
                if (!transform.Find("特效 /右边穿墙").gameObject.activeSelf)
                {
                    transform.Find("特效 /右边穿墙").gameObject.SetActive(true);
                }
            }
        }
    }

    public void RefreshStrone(RoomBase room)
    {
        if (room.roomType == RoomType.StopRoom)
        {
            if (room.linkLeft)
            {
                leftStrone.SetActive(false);
            }
            if (room.linkRight)
            {
                rightStrone.SetActive(false);
            }
        }
    }

    public void RefreshRoots(RoomBase room)
    {
        Through(room);
        RefreshStrone(room);
        transform.Find("通用房间素材/小根1").gameObject.SetActive(false);
        transform.Find("通用房间素材/小根2").gameObject.SetActive(false);
        transform.Find("通用房间素材/小根3").gameObject.SetActive(false);
        foreach (Transform item in transform.Find("通用房间素材/大根"))
        {
            item.gameObject.SetActive(false);
        }

        if (GameManager.instance.currentRoom == room)
        {
            foreach (var item in lighta)
            {
                item.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var item in lighta)
            {
                item.gameObject.SetActive(false);
            }
        }

        if (room.linkTop && !room.linkLeft && !room.linkRight && !room.linkDown) // 上
        {
            transform.Find("通用房间素材/小根1").gameObject.SetActive(true);
            transform.Find("通用房间素材/小根1").GetComponent<Animator>().SetBool("根生长", true);
        }
        else if (!room.linkTop && room.linkLeft && !room.linkRight && !room.linkDown) // 左
        {
            transform.Find("通用房间素材/小根2").gameObject.SetActive(true);
            transform.Find("通用房间素材/小根2").GetComponent<Animator>().SetBool("根生长", true);
        }
        else if (!room.linkTop && !room.linkLeft && room.linkRight && !room.linkDown) // 右
        {
            transform.Find("通用房间素材/小根3").gameObject.SetActive(true);
            transform.Find("通用房间素材/小根3").GetComponent<Animator>().SetBool("根生长", true);
        }
        else if (room.linkTop && room.linkLeft && !room.linkRight && !room.linkDown) // 上左
        {
            transform.Find("通用房间素材/大根/5").gameObject.SetActive(true);
        }
        else if (room.linkTop && !room.linkLeft && room.linkRight && !room.linkDown) // 上右
        {
            transform.Find("通用房间素材/大根/6").gameObject.SetActive(true);
        }
        else if (room.linkTop && !room.linkLeft && !room.linkRight && room.linkDown) // 上下
        {
            transform.Find("通用房间素材/大根/8").gameObject.SetActive(true);
        }
        else if (!room.linkTop && room.linkLeft && !room.linkRight && room.linkDown) // 左下
        {
            transform.Find("通用房间素材/大根/10").gameObject.SetActive(true);
        }
        else if (!room.linkTop && room.linkLeft && room.linkRight && !room.linkDown) // 左右
        {
            transform.Find("通用房间素材/大根/9").gameObject.SetActive(true);
        }
        else if (!room.linkTop && !room.linkLeft && room.linkRight && room.linkDown) // 右下
        {
            transform.Find("通用房间素材/大根/11").gameObject.SetActive(true);
        }
        else if (room.linkTop && room.linkLeft && !room.linkRight && room.linkDown) // 上左下
        {
            transform.Find("通用房间素材/大根/2").gameObject.SetActive(true);
        }
        else if (room.linkTop && !room.linkLeft && room.linkRight && room.linkDown) // 上右下
        {
            transform.Find("通用房间素材/大根/3").gameObject.SetActive(true);
        }
        else if (room.linkTop && room.linkLeft && room.linkRight && !room.linkDown) // 上左右
        {
            transform.Find("通用房间素材/大根/4.001").gameObject.SetActive(true);
        }
        else if (!room.linkTop && room.linkLeft && room.linkRight && room.linkDown) // 左右下
        {
            transform.Find("通用房间素材/大根/4").gameObject.SetActive(true);
        }
        else if (room.linkTop && room.linkLeft && room.linkRight && room.linkDown) // 上下左右
        {
            transform.Find("通用房间素材/大根/7").gameObject.SetActive(true);
        }
    }
}
