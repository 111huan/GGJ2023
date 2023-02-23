using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public int paraA = 0;
        public int paraB = 0;
        public int paraC = 0;
    }
    public Data data;
    /*
    public void HightLight()
    {
        //预制体亮显
    }
    public void CloseLight()
    {
        //预制体恢复
    }
    */
    /*
    private void Start()
    {
        SetObjInside();
    }
    /// <summary>
    /// 设置内部会显示的部件
    /// </summary>
    /// <param name="paraA">属性A的值</param>
    /// <param name="paraB">属性B的值</param>
    /// <param name="paraC">属性C的值</param>
    public void SetObjInside(int paraA = 0, int paraB = 0, int paraC = 0)
    {
        DisplayOnParaA();
        DisplayOnParaB();
        DisplayOnParaC();
    }
    */
    public void SetParaInside(int paraA = 0, int paraB = 0, int paraC = 0)
    {
        data.paraA = paraA;
        data.paraB = paraB;
        data.paraC = paraC;
    }
    /// <summary>
    /// 根据A的值确定A相关物体的显示
    /// </summary>
    private void DisplayOnParaA() { }
    private void DisplayOnParaB() { }
    private void DisplayOnParaC() { }
}
