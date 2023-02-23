using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// A���Ե�ֵ,Ĭ��Ϊ10
    /// </summary>
    [HideInInspector] public int mA_Num = 10;
    /// <summary>
    /// B���Ե�ֵ,Ĭ��Ϊ10
    /// </summary>
    [HideInInspector] public int mB_Num = 10;
    /// <summary>
    /// C���Ե�ֵ,Ĭ��Ϊ10
    /// </summary>
    [HideInInspector] public int mC_Num = 10;
    /// <summary>
    /// ����A���Ե�ֵ
    /// </summary>
    /// <param name="numA"></param>
    private void OnChangeNumA(int numA)
    {
        mA_Num += numA;
    }
    /// <summary>
    /// ����B���Ե�ֵ
    /// </summary>
    /// <param name="numB"></param>
    private void OnChangeNumB(int numB)
    {
        mB_Num += numB;
    }
    /// <summary>
    /// ����A���Ե�ֵ
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
