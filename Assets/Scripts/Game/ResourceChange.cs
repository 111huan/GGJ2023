using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceChange : MonoBehaviour
{
    Player player;
    float resourceA = 0;//��Ϸ��ʼ��ʼ��Դ��
    float resourceB = 0;
    float resourceC = 0;
    float reduceRateA = Time.deltaTime;//ÿ������a��Դ
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
        //player.OnNumChange(reduceRateA, reduceRateB, reduceRateC);//��Դÿ������Ȼ���٣�Ҫ��Ҫ����
        if (resourceA * resourceB * resourceC == 0)
        {
            gameFail();
        }
        if (achieveBottom)
        {
            gameSuccess();
        }
        if (move)//���ж����ø÷�����Դ
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
