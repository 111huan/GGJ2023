using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector3 nextPosition;
    bool move = false;//�Ƿ�ʼ�ƶ�����һ������
    float cameraSpeed = 2f;
    void Start()
    {
        //����nextPosition
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
