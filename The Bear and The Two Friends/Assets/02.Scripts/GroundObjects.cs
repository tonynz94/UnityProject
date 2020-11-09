using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundObjects : MonoBehaviour {

    Vector3 nowScale;

    private void Awake()
    {
        nowScale = gameObject.transform.localScale;
    }

    public void Disappear()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    public void Show()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        gameObject.transform.DOScale(nowScale, 0.2f).SetEase(Ease.Linear).OnComplete(Show2);

        gameObject.transform.DOLocalMoveY(0.05f, 0.2f).OnComplete(Show3);
    }

    void Show2()
    {
        gameObject.transform.DOPunchScale(new Vector3(0.01f, 0.01f, 0.01f), 0.5f);
    }

    void Show3()
    {
        gameObject.transform.DOLocalMoveY(0, 0.3f);
    }
}
