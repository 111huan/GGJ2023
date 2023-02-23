using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ayi : MonoBehaviour
{
    public GameObject zou;
    public GameObject pao;
    public GameObject die;
    public GameObject xue;

    public float waitToPao = 1;
    public float waitToDie = 1;
    //public float waitToDismiss = 1;

    private void Awake()
    {
        pao.SetActive(false);
        die.SetActive(false);
        xue.SetActive(false);

        //StartCoroutine(Wait());
    }

    public void AyiPao()
    {
        GetComponent<Animator>().SetBool("跑", true);
        pao.SetActive(true);
        zou.SetActive(false);
    }

    public void AyiSi()
    {
        StartCoroutine(Wait1());
    }

    IEnumerator Wait1()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(GetComponent<Animator>());
        pao.SetActive(false);
        die.SetActive(true);
        die.GetComponent<Animator>().SetBool("die", true);
        GetComponent<Animator>().SetBool("死", true);
        xue.SetActive(true);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitToPao);
        AyiPao();

        yield return new WaitForSeconds(waitToDie);
        AyiSi();

        //yield return new WaitForSeconds(waitToDismiss);
        //gameObject.SetActive(false);
    }
}
