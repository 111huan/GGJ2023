using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    private int waterPara;
    private int proteinPara;
    private Text waterText;
    private Text proteinText;
    private Text sanText;
    private void Awake()
    {
        PlayerData.instance.Init();
        waterText = this.gameObject.transform.Find("Top/Water").GetComponent<Text>();
        print("waterText:" + waterText);
        proteinText = this.gameObject.transform.Find("Top/Protein").GetComponent<Text>();
        sanText = this.gameObject.transform.Find("Top/San").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        waterPara = PlayerData.instance.itemBox[ItemType.Water];
        waterText.text = "water:" + waterPara;
        proteinText.text = "protein:" + waterPara;
    }
}
