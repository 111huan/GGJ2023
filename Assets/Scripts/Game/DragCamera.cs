using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : MonoBehaviour
{
    private Vector3 cameraPosOri = Vector3.zero;
    private Vector3 cameraPosAfter = Vector3.zero;
    private Vector3 mousePosOri = Vector3.zero;
    private int cameraStage = 0;
    private int mouseStage = 0;
    public float speed = 0f;
    public float coe = 0.1f;
    private void OnGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mouseStage == 0)
            {
                cameraPosOri = this.gameObject.transform.position;
                mousePosOri = Input.mousePosition;
                this.transform.GetComponent<CameraFollow>().enabled = false;
                mouseStage = 1;
            }
        }
        if (Input.GetMouseButton(0))
        {
            print("mousePosOri:" + mousePosOri);
            Vector3 deltaMouseMove = Input.mousePosition - mousePosOri;
            this.gameObject.transform.position += cameraPosOri + deltaMouseMove * coe;
        }
        if (Input.GetMouseButtonUp(0))
        {
            cameraPosAfter = this.gameObject.transform.position;
            cameraStage = 1;
            mouseStage = 0;
        }
    }
    private void Update()
    {
        if (cameraStage == 1)
        {
            Vector3 tempVector3 = Vector3.Lerp(cameraPosAfter, cameraPosOri, speed * Time.deltaTime);
            this.gameObject.transform.position = tempVector3;
            if (tempVector3 == cameraPosOri)
            {
                this.transform.GetComponent<CameraFollow>().enabled = true;
                cameraStage = 0;
            }
        }
    }
}
