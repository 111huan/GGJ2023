using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset = Vector3.zero;
    private Vector3 pos;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
            offset = this.transform.position - target.position;

        //offset = target.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            pos = target.position + offset;
            this.transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
        }
    }

    public void ChangeTarget(Transform target)
    {
        if (offset == Vector3.zero) offset = this.transform.position - target.position;
        this.target = target;
    }
}
