using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceChange : MonoBehaviour
{
    Player player;
    float resourceA = 0;//游戏开始初始资源量
    float resourceB = 0;
    float resourceC = 0;
    float reduceRateA = Time.deltaTime;//每秒消耗a资源
    float reduceRateB = Time.deltaTime;
    float reduceRateC = Time.deltaTime;
    bool achieveBottom = false;
    public static bool move = false;
    Room thisRoom;
    void Start()
    {
        resourceA = player.mA_Num;
        resourceB = player.mB_Num;
        resourceC = player.mC_Num;
        reduceRateA = thisRoom.data.paraA;
    }

    void Update()
    {
        //player.OnNumChange(reduceRateA, reduceRateB, reduceRateC);//资源每秒钟自然减少，要不要待定
        if (resourceA * resourceB * resourceC == 0)
        {
            gameFail();
        }
        if (achieveBottom)
        {
            gameSuccess();
        }
        if (move)//若行动则获得该房间资源
        {
            player.OnNumChange(thisRoom.data.paraA, thisRoom.data.paraB, thisRoom.data.paraC);
        }
    }

    public static void gameFail()
    {
        Debug.Log("Game Fail");
    }
    public static void gameSuccess()
    {
        Debug.Log("Game Success");
    }
}
