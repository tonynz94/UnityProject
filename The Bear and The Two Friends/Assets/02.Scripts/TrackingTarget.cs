using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackingTarget : MonoBehaviour, ITrackableEventHandler {

    // 트래킹 이벤트를 등록할 클래스
    private TrackableBehaviour track;

    // 활성화하거나 비활성화할 RotatePlanet 스크립트를 저장할 배열
    [SerializeField]
    private RotatePlanet[] scripts;

    private void Start()
    {
        // 모든 RotatePlanet 스크립트를 검색해 저장
        scripts = GameObject.FindObjectsOfType<RotatePlanet>();

        track = GetComponent<TrackableBehaviour>();
        if (track)
        {
            // 트래킹 이벤트가 발생하면 이 클래스로 이벤트 전달받을 수 있게 등록
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
            ScriptEnable(true);
        }
        else
        {
            ScriptEnable(false);
        }
    }

    // 스크립트의 활성화/비활성화 상태를 결정
    void ScriptEnable(bool _enabled)
    {
        foreach (var script in scripts)
        {
            script.enabled = _enabled;
        }
    }

}
