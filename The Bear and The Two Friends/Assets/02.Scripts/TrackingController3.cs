using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingController3 : MonoBehaviour, ITrackableEventHandler
{
    public GameObject blackShadow;

    // 트래킹 이벤트를 등록할 클래스
    private TrackableBehaviour track;

    // 활성화하거나 비활성화할 RotatePlanet 스크립트를 저장할 배열
    [SerializeField]
    private GroundObjects[] scripts;

    public PageThreeScene pageThreeSceneScript;

    private void Start()
    {
        // 모든 GroundObjects 스크립트를 검색해 저장
        scripts = GameObject.FindObjectsOfType<GroundObjects>();

        foreach (var script in scripts)
        {
            script.Disappear();
        }

        track = GetComponent<TrackableBehaviour>();
        if (track)
        {
            // 트래킹 이벤트가 발생하면 이 클래스로 이벤트를 전달받을 수 있게 등록
            track.RegisterTrackableEventHandler(this);
        }
    }

    void OnDisable()
    {
        track.UnregisterTrackableEventHandler(this);
    }

    // 트래킹의 상태가 변경될 때 발생하는 이벤트(Refactor 기능을 이용해 작성한다)
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        // 트래킹이 감지/진행/확장 트래킹 상태로 변경되는 경우를 판단
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
           newStatus == TrackableBehaviour.Status.TRACKED ||
           newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            StartCoroutine(ShowObjects(true));
        }
        else
        {
            ScriptEnable(false);
        }
    }


    // 스크립트의 활성화/비활성화 상태를 결정
    void ScriptEnable(bool _enabled)
    {
        GameManager.Instance.inputAllowed = false;

        if (pageThreeSceneScript._sceneFinished == false)
            blackShadow.SetActive(true);

        foreach (var script in scripts)
        {
            if (_enabled == false)
            {
                script.Disappear();
            }
        }

        if (pageThreeSceneScript != null)
            pageThreeSceneScript.TrackedOut();

    }

    IEnumerator ShowObjects(bool _enabled)
    {
        GameManager.Instance.inputAllowed = true;

        if (pageThreeSceneScript._sceneFinished == false)
            blackShadow.SetActive(false);

        foreach (var script in scripts)
        {
            if (_enabled)
            {
                script.Show();
                yield return new WaitForSeconds(0.02f);
            }
        }

        pageThreeSceneScript.PlayStory(pageThreeSceneScript.status);
    }

}
