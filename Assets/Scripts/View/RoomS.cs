using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomS : MonoBehaviour
{
    public GameObject roomPrefab;
    public Room[][] rooms;
    private void Start()
    {
        InitRoom(4, 5);
    }
    public void InitRoom(int Row, int Col)
    {
        rooms = new Room[Row][];
        for (int i = 0; i < Row; i++)
        {
            rooms[i] = new Room[Col];
            for (int j = 0; j < Col; j++)
            {
                rooms[i][j] = CreateRoom(i, j);
                rooms[i][j].SetParaInside();
            }
        }
    }
    private Room CreateRoom(int Row, int Col)
    {
        Vector3 pos = CalcPosition(Row, Col);
        GameObject gameObj = GameObject.Instantiate(roomPrefab, pos, Quaternion.Euler(Vector3.zero));
        return gameObj.GetComponent<Room>();
    }
    public Vector3 CalcPosition(int Row, int Col)
    {
        Vector3 posOri = new Vector3(-6.1f, 4.18f, 0f);
        float sizeX = 1f;
        float sizeY = -1f;
        Vector3 posResult = posOri + new Vector3(sizeX * Row, sizeY * Col, 0f);
        return posResult;
    }
}
