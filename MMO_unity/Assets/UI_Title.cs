using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Title : UI_Base
{
    Animator animator;


    GameObject _bodyText;
    GameObject _sceneButton;

    enum GameObjects
    {
        HeadText,
        BodyText,
        SceneButton,
        LoadingBar
    }

    void Awake()
    {
        Bind<GameObject>(typeof(GameObjects));
    }
    public override void Start()
    {   
        Init();   
    }

    public override void Init()
    {
        GameObject titleText = Get<GameObject>((int)GameObjects.HeadText);
        animator = titleText.GetComponent<Animator>();

        _bodyText = Get<GameObject>((int)GameObjects.BodyText);
        _sceneButton = Get<GameObject>((int)GameObjects.SceneButton);

        _bodyText.SetActive(false);
        _sceneButton.SetActive(false);

        StartCoroutine(coBodyTitle());
    }

    IEnumerator coBodyTitle()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        _bodyText.SetActive(true);
        _sceneButton.SetActive(true);
    }

    public void LoadScene(int sceneindex)
    {
        StartCoroutine(coLoadAsynScene(1));
    }

    IEnumerator coLoadAsynScene(int sceneIndex)
    {
        while(true)
        {
            yield return null;
        }
    }
}
