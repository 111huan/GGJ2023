using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// A属性的值,默认为10
    /// </summary>
    [HideInInspector] public int mA_Num = 10;
    /// <summary>
    /// B属性的值,默认为10
    /// </summary>
    [HideInInspector] public int mB_Num = 10;
    /// <summary>
    /// C属性的值,默认为10
    /// </summary>
    [HideInInspector] public int mC_Num = 10;
    /// <summary>
    /// 设置A属性的值
    /// </summary>
    /// <param name="numA"></param>
    private void OnChangeNumA(int numA)
    {
        mA_Num += numA;
    }
    /// <summary>
    /// 设置B属性的值
    /// </summary>
    /// <param name="numB"></param>
    private void OnChangeNumB(int numB)
    {
        mB_Num += numB;
    }
    /// <summary>
    /// 设置A属性的值
    /// </summary>
    /// <param name="numA"></param>
    private void OnChangeNumC(int numC)
    {
        mC_Num += numC;
    }
    public void OnNumChange(int numA = 0, int numB = 0, int numC = 0)
    {
        OnChangeNumA(numA);
        OnChangeNumB(numB);
        OnChangeNumC(numC);
    }
}
