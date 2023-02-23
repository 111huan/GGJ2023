using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NumMove : MonoBehaviour
{
    private void Start()
    {
        DoMove();
        StartCoroutine(Dele());
    }
    public void DoMove()
    {
        this.gameObject.transform.DOLocalMoveY(65f, 0.8f).SetEase(Ease.OutQuart);
    }
    IEnumerator Dele()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
