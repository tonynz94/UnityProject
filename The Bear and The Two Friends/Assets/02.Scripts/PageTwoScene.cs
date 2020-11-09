using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PageTwoScene : MonoBehaviour {

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
    public GameObject bear;

    public Animator lankyAnimator;
    public Animator fattyAnimator;
    public Animator bearAnimator;

    private Vector3 lankyStartingPos;
    private Vector3 fattyStartingPos;
    private Vector3 bearStartingPos;

    private Quaternion lankyNormalRotation;
    private Quaternion fattyNormalRotation;
    private Quaternion bearNormalRotation;

    public GameObject lankyTarget1;
    public GameObject lankyTarget2;
    public GameObject lankyTarget2_1;
    public GameObject lankyTarget3;
    public GameObject lankyTarget4;
    
    public GameObject fattyTarget1;
    public GameObject fattyTarget2;

    public GameObject bearTarget1;
    public GameObject bearTarget2;

    public GameObject umbrella;

    private void Awake()
    {
        speechBoxSize = speechBox.transform.localScale;
        lankyBoxSize = lankyBox.transform.localScale;
        fattyBoxSize = fattyBox.transform.localScale;
        selectableBoxSize = selectableBox.transform.localScale;

        lankyStartingPos = lanky.transform.position;
        fattyStartingPos = fatty.transform.position;

        lankyNormalRotation = lanky.transform.rotation;
        fattyNormalRotation = fatty.transform.rotation;
    }

    void Start()
    {
        bear.SetActive(true);
        bearStartingPos = bear.transform.position;
        bearNormalRotation = bear.transform.rotation;
        bear.SetActive(false);

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
                GameManager.Instance.inputAllowed = false;
                StartCoroutine(Cut0());     // 걷기
                break;
            case 1:
                GameManager.Instance.inputAllowed = true;
                StartCoroutine(Cut1());
                break;
            case 2:
                StartCoroutine(Cut2());
                break;
            case 3:
                StartCoroutine(Cut3());
                break;
            case 4:
                StartCoroutine(Cut4());     // "그래, 그러자꾸나."
                break;
            case 5:
                GameManager.Instance.inputAllowed = false;
                StartCoroutine(Cut5());     // 곰 등장
                break;
            case 6:
                GameManager.Instance.inputAllowed = true;
                StartCoroutine(Cut6());
                break;
            case 7:
                GameManager.Instance.inputAllowed = false;
                StartCoroutine(Cut7());     // 선택지
                break;
            case 8:
                switch (selectedNumber)
                {
                    case 0:
                        StopAllCoroutines();
                        StartCoroutine(Select0());      // 곰의 공격
                        break;
                    case 1:
                        StopAllCoroutines();
                        StartCoroutine(Select1());      // 나무 타기

                        GameManager.Instance.ending = 0;
                        break;
                    case 2:
                        StopAllCoroutines();
                        StartCoroutine(Select2());      // 양산 펴기

                        GameManager.Instance.ending = 1;
                        break;
                    case 3:
                        StopAllCoroutines();
                        GameManager.Instance.inputAllowed = true;
                        StartCoroutine(Select3());      // "이봐, 곰 친구. 우리는 조용히 여행 중이니까 길을 비켜주지 않겠니?"
                        break;
                }
                break;
            case 9:
                switch (selectedNumber)
                {
                    case 0:
                        GameManager.Instance.inputAllowed = true;
                        StartCoroutine(Select0_1());    // "두 친구는 무시무시한 곰에게 목숨을 잃고 말았어요."
                        break;
                    case 1:
                        GameManager.Instance.inputAllowed = true;
                        StartCoroutine(Select1_1());    // "달리기가 느린 통통이는 도망칠 수가 없었어요."
                        break;
                    case 2:
                        GameManager.Instance.inputAllowed = true;
                        StartCoroutine(Select2_1());    // "양산을 펼치자 곰은 놀라서 돌아가버렸어요."
                        break;
                    case 3:
                        GameManager.Instance.inputAllowed = true;
                        StartCoroutine(Select3_1());      // "하지만 곰은 사람 말을 알아듣지 못하는군요."
                        break;
                }
                break;
            case 10:
                switch (selectedNumber)
                {
                    case 0:
                        GameManager.Instance.inputAllowed = true;
                        StartCoroutine(Select0_2());    // "야생곰은 정말 무서운 동물이랍니다."
                        break;
                    case 1:
                        GameManager.Instance.inputAllowed = false;
                        StartCoroutine(Select1_2());    // 죽은 척하기
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case 11:
                switch (selectedNumber)
                {
                    case 1:
                        GameManager.Instance.inputAllowed = true;
                        StartCoroutine(Select1_3());    // "그래서 바닥에 엎드려 죽은 척을 했지요."
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case 12:
                switch (selectedNumber)
                {
                    case 1:
                        GameManager.Instance.inputAllowed = false;
                        StartCoroutine(Select1_4());    // 곰 접근
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case 13:
                switch (selectedNumber)
                {
                    case 1:
                        GameManager.Instance.inputAllowed = true;
                        StartCoroutine(Select1_5());    // "곰이 통통이에게 다가왔어요."
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case 14:
                switch (selectedNumber)
                {
                    case 1:
                        GameManager.Instance.inputAllowed = false;
                        StartCoroutine(Select1_6());    // 냄새 맡고 가기
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case 15:
                switch (selectedNumber)
                {
                    case 1:
                        GameManager.Instance.inputAllowed = true;
                        StartCoroutine(Select1_7());    // "그리고는 냄새를 킁킁 맡더니 그냥 가버렸답니다."
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
        }
    }

    IEnumerator Cut0()
    {
        lanky.transform.position = lankyStartingPos;
        fatty.transform.position = fattyStartingPos;

        Walk(lanky, lankyTarget1.transform.position);
        Walk(fatty, fattyTarget1.transform.position);

        yield return new WaitForSeconds(3.5f);
        
        status++;
        PlayStory(status);
    }

    IEnumerator Cut1()
    {
        StartCoroutine(SpeechStartSet(fattyBox));
        yield return new WaitForSeconds(0.5f);
        speechText.DOText(GameManager.Instance.lines2_1, 2f).SetEase(Ease.Linear);      // "이제 슬슬 배가 고픈데. 너는 배 안고프니, 친구야?"
        yield return new WaitForSeconds(2.1f);
        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Cut2()
    {
        StartCoroutine(SpeechStartSet(lankyBox));
        yield return new WaitForSeconds(0.5f);
        speechText.DOText(GameManager.Instance.lines2_2, 2f).SetEase(Ease.Linear);    // "나도 막 배가 고프던 참이야."
        yield return new WaitForSeconds(2.1f);
        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Cut3()
    {
        SpeechRepeatSet(lankyBox);
        yield return new WaitForSeconds(0.5f);
        speechText.DOText(GameManager.Instance.lines2_3, 2f).SetEase(Ease.Linear);      // "우리 저 큰 나무 밑에 앉아서 챙겨온 음식을 먹자."
        yield return new WaitForSeconds(2.1f);
        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Cut4()
    {
        StartCoroutine(SpeechStartSet(fattyBox));
        yield return new WaitForSeconds(0.5f);
        speechText.DOText(GameManager.Instance.lines2_4, 2f).SetEase(Ease.Linear);      // "그래, 그러자꾸나."
        yield return new WaitForSeconds(2.1f);
        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Cut5()      // 곰 등장
    {
        lanky.transform.position = lankyTarget1.transform.position;
        fatty.transform.position = fattyTarget1.transform.position;
        Walk(lanky, lankyTarget2.transform.position);
        Walk(fatty, fattyTarget2.transform.position);

        bear.SetActive(true);
        bear.transform.position = bearStartingPos;
        bear.transform.localScale = new Vector3(0, 0, 0);
        yield return null;
        bear.GetComponent<GroundObjects>().Show();

        yield return new WaitForSeconds(1f);

        Run(bear, bearTarget1.transform.position);

        yield return new WaitForSeconds(3f);

        status++;
        PlayStory(status);
    }

    IEnumerator Cut6()
    {
        SpeechRepeatSet(lankyBox);

        yield return new WaitForSeconds(0.5f);

        speechText.DOText(GameManager.Instance.lines2_6, 2f).SetEase(Ease.Linear);      // "으악... 고, 고, 곰이다!!"

        yield return new WaitForSeconds(2.1f);

        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Cut7()      // 선택지
    {
        selectableBox.SetActive(true);

        selectableBox.transform.localScale = new Vector3(0, 0, 0);
        selectableBox.transform.DOScale(speechBoxSize, 0.2f);
        selectableBox.transform.DOScale(selectableBoxSize, 0.2f);

        yield return new WaitForSeconds(0.2f);

        selectableBox.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.3f);
    }


    public void Select(int selectedNum)
    {
        selectedNumber = selectedNum;
        
        CutChange();
        status++;
        PlayStory(status);
    }

    IEnumerator Select0()       // 곰과 싸운다
    {
        lanky.transform.rotation = lankyNormalRotation;
        fatty.transform.rotation = fattyNormalRotation;

        lanky.transform.position = lankyTarget2.transform.position;
        fatty.transform.position = fattyTarget2.transform.position;

        lankyAnimator.Play("idle", -1, 0);
        fattyAnimator.Play("idle", -1, 0);
        bearAnimator.Play("idle", -1, 0);

        lanky.transform.position = lankyTarget2.transform.position;
        fatty.transform.position = fattyTarget2.transform.position;

        lanky.transform.rotation = lankyNormalRotation;
        fatty.transform.rotation = fattyNormalRotation;

        Walk(lanky, lankyTarget2_1.transform.position);
        Walk(fatty, fattyTarget2.transform.position);

        bear.transform.position = bearTarget1.transform.position;
        Walk(bear, bearTarget2.transform.position);

        yield return new WaitForSeconds(3f);

        lankyAnimator.SetTrigger("leftPunch");

        yield return new WaitForSeconds(0.5f);

        lankyAnimator.SetTrigger("rightPunch");

        yield return new WaitForSeconds(0.5f);
        
        lankyAnimator.SetTrigger("leftPunch");

        yield return new WaitForSeconds(1f);

        bearAnimator.SetTrigger("rightPunch");

        yield return new WaitForSeconds(0.8f);

        fattyAnimator.SetTrigger("die");

        yield return new WaitForSeconds(0.1f);

        bearAnimator.SetTrigger("leftPunch");

        yield return new WaitForSeconds(1f);

        lankyAnimator.SetTrigger("die");

        yield return new WaitForSeconds(1.5f);

        status++;
        PlayStory(status);
    }

    IEnumerator Select0_1()
    {
        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.resultLines2_1_1, 1.5f).SetEase(Ease.Linear);     // "두 친구는 무시무시한 곰에게 목숨을 잃고 말았어요."

        yield return new WaitForSeconds(1.6f);

        StartCoroutine(NarrationEndSet());
    }

    IEnumerator Select0_2()
    {
        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.resultLines2_1_2, 1.5f).SetEase(Ease.Linear);     // "야생곰은 정말 무서운 동물이랍니다."

        yield return new WaitForSeconds(1.6f);

        StartCoroutine(NarrationEndSet());
    }

    IEnumerator Select1()   // 혼자서 잽싸게 나무 위로 도망간다
    {
        lanky.transform.rotation = lankyNormalRotation;
        fatty.transform.rotation = fattyNormalRotation;

        lanky.transform.position = lankyTarget2.transform.position;
        fatty.transform.position = fattyTarget2.transform.position;

        lankyAnimator.Play("idle", -1, 0);
        fattyAnimator.Play("idle", -1, 0);
        bearAnimator.Play("idle", -1, 0);

        lanky.transform.position = lankyTarget2.transform.position;
        fatty.transform.position = fattyTarget2.transform.position;

        lanky.transform.rotation = lankyNormalRotation;
        fatty.transform.rotation = fattyNormalRotation;

        yield return null;

        lanky.transform.LookAt(lankyTarget3.transform.position);
        Run(lanky, lankyTarget3.transform.position);

        yield return new WaitForSeconds(2.5f);

        lankyAnimator.SetTrigger("climb");
        lanky.transform.DOMove(lankyTarget4.transform.position, 1f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(0.25f);
        lankyAnimator.SetTrigger("climb");

        yield return new WaitForSeconds(0.25f);
        lankyAnimator.SetTrigger("climb");

        yield return new WaitForSeconds(0.25f);
        lankyAnimator.SetTrigger("climb");

        yield return new WaitForSeconds(0.25f);
        lankyAnimator.SetTrigger("climb");

        status++;
        PlayStory(status);
    }

    IEnumerator Select1_1()
    {
        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.resultLines2_2_1, 1.5f).SetEase(Ease.Linear);     // "달리기가 느린 통통이는 도망칠 수가 없었어요."

        yield return new WaitForSeconds(1.6f);

        StartCoroutine(NarrationEndSet());
    }

    IEnumerator Select1_2()     // 죽은 척하기
    {
        fatty.transform.rotation = fattyNormalRotation;
        fatty.transform.position = fattyTarget2.transform.position;
        
        fattyAnimator.Play("idle", -1, 0);
        bearAnimator.Play("idle", -1, 0);
        
        fatty.transform.position = fattyTarget2.transform.position;
        fatty.transform.rotation = fattyNormalRotation;
        
        yield return new WaitForSeconds(0.5f);

        fattyAnimator.SetTrigger("die");

        yield return new WaitForSeconds(1f);

        status++;
        PlayStory(status);
    }

    IEnumerator Select1_3()
    {
        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.resultLines2_2_2, 1.5f).SetEase(Ease.Linear);     // "그래서 바닥에 엎드려 죽은 척을 했지요."

        yield return new WaitForSeconds(1.8f);

        StartCoroutine(NarrationEndSet());
    }

    IEnumerator Select1_4()
    {
        bear.transform.position = bearTarget1.transform.position;

        yield return new WaitForSeconds(0.6f);

        Walk(bear, bearTarget2.transform.position);

        yield return new WaitForSeconds(3f);

        status++;
        PlayStory(status);
    }

    IEnumerator Select1_5()     
    {
        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.resultLines2_2_3, 1.5f).SetEase(Ease.Linear);     // "곰이 통통이에게 다가왔어요."

        yield return new WaitForSeconds(1.6f);

        StartCoroutine(NarrationEndSet());
    }

    IEnumerator Select1_6()     // 냄새 맡고 가기
    {
        bear.transform.position = bearTarget2.transform.position;
        bear.transform.rotation = bearNormalRotation;
        Walk(bear, bearTarget2.transform.position);

        yield return new WaitForSeconds(1f);

        bearAnimator.SetTrigger("smell");

        yield return new WaitForSeconds(3f);

        bearAnimator.Play("idle", -1, 0);

        bearAnimator.SetTrigger("turn");

        yield return new WaitForSeconds(2.1f);

        bear.transform.LookAt(bearStartingPos);

        Run(bear, bearStartingPos);

        yield return new WaitForSeconds(3f);

        bear.SetActive(false);

        status++;
        PlayStory(status);
    }

    IEnumerator Select1_7()
    {
        NarrationStartSet();

        narrationText.DOText(GameManager.Instance.resultLines2_2_4, 1.5f).SetEase(Ease.Linear);     // "그리고는 냄새를 킁킁 맡더니 그냥 가버렸답니다."

        yield return new WaitForSeconds(1.6f);

        StartCoroutine(NarrationEndSet());
    }

    IEnumerator Select2()   // 양산 펼치니까 도망가기
    {
        umbrella.SetActive(true);
        umbrella.transform.localScale = new Vector3(0, 0, 0);

        yield return null;

        umbrella.GetComponent<GroundObjects>().Show();

        yield return new WaitForSeconds(1f);

        bearAnimator.SetTrigger("turn");

        yield return new WaitForSeconds(1.5f);

        bear.transform.LookAt(bearStartingPos);

        Run(bear, bearStartingPos);

        yield return new WaitForSeconds(3f);

        bear.SetActive(false);

        status++;
        PlayStory(status);
    }
    
    IEnumerator Select2_1()
    {
        NarrationStartSet();
        narrationText.DOText(GameManager.Instance.resultLines2_3, 1.5f).SetEase(Ease.Linear);     // "양산을 펼치자 곰은 놀라서 돌아가버렸어요."
        yield return new WaitForSeconds(1.6f);
        StartCoroutine(NarrationEndSet());
    }

    IEnumerator Select3()
    {
        StartCoroutine(SpeechStartSet(lankyBox));
        yield return new WaitForSeconds(0.5f);
        speechText.DOText(GameManager.Instance.resultLines2_4_1, 2f).SetEase(Ease.Linear);    // "이봐, 곰 친구. 우리는 조용히 여행 중이니까 길을 비켜주지 않겠니?"
        yield return new WaitForSeconds(2.1f);
        StartCoroutine(SpeechEndSet());
    }

    IEnumerator Select3_1()
    {
        NarrationStartSet();
        narrationText.DOText(GameManager.Instance.resultLines2_4_2, 1.5f).SetEase(Ease.Linear);     // "하지만 곰은 사람 말을 알아듣지 못하는군요."
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
            if (status == 10 && selectedNumber == 0)
            {
                // 게임 오버
                _sceneFinished = true;

                endingBox.SetActive(true);

                TrackedOut();
                lanky.SetActive(false);
                fatty.SetActive(false);
            }
            else if (status == 9 && selectedNumber == 3)
            {
                // 다시 질문
                CutChange();
                status = 7;
                PlayStory(status);
            }
            else if((status == 15 && selectedNumber == 1) || (status == 9 && selectedNumber == 2))
            {
                // 씬 전환
                _sceneFinished = true;
                fader.DOFade(1, 0.5f).OnComplete(ChangeScene);
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

        switch (status)
        {
            case 0:
                // 걷기
                break;
            case 1:
                speechBox.SetActive(true);
                lankyBox.SetActive(false);
                fattyBox.SetActive(true);
                speechText.text = GameManager.Instance.lines2_1;    // "이제 슬슬 배가 고픈데. 너는 배 안고프니, 친구야?"
                break;
            case 2:
                speechBox.SetActive(true);
                lankyBox.SetActive(true);
                fattyBox.SetActive(false);
                speechText.text = GameManager.Instance.lines2_2;    // "나도 막 배가 고프던 참이야."
                break;
            case 3:
                speechBox.SetActive(true);
                lankyBox.SetActive(true);
                fattyBox.SetActive(false);
                speechText.text = GameManager.Instance.lines2_3;    // "우리 저 큰 나무 밑에 앉아서 챙겨온 음식을 먹자."
                break;
            case 4:
                speechBox.SetActive(true);
                lankyBox.SetActive(false);
                fattyBox.SetActive(true);
                speechText.text = GameManager.Instance.lines2_4;    // "그래, 그러자꾸나."
                break;
            case 5:
                // 곰 등장
                break;
            case 6:
                speechBox.SetActive(true);
                lankyBox.SetActive(true);
                fattyBox.SetActive(false);
                speechText.text = GameManager.Instance.lines2_6;    // "으악... 고, 고, 곰이다!!"
                break;
            case 7:
                // 선택지
                break;
            case 8:
                switch (selectedNumber)
                {
                    case 0:
                        // 곰과 결투
                        break;
                    case 1:
                        // 나무 오르기
                        break;
                    case 2:
                        // 양산 펴기
                        break;
                    case 3:
                        speechBox.SetActive(true);
                        lankyBox.SetActive(true);
                        fattyBox.SetActive(false);
                        speechText.text = GameManager.Instance.resultLines2_4_1;    // "이봐, 곰 친구. 우리는 조용히 여행 중이니까 길을 비켜주지 않겠니?"
                        break;
                }
                break;
            case 9:
                switch (selectedNumber)
                {
                    case 0:
                        narrationBox.SetActive(true);
                        narrationText.text = GameManager.Instance.resultLines2_1_1;    // "두 친구는 무시무시한 곰에게 목숨을 잃고 말았어요."
                        break;
                    case 1:
                        narrationBox.SetActive(true);
                        narrationText.text = GameManager.Instance.resultLines2_2_1;    // "달리기가 느린 통통이는 도망칠 수가 없었어요."
                        break;
                    case 2:
                        narrationBox.SetActive(true);
                        narrationText.text = GameManager.Instance.resultLines2_3;    // "양산을 펼치자 곰은 놀라서 돌아가버렸어요."
                        break;
                    case 3:
                        narrationBox.SetActive(true);
                        narrationText.text = GameManager.Instance.resultLines2_4_2;    // "하지만 곰은 사람 말을 알아듣지 못하는군요."
                        break;
                }
                break;
            case 10:
                switch (selectedNumber)
                {
                    case 0:
                        narrationBox.SetActive(true);
                        narrationText.text = GameManager.Instance.resultLines2_1_2;    // "야생곰은 정말 무서운 동물이랍니다."
                        break;
                    case 1:
                        // 죽은 척하기
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case 11:
                switch (selectedNumber)
                {
                    case 1:
                        narrationBox.SetActive(true);
                        narrationText.text = GameManager.Instance.resultLines2_2_2;    // "그래서 바닥에 엎드려 죽은 척을 했지요."
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case 12:
                switch (selectedNumber)
                {
                    case 1:
                        // 곰 접근
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case 13:
                switch (selectedNumber)
                {
                    case 1:
                        narrationBox.SetActive(true);
                        narrationText.text = GameManager.Instance.resultLines2_2_3;    // "곰이 통통이에게 다가왔어요."
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case 14:
                switch (selectedNumber)
                {
                    case 1:
                        // 냄새 맡고 가기
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case 15:
                switch (selectedNumber)
                {
                    case 1:
                        narrationBox.SetActive(true);
                        narrationText.text = GameManager.Instance.resultLines2_2_4;    // "그리고는 냄새를 킁킁 맡더니 그냥 가버렸답니다."
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
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
        if(bear.activeSelf)
            StopRun(bear);
    }

    void Walk(GameObject man, Vector3 targetPos)
    {
        if (man == lanky)
            lankyAnimator.SetBool("walk", true);
        else if (man == fatty)
            fattyAnimator.SetBool("walk", true);
        else
        {
            if (bear.activeSelf)
                bearAnimator.SetBool("walk", true);
        }
        
        man.transform.DOMove(targetPos, 9f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(()=>StopWalk(man));
    }

    void StopWalk(GameObject man)
    {
        if (_sceneFinished)
            return;

        if (man == lanky)
            lankyAnimator.SetBool("walk", false);
        else if (man == fatty)
            fattyAnimator.SetBool("walk", false);
        else
        {
            if (bear.activeSelf)
                bearAnimator.SetBool("walk", false);
        }
    }

    void Run(GameObject man, Vector3 targetPos)
    {
        if (man == lanky)
            lankyAnimator.SetBool("run", true);
        else
        {
            if (bear.activeSelf)
                bearAnimator.SetBool("run", true);
        }

        man.transform.DOMove(targetPos, 16f).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() => StopRun(man));
    }

    void StopRun(GameObject man)
    {
        if (_sceneFinished)
            return;
        
        if (man == lanky)
            lankyAnimator.SetBool("run", false);
        else
        {
            if (bear.activeSelf)
                bearAnimator.SetBool("run", false);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("Page03");

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
        SceneManager.LoadScene("Page02");
    }
}
