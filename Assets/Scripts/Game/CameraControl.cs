using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector3 nextPosition;
    bool move = false;//是否开始移动到下一个房间
    float cameraSpeed = 2f;
    void Start()
    {
        //输入nextPosition
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, nextPosition, cameraSpeed * Time.deltaTime);
        }
    }
}
