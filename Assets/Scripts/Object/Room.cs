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
        //Ԥ��������
    }
    public void CloseLight()
    {
        //Ԥ����ָ�
    }
    */
    /*
    private void Start()
    {
        SetObjInside();
    }
    /// <summary>
    /// �����ڲ�����ʾ�Ĳ���
    /// </summary>
    /// <param name="paraA">����A��ֵ</param>
    /// <param name="paraB">����B��ֵ</param>
    /// <param name="paraC">����C��ֵ</param>
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
    /// ����A��ֵȷ��A����������ʾ
    /// </summary>
    private void DisplayOnParaA() { }
    private void DisplayOnParaB() { }
    private void DisplayOnParaC() { }
}
