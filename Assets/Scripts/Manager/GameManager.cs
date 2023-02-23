using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.IO;

// 1、相机跟随初始房间
// 2、监听上下左右选择
// 3、选择房间后转换
// 4、失败胜利监听
// 5、UI显示修改
public class GameManager : MonoSingleton<GameManager>
{
    public CameraFollow cameraFollow;
    public GameObject treeSuccess;
    public GameObject lightAfter;
    public GameObject treeFail;
    public Transform failPos;

    public RoomBase[] roomList;
    public Transform cameraPoint;

    public GridLayoutGroup group;
    private int cloumnCount = 11;
    private int rowMax;
    public RoomBase currentRoom;

    private bool finish;

    #region UI相关
    private int sanPara;
    private int waterPara;
    private int proteinPara;
    private Text waterText;
    private Text proteinText;
    private Text sanText;
    private Transform sanAddPos;
    private Transform waterAddPos;
    private Transform proteinAddPos;
    public GameObject plusOnePrefab;
    public GameObject minusOnePrefab;

    #endregion
    public void Awake()
    {
        Application.targetFrameRate = 60;
        List<RoomBase> temp = new List<RoomBase>();
        foreach (Transform item in group.transform)
        {
            temp.Add(item.GetComponent<RoomBase>());
        }
        //roomList = temp.ToArray();

        //foreach (var item in roomList)
        //{
        //    item.Creat3DRoom();
        //}

        ReadLevel();

        PlayerData.instance.Init();
        waterText = GameObject.Find("UICanvas/Top/Water").GetComponent<Text>();
        proteinText = GameObject.Find("UICanvas/Top/Protein").GetComponent<Text>();
        sanText = GameObject.Find("UICanvas/Top/San").GetComponent<Text>();
        sanAddPos = GameObject.Find("UICanvas/Top/San/SanAddPos").GetComponent<Transform>();
        waterAddPos = GameObject.Find("UICanvas/Top/Water/WaterAddPos").GetComponent<Transform>();
        proteinAddPos = GameObject.Find("UICanvas/Top/Protein/ProteinAddPos").GetComponent<Transform>();

        //结束动画
        //GameObject parentSuccess = GameObject.Find("TreeSuccess");
        //treeSuccess = parentSuccess.transform.Find("结算画面/骷髅树").gameObject;


        //PlayerData.instance.Init();
        //group.constraintCount = cloumnCount;

        //roomList = group.GetComponentsInChildren<RoomBase>();
        rowMax = roomList.Length / cloumnCount;



        // 给房间初始化x、y
        int row = 0;
        for (int i = 0; i < roomList.Length; i++)
        {
            RoomBase roomBase = roomList[i];
            roomBase.x = i % cloumnCount;
            roomBase.y = row;
            if ((i + 1) % cloumnCount == 0)
            {
                row++;
            }
        }
    }

    private void ReadLevel()
    {

        TextAsset text = Resources.Load<TextAsset>("level");
        string[] line = text.text.Split("\n");
        int index = 0;
        for (int i = 0; i < line.Length; i++)
        {
            if (string.IsNullOrEmpty(line[i])) continue;
            string[] each = line[i].Split('	');
            for (int j = 0; j < each.Length; j++)
            {
                roomList[index].SetType((RoomType)int.Parse(each[j]));
                index++;
            }
        }
    }

