using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    //获取格子数量
    public Transform gridParent;
    public int gridRow;
    public int gridCol;
    //格子的预置体
    public MyGrid[][] grids;

    //处理鼠标事件
    private Vector3 mouseDownPos, mouseUpPos;
    private void Awake()
    {
        //初始化格子
        InitListGrids();
    }
    public void InitListGrids()
    {
        MyGrid[] mMyGrids = gridParent.GetComponentsInChildren<MyGrid>();
        //初始化格子
        grids = new MyGrid[gridRow][];
        for (int i = 0; i < gridRow; i++)
        {
            grids[i] = new MyGrid[gridCol];
            for (int j = 0; j < gridCol; j++)
            {
                int tempIndex = i * gridRow + j;
                grids[i][j] = mMyGrids[tempIndex];
            }
        }
    }
}