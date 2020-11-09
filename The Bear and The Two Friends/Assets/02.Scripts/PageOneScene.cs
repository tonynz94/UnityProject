using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PageOneScene : MonoBehaviour {

    bool _statusCompleted = false;
    
    public Image fader;

    public GameObject nextButton;

    public GameObject narrationBox;
    public Text narrationText;

    public GameObject speechBox;
    public Text speechText;
    Vector3 speechBoxSize;

    public GameObject lankyBox;
    Vector3 lankyBoxSize;
    public GameObject fattyBox;
    Vector3 fattyBoxSize;

    public GameObject selectableBox;
    Vector3 selectableBoxSize;

    public GameObject endingBox;

    int selectedNumber;

    public int status = 0;

    public bool _sceneFinished = false;

    //------------------------------
    // 애니메이션 관련
    public GameObject lanky;
    public GameObject fatty;

    public Animator lankyAnimator;
    public Animator fattyAnimator;

    public GameObject lankyTarget1;
    public GameObject lankyTarget2;

    public GameObject fattyTarget1;
    public GameObject fattyTarget2;

    private void Awake()
    {
        speechBoxSize = speechBox.transform.localScale;
        lankyBoxSize = lankyBox.transform.localScale;
        fattyBoxSize = fattyBox.transform.localScale;
        selectableBoxSize = selectableBox.transform.localScale;
    }

    void Start()
    {
        // PlayStory(status);
    }
    
    public void PlayStory(int status)
    {
        nextButton.SetActive(false);
        _statusCompleted = false;

        if (_sceneFinished)
            return;

        switch (status)
        {
            case 0:
                StartCoroutine(IntroCut());     // "홀쭉이와 통통이는 절친한 친구 사이에요."
                break;
            case 1:
                StartCoroutine(IntroCut2());    // "둘은 함께 숲 속을 여행하는 중이었지요."
                break;
            case 2:
                StartCoroutine(Speech1());      // "이야~! 날씨가 너무 화창하고 기분이 좋아!"
                Walk();
                break;
            case 3:
                StartCoroutine(Speech2());      // "게다가 울창한 수풀이 만들어주는 그늘이 너무 시원해!"
                Walk();
                break;
            case 4:
                StartCoroutine(Speech3());      // "덕분에 우리가 챙겨온 양산은 필요가 없는걸?, 하하하"
                Walk();
                break;
            case 5:
                DOTween.KillAll();
                StartCoroutine(Speech4());      // "너와 이렇게 함께 여행을 하니 정말 행복해."
                break;
            case 6:
                StartCoroutine(Speech5());  // "우리 우정 앞으로도 영원하자."
                break;
            case 7:
                StartCoroutine(Select1());
                break;
            case 8:
                Walk();

                if (selectedNumber == 0)
                    StartCoroutine(Selected0());
                else if (selectedNumber == 1)
                    StartCoroutine(Selected1());
                else
                    StartCoroutine(Selected2());
                break;
            case 9:
                Walk();

                if (selectedNumber == 0)
                    StartCoroutine(Selected0_1());
                else
                    StartCoroutine(Selected1_1());
                break;
            case 10:
                Walk();

                if (selectedNumber != 0)
                    StartCoroutine(Selected1_2()); // "두 친구는 여행이 끝날 때 까지 거의 대화를 하지 않았답니다."
                break;
            case 11:
                Walk();

                if (selectedNumber != 0)
                    StartCoroutine(Selected1_3()); // "결국 두 친구는 어색한 사이가 되고 말았어요."
                break;
        }
    }

    IEnumerator IntroCut()
    {
        Debug.Log("인트로컷1 실행~~~~");
        GameManager.Instance.inputAllowed = false;

        yield return new WaitForSeconds(2f);

        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.lines1_1, 1.5f).SetEase(Ease.Linear);

        GameManager.Instance.inputAllowed = true;

        yield return new WaitForSeconds(1.6f);

        StartCoroutine(NarrationEndSet());
    }
    IEnumerator IntroCut2()
    {
        Debug.Log("인트로컷2 실행~~~~");
        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.lines1_2, 1.5f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(1.6f);

        StartCoroutine(NarrationEndSet());
    }
    IEnumerator Speech1()
    {
        StartCoroutine(SpeechStartSet(lankyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.lines1_3, 2f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }
    IEnumerator Speech2()
    {
        StartCoroutine(SpeechStartSet(fattyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.lines1_4, 2f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }
    IEnumerator Speech3()   // "덕분에 우리가 챙겨온 양산은 필요가 없는걸?, 하하하"
    {
        StartCoroutine(SpeechStartSet(lankyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.lines1_5, 2f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }
    IEnumerator Speech4()   // "너와 이렇게 함께 여행을 하니 정말 행복해."
    {
        StartCoroutine(SpeechStartSet(fattyBox));

        speechText.DOText(GameManager.Instance.lines1_6, 2f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }
    IEnumerator Speech5()   // "우리 우정 앞으로도 영원하자."
    {
        SpeechRepeatSet(fattyBox);

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.lines1_7, 2f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }
    IEnumerator Select1()
    {
        GameManager.Instance.inputAllowed = false;

        selectableBox.SetActive(true);

        selectableBox.transform.localScale = new Vector3(0, 0, 0);
        selectableBox.transform.DOScale(speechBoxSize, 0.2f);
        selectableBox.transform.DOScale(selectableBoxSize, 0.2f);

        yield return new WaitForSeconds(0.2f);

        selectableBox.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.3f);
    }
    public void Select1Button(int selectedNum)
    {
        selectedNumber = selectedNum;

        GameManager.Instance.inputAllowed = true;

        CutChange();
        status++;
        PlayStory(status);
    }
    IEnumerator Selected0()     // "당연하지. 기쁠 때나 힘들 때나 우리 항상 함께하자!"
    {
        StartCoroutine(SpeechStartSet(lankyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.selectableLines1_1, 2f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }
    IEnumerator Selected1()
    {
        StartCoroutine(SpeechStartSet(lankyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.selectableLines1_2, 2f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }
    IEnumerator Selected2()
    {
        StartCoroutine(SpeechStartSet(lankyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.selectableLines1_3, 2f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }
    IEnumerator Selected0_1()   // "나도 네가 어려울 때 가장 먼저 도와주는 친구가 될거야."
    {
        StartCoroutine(SpeechStartSet(fattyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.resultLines1_1, 2f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }
    IEnumerator Selected1_1()   // "통통이는 마음이 무척 상했어요."
    {
        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.resultLines1_2, 1.5f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(1.6f);

        StartCoroutine(NarrationEndSet());
    }
    IEnumerator Selected1_2()   // "두 친구는 여행이 끝날 때 까지 거의 대화를 하지 않았답니다."
    {
        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.resultLines1_3, 2f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(NarrationEndSet());
    }
    IEnumerator Selected1_3()   // "결국 두 친구는 어색한 사이가 되고 말았어요."
    {
        Debug.Log(status);

        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.resultLines1_4, 1.8f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(1.9f);

        StartCoroutine(NarrationEndSet());
    }

    public void InputButton()
    {
        if (GameManager.Instance.inputAllowed == false)
            return;

        if(_statusCompleted == false)
        {
            CompleteAnimation(status);
        }
        else
        {
            if(status == 9 && selectedNumber == 0)  //마지막부분에 선택한것이 0분이면 씬을 바꾼다.
            {
                // 씬 전환
                _sceneFinished = true;

                fader.DOFade(1, 0.5f).OnComplete(ChangeScene);  //fadeout
            }
            else if(status == 11)
            {
                // 게임 오버
                _sceneFinished = true;

                endingBox.SetActive(true);
            
                TrackedOut();
                lanky.SetActive(false);
                fatty.SetActive(false);
            }
            else
            {
                CutChange();
                status++;
                PlayStory(status);
            }
        }
    }

    public void CompleteAnimation(int status)
    {
        DOTween.KillAll();
        StopAllCoroutines();
        StopWalk();

        switch (status)
        {
            case 0:
                narrationBox.SetActive(true);
                narrationText.text = GameManager.Instance.lines1_1;
                break;
            case 1:
                narrationBox.SetActive(true);
                narrationText.text = GameManager.Instance.lines1_2;
                break;
            case 2:
                speechBox.SetActive(true);
                lankyBox.SetActive(true);
                fattyBox.SetActive(false);
                speechText.text = GameManager.Instance.lines1_3;
                break;
            case 3:
                speechBox.SetActive(true);
                lankyBox.SetActive(false);
                fattyBox.SetActive(true);
                speechText.text = GameManager.Instance.lines1_4;
                break;
            case 4:
                speechBox.SetActive(true);
                lankyBox.SetActive(true);
                fattyBox.SetActive(false);
                speechText.text = GameManager.Instance.lines1_5;
                break;
            case 5:
                speechBox.SetActive(true);
                lankyBox.SetActive(false);
                fattyBox.SetActive(true);
                speechText.text = GameManager.Instance.lines1_6;
                break;
            case 6:
                speechBox.SetActive(true);
                lankyBox.SetActive(false);
                fattyBox.SetActive(true);
                speechText.text = GameManager.Instance.lines1_7;    // "우리 우정 앞으로도 영원하자."
                break;
            case 7:
                break;
            case 8:
                speechBox.SetActive(true);
                lankyBox.SetActive(true);
                fattyBox.SetActive(false);

                if (selectedNumber == 0)
                    speechText.text = GameManager.Instance.selectableLines1_1;
                else if (selectedNumber == 1)
                    speechText.text = GameManager.Instance.selectableLines1_2;
                else
                    speechText.text = GameManager.Instance.selectableLines1_3;
                break;
            case 9:
                if (selectedNumber == 0)
                {
                    speechBox.SetActive(true);
                    lankyBox.SetActive(false);
                    fattyBox.SetActive(true);
                    speechText.text = GameManager.Instance.resultLines1_1;  // "나도 네가 어려울 때 가장 먼저 도와주는 친구가 될거야."
                }
                else
                {
                    narrationBox.SetActive(true);
                    narrationText.text = GameManager.Instance.resultLines1_2;   //  "통통이는 마음이 무척 상했어요."
                }
                break;
            case 10:
                if (selectedNumber != 0)
                {
                    narrationBox.SetActive(true);
                    narrationText.text = GameManager.Instance.resultLines1_3;
                }
                break;
            case 11:
                if (selectedNumber != 0)
                {
                    narrationBox.SetActive(true);
                    narrationText.text = GameManager.Instance.resultLines1_4;
                }
                break;
        }

        nextButton.SetActive(true);

        GameManager.Instance.inputAllowed = true;
        _statusCompleted = true;
    }

    void NarrationStartSet()
    {
        narrationBox.SetActive(true);
        narrationText.text = "";
    }

    IEnumerator NarrationEndSet()
    {
        _statusCompleted = true;

        yield return new WaitForSeconds(2f);

        CompleteAnimation(status);
    }

    IEnumerator SpeechStartSet(GameObject characterBox)
    {
        narrationBox.SetActive(false);

        speechBox.SetActive(false);
        lankyBox.SetActive(false);
        fattyBox.SetActive(false);

        GameManager.Instance.inputAllowed = false;

        speechBox.SetActive(true);
        speechBox.transform.localScale = new Vector3(0, 0, 0);
        speechText.text = "";
        speechBox.transform.DOScale(speechBoxSize, 0.2f);

        characterBox.SetActive(true);
        characterBox.transform.localScale = new Vector3(0, 0, 0);

        if(characterBox == lankyBox)
            characterBox.transform.DOScale(lankyBoxSize, 0.2f);
        else if(characterBox == fattyBox)
            characterBox.transform.DOScale(fattyBoxSize, 0.2f);

        yield return new WaitForSeconds(0.2f);

        speechBox.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.3f);

        characterBox.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.3f);

        yield return new WaitForSeconds(0.3f);

        GameManager.Instance.inputAllowed = true;
    }

    void SpeechRepeatSet(GameObject characterBox)
    {
        speechBox.SetActive(true);
        speechText.text = "";

        characterBox.SetActive(true);
    }

    IEnumerator SpeechEndSet()
    {
        _statusCompleted = true;

        yield return new WaitForSeconds(2f);

        CompleteAnimation(status);
    }

    public void TrackedOut()
    {
        CutChange();
        
        DOTween.KillAll();
    }

    void CutChange()
    {
        StopAllCoroutines();

        _statusCompleted = false;

        nextButton.SetActive(false);

        narrationText.text = "";
        narrationBox.SetActive(false);

        speechText.text = "";
        speechBox.SetActive(false);

        lankyBox.SetActive(false);
        fattyBox.SetActive(false);

        selectableBox.SetActive(false);

        StopWalk();
    }

    void Walk()
    {
        lankyAnimator.SetBool("walk", true);
        fattyAnimator.SetBool("walk", true);

        if (lanky.transform.position.x <= lankyTarget1.transform.position.x)
        {
            lanky.transform.DOMove(lankyTarget1.transform.position, 5f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(StopWalk);
        }
        else if(lanky.transform.position.x <= lankyTarget2.transform.position.x)
        {
            lanky.transform.DOMove(lankyTarget1.transform.position, 5f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(StopWalk);
        }
        else if(lanky.transform.position.x >= lankyTarget2.transform.position.x)
        {
            StopWalk();
        }

        if (fatty.transform.position.x <= fattyTarget1.transform.position.x)
        {
            fatty.transform.DOMove(fattyTarget1.transform.position, 5f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(StopWalk);
        }
        else if (fatty.transform.position.x <= fattyTarget2.transform.position.x)
        {
            fatty.transform.DOMove(fattyTarget1.transform.position, 5f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(StopWalk);
        }
        else if (fatty.transform.position.x >= fattyTarget2.transform.position.x)
        {
            StopWalk();
        }
    }

    void StopWalk()
    {
        if (_sceneFinished)
            return;

        lankyAnimator.SetBool("walk", false);
        fattyAnimator.SetBool("walk", false);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Page02");
    }

    public void RestartGame(bool _main)
    {
        if(_main)
            fader.DOFade(1, 0.5f).OnComplete(MainScene);
        else
            fader.DOFade(1, 0.5f).OnComplete(NowScene);
    }

    void MainScene()
    {
        SceneManager.LoadScene("Main");
    }

    void NowScene()
    {
        SceneManager.LoadScene("Page01");
    }
}