    private void Start()
    {
        // 初始化第一个房间
        currentRoom = roomList[Mathf.FloorToInt(cloumnCount / 2f)];
        currentRoom.linkTop = true;
        currentRoom.Creat3DRoom();
        cameraFollow.ChangeTarget(currentRoom.transform);
    }
    private void Update()
    {
        if (finish) return;
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            int yTo = currentRoom.y - 1;
            int xTo = currentRoom.x;
            if (InsideBorder(xTo, yTo))
            {
                ClickRoom(roomList[yTo * cloumnCount + xTo]);
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            int yTo = currentRoom.y + 1;
            int xTo = currentRoom.x;
            if (InsideBorder(xTo, yTo))
            {
                ClickRoom(roomList[yTo * cloumnCount + xTo]);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            int yTo = currentRoom.y;
            int xTo = currentRoom.x - 1;
            if (InsideBorder(xTo, yTo))
            {
                ClickRoom(roomList[yTo * cloumnCount + xTo]);
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            int yTo = currentRoom.y;
            int xTo = currentRoom.x + 1;
            if (InsideBorder(xTo, yTo))
            {
                ClickRoom(roomList[yTo * cloumnCount + xTo]);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentRoom.ChangeRoom3dType(false);
            //修改UI
            InitNumMove(currentRoom);
            PlayerData.instance.AddItem(currentRoom.reward);
            currentRoom.reward.Clear();
            StartCoroutine(SetUIText());

            if (currentRoom.roomType == RoomType.ManRoom1 || currentRoom.roomType == RoomType.ManRoom2)
            {
                AudioManager.instance.PlayOneShout(AudioManager.instance.kill);
            }
            else
            {
                AudioManager.instance.PlayOneShout(AudioManager.instance.zhanling);
            }
        }
    }

    //判断有没有超出边界
    private bool InsideBorder(int x, int y)
    {
        if (x >= cloumnCount || x < 0) return false;
        if (y >= rowMax || y < 0) return false;
        return true;
    }

    // 点击某个房间
    public void ClickRoom(RoomBase nextRoom)
    {
        if (CanMove(currentRoom, nextRoom))
        {
            RoomBase pre = currentRoom;
            ChangeRoom(nextRoom);

            pre.LinkNextRoom(nextRoom);
            pre.Refresh3DRoomUI();

            nextRoom.Creat3DRoom();
            nextRoom.Refresh3DRoomUI();
            currentRoom.ChangeRoom3dType(true);
        }
        else
        {
            Debug.Log("点击了非相邻的房间");
        }
    }

    IEnumerator SetUIText()
    {
        sanPara = PlayerData.instance.itemBox[ItemType.San];
        waterPara = PlayerData.instance.itemBox[ItemType.Water];
        yield return new WaitForSeconds(1f);
        sanText.text = "san:" + sanPara;
        waterText.text = "water:" + waterPara;
        proteinText.text = "protein:" + waterPara;
    }

    private void InitNumMove(RoomBase nextRoom)
    {
        if (nextRoom.reward.ContainsKey(ItemType.San))
        {
            if (nextRoom.reward[ItemType.San] < 0)
            {
                GameObject.Instantiate(minusOnePrefab, sanAddPos);
            }
            if (nextRoom.reward[ItemType.San] > 0)
            {
                GameObject.Instantiate(plusOnePrefab, sanAddPos);
            }
        }
        if (nextRoom.reward.ContainsKey(ItemType.Water))
        {
            print("here");
            if (nextRoom.reward[ItemType.Water] < 0)
            {
                GameObject.Instantiate(minusOnePrefab, waterAddPos);
            }
            if (nextRoom.reward[ItemType.Water] > 0)
            {
                GameObject.Instantiate(plusOnePrefab, waterAddPos);
            }
        }
    }

    private void ChangeRoom(RoomBase targetRoom)
    {
        currentRoom = targetRoom;
        cameraFollow.ChangeTarget(targetRoom.transform);
        if (targetRoom.roomType == RoomType.WinRoom)
        {
            GameFinish(true);
        }
    }

    public void GameFinish(bool success)
    {
        if (!finish)
        {
            Debug.Log("游戏结束：" + success);
            finish = true;
            cameraFollow.ChangeTarget(cameraPoint);
            Transform t = success ? treeSuccess.transform : treeFail.transform;
            //cameraFollow.transform.LookAt(t);
            lightAfter.SetActive(success);
            StartCoroutine(wait(success, t));
        }

        AudioManager.instance.bgmPlayer.Pause();
        AudioManager.instance.PlayOneShout(AudioManager.instance.win);
    }
    IEnumerator wait(bool success, Transform t)
    {
        yield return new WaitForSeconds(1);
        if (success == true)
        {
            t.gameObject.SetActive(true);
        }
        if (success == false)
        {
            cameraFollow.ChangeTarget(failPos);
        }
        //a.SetBool("开始", true);
    }

    // 是否在左、右、下
    private bool CanMove(RoomBase room1, RoomBase room2)
    {
        //if (room1.roomType == RoomType.OnlyIn)
        //{
        //    return false;
        //}
        //Debug.Log(room1.ToString());
        //Debug.Log(room2.ToString());
        if (room1.roomType == RoomType.StopRoom && room2.room3D == null) return false;
        if (room2.x >= cloumnCount || room2.x < 0) return false;
        if (room2.y >= rowMax || room2.y < 0) return false;
        if (room1.y == room2.y && Mathf.Abs(room1.x - room2.x) == 1 ||
            (room1.x == room2.x && room1.y + 1 == room2.y))
        {
            return true;
        }
        if (room1.x == room2.x && room1.y - 1 == room2.y)
        {
            return room1.linkTop;
        }
        return false;
    }
}
