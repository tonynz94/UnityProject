using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PageThreeScene : MonoBehaviour
{
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

    private Vector3 lankyStartingPos;
    private Vector3 fattyStartingPos;
    
    public GameObject lankyTarget1;
    public GameObject fattyTarget1;

    private void Awake()
    {
        speechBoxSize = speechBox.transform.localScale;
        lankyBoxSize = lankyBox.transform.localScale;
        fattyBoxSize = fattyBox.transform.localScale;
        selectableBoxSize = selectableBox.transform.localScale;

        lankyStartingPos = lanky.transform.position;
        fattyStartingPos = fatty.transform.position;
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

        Walk(lanky, lankyTarget1.transform.position);
        Walk(fatty, fattyTarget1.transform.position);

        GameManager.Instance.inputAllowed = true;

        if (GameManager.Instance.ending == 0)
        {
            switch (status)
            {
                case 0:
                    StartCoroutine(Ending0());
                    break;
                case 1:
                    StartCoroutine(Ending0_1());
                    break;
                case 2:
                    StartCoroutine(Ending0_2());
                    break;
                case 3:
                    StartCoroutine(Ending0_3());
                    break;
                case 4:
                    StartCoroutine(Ending0_4());
                    break;
            }
        }
        else
        {
            switch (status)
            {
                case 0:
                    StartCoroutine(Ending1());
                    break;
                case 1:
                    StartCoroutine(Ending1_1());
                    break;
                case 2:
                    StartCoroutine(Ending1_2());
                    break;
                case 3:
                    StartCoroutine(Ending1_3());
                    break;
                case 4:
                    StartCoroutine(Ending1_4());
                    break;
                case 5:
                    StartCoroutine(Ending1_5());
                    break;
            }
        }
    }
    
    IEnumerator Ending0()
    {
        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.resultLines2_2_5, 1.5f).SetEase(Ease.Linear);     // "곰이 돌아간 후에 홀쭉이는 다시 나무 밑으로 내려왔어요."

        yield return new WaitForSeconds(1.6f);

        StartCoroutine(NarrationEndSet());
    }

    IEnumerator Ending0_1()
    {
        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.resultLines2_2_6, 1.5f).SetEase(Ease.Linear);     // "통통이도 다시 일어났답니다."

        yield return new WaitForSeconds(1.6f);

        StartCoroutine(NarrationEndSet());
    }

    IEnumerator Ending0_2()
    {
        StartCoroutine(SpeechStartSet(lankyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.resultLines3_1_1, 2f).SetEase(Ease.Linear);    // "그런데 친구야. 아까 곰이 너한테 뭐라고 속삭이는 것 같던데."

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Ending0_3()
    {
        SpeechRepeatSet(lankyBox);

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.resultLines3_1_2, 2f).SetEase(Ease.Linear);      // "뭐라고 그랬니?"

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Ending0_4()
    {
        StartCoroutine(SpeechStartSet(fattyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.resultLines3_1_3, 2f).SetEase(Ease.Linear);    // "위기에 처했을 때 혼자만 살겠다고 도망가는 녀석은 친구가 아니라더군."

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }


    IEnumerator Ending1()
    {
        StartCoroutine(SpeechStartSet(lankyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.resultLines3_2_1, 2f).SetEase(Ease.Linear);    // "정말 놀랬다니까. 휴우. 천만다행이야."

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Ending1_1()
    {
        StartCoroutine(SpeechStartSet(fattyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.resultLines3_2_2, 2f).SetEase(Ease.Linear);    // "그런데 어떻게 양산을 펼칠 생각을 했니. 친구야?"

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Ending1_2()
    {
        StartCoroutine(SpeechStartSet(lankyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.resultLines3_2_3, 2f).SetEase(Ease.Linear);    // "곰이 나타났을 때는 절대 등을 보이며 도망가서는 안되고,"

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Ending1_3()
    {
        SpeechRepeatSet(lankyBox);

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.resultLines3_2_4, 2f).SetEase(Ease.Linear);      // "곰보다 더 커보이게끔 우산 같은 걸 펼치라고 배운 적이 있거든."

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Ending1_4()
    {
        StartCoroutine(SpeechStartSet(fattyBox));

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.resultLines3_2_5, 2f).SetEase(Ease.Linear);    // "너는 정말 똑똑한 친구구나. 네가 정말 자랑스러워!"

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Ending1_5()
    {
        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.resultLines3_2_6, 1.5f).SetEase(Ease.Linear);     // "둘은 무사히 여행을 마치고 더욱 절친한 친구가 되었답니다."

        yield return new WaitForSeconds(1.6f);

        StartCoroutine(NarrationEndSet());
    }


    public void InputButton()
    {
        if (GameManager.Instance.inputAllowed == false)
            return;

        if (_statusCompleted == false)
        {
            CompleteAnimation(status);
        }
        else
        {
            if ((status == 4 && GameManager.Instance.ending == 0) || status >= 5)
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
        
        if (GameManager.Instance.ending == 0)
        {
            switch (status)
            {
                case 0:
                    narrationBox.SetActive(true);
                    narrationText.text = GameManager.Instance.resultLines2_2_5;    // "곰이 돌아간 후에 홀쭉이는 다시 나무 밑으로 내려왔어요."
                    break;
                case 1:
                    narrationBox.SetActive(true);
                    narrationText.text = GameManager.Instance.resultLines2_2_6;    // "통통이도 다시 일어났답니다."
                    break;
                case 2:
                    speechBox.SetActive(true);
                    lankyBox.SetActive(true);
                    fattyBox.SetActive(false);
                    speechText.text = GameManager.Instance.resultLines3_1_1;    // "그런데 친구야. 아까 곰이 너한테 뭐라고 속삭이는 것 같던데."
                    break;
                case 3:
                    speechBox.SetActive(true);
                    lankyBox.SetActive(true);
                    fattyBox.SetActive(false);
                    speechText.text = GameManager.Instance.resultLines3_1_2;    // "뭐라고 그랬니?"
                    break;
                case 4:
                    speechBox.SetActive(true);
                    lankyBox.SetActive(false);
                    fattyBox.SetActive(true);
                    speechText.text = GameManager.Instance.resultLines3_1_3;    // "위기에 처했을 때 혼자만 살겠다고 도망가는 녀석은 친구가 아니라더군."
                    break;
            }
        }
        else
        {
            switch (status)
            {
                case 0:
                    speechBox.SetActive(true);
                    lankyBox.SetActive(true);
                    fattyBox.SetActive(false);
                    speechText.text = GameManager.Instance.resultLines3_2_1;    // "정말 놀랬다니까. 휴우. 천만다행이야."
                    break;
                case 1:
                    speechBox.SetActive(true);
                    lankyBox.SetActive(false);
                    fattyBox.SetActive(true);
                    speechText.text = GameManager.Instance.resultLines3_2_2;    // "그런데 어떻게 양산을 펼칠 생각을 했니. 친구야?"
                    break;
                case 2:
                    speechBox.SetActive(true);
                    lankyBox.SetActive(true);
                    fattyBox.SetActive(false);
                    speechText.text = GameManager.Instance.resultLines3_2_3;    // "곰이 나타났을 때는 절대 등을 보이며 도망가서는 안되고,"
                    break;
                case 3:
                    speechBox.SetActive(true);
                    lankyBox.SetActive(true);
                    fattyBox.SetActive(false);
                    speechText.text = GameManager.Instance.resultLines3_2_4;    // "곰보다 더 커보이게끔 우산 같은 걸 펼치라고 배운 적이 있거든."
                    break;
                case 4:
                    speechBox.SetActive(true);
                    lankyBox.SetActive(false);
                    fattyBox.SetActive(true);
                    speechText.text = GameManager.Instance.resultLines3_2_5;    // "너는 정말 똑똑한 친구구나. 네가 정말 자랑스러워!"
                    break;
                case 5:
                    narrationBox.SetActive(true);
                    narrationText.text = GameManager.Instance.resultLines3_2_6;    // "둘은 무사히 여행을 마치고 더욱 절친한 친구가 되었답니다."
                    break;
            }
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

        if (characterBox == lankyBox)
            characterBox.transform.DOScale(lankyBoxSize, 0.2f);
        else if (characterBox == fattyBox)
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
        DOTween.KillAll();

        CutChange();
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

        StopWalk(lanky);
        StopWalk(fatty);
    }

    void Walk(GameObject man, Vector3 targetPos)
    {
        if (man == lanky)
            lankyAnimator.SetBool("walk", true);
        else if (man == fatty)
            fattyAnimator.SetBool("walk", true);

        man.transform.DOMove(targetPos, 9f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() => StopWalk(man));
    }

    void StopWalk(GameObject man)
    {
        if (_sceneFinished)
            return;

        if (man == lanky)
            lankyAnimator.SetBool("walk", false);
        else if (man == fatty)
            fattyAnimator.SetBool("walk", false);
    }

    public void RestartGame(bool _main)
    {
        if (_main)
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
        SceneManager.LoadScene("Page03");
    }
}
