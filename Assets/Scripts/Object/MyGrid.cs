using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public int paraA = 0;
        public int paraB = 0;
        public int paraC = 0;
    }
    public Data data;
    public void ClearData()
    {
        data.paraA = 0;
        data.paraB = 0;
        data.paraC = 0;
    }
    public void SetData(int i, int j, int k)
    {
        data.paraA = i;
        data.paraB = j;
        data.paraC = k;
    }
}
