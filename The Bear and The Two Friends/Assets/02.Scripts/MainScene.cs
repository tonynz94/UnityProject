using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour {

    public GameObject title;

    public GameObject startButton;

    public Image fader;

    public void Start()
    {
        fader.DOFade(0, 0.5f);
    }

    public void startGame()
    {
        title.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), 1).OnComplete(FadeOut);

        startButton.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0), 0.5f);

        Invoke("disappearButton", 0.5f);
    }

    void disappearButton()
    {
        startButton.SetActive(false);
    }

    void FadeOut()
    {
        fader.DOFade(1, 0.5f).OnComplete(ChangeScene);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Page01");
    }
}
