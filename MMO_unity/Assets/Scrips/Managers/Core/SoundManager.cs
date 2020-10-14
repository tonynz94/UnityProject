using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{

    //크기가 2인 배열(Bgm, Effect) 두가지
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    //캐싱 역할
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    
    //mp3 player => AudioSource
    //mp3 음원   => AudioClip
    //관객 귀      => AudioListener

    public void Init()
    {
        //게임씬에서 @Sound가 있는지 확인
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            //없으면 새로 생성
            root = new GameObject { name = "@Sound" };
            //영영 해제되지 않음.
            Object.DontDestroyOnLoad(root);

            string[] soundName = System.Enum.GetNames(typeof(Define.Sound));
            for(int i = 0; i < soundName.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundName[i] };
                _audioSources[i] = go.AddComponent<AudioSource>(); //go가 가지고 있는 AudioSource 컴포넌트를 반환
                go.transform.parent = root.transform;
            }
            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }


    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        //loop => BGM
        //path라는 경로에 sounds가 포함 되어 있지 않으면 문자열로 추가.
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            //다른 Bgm이 플레이 되고 있으면(사실 없어도 되지만 혹시 모르니 안전한 코드로 작성)
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); //기존에 있는것을 멈춰주고
            }
            audioSource.pitch = 1.0f;
            audioSource.clip = audioClip;  //새로운 클립을 넣어준 후
            audioSource.Play(); //실행해준다.
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = 1.0f;
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
            //오디오 클림을 찾음.
            AudioClip _audioClip = Managers.Resource.Load<AudioClip>(path);


        }
        else
        {
            
            //만약 딕셔너리에 없다면 (Key , out 반환값)
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path); //가져온 후
                _audioClips.Add(path, audioClip);   //캐싱에 추가
            }
        }
        if (audioClip == null)
        {
            Debug.Log("Audio Clip Missing! ");
        }

        return audioClip;
    }
}
