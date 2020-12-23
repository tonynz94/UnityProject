using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{

    //크기가 2인 배열(Bgm, Effect) 두가지를 담을 수 있음.
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    //한번 사용된 소리는 캐싱 역할 다시 사용을 위한 딕셔너리 
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    
    //mp3 player => AudioSource
    //mp3 음원   => AudioClip
    //관객 귀      => AudioListener
    public void Init()
    {
        //게임씬에서 @Sound이름을 가진 오브젝트가 있는지 확인한다.
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            //없으면 새로 생성해 준다.
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            //오디오 두개를 생성해준다.
            //1. BGM를 플레이 시킬 오디오.
            //2. 효과음을 플레이 시킬 오디오.
            string[] soundName = System.Enum.GetNames(typeof(Define.Sound));
            for(int i = 0; i < soundName.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundName[i] };
                //go가 가지고 있는 AudioSource 컴포넌트를 반환
                _audioSources[i] = go.GetOrAddComponent<AudioSource>(); 
                go.transform.parent = root.transform;
            }
            //BGM을 위한 AudioSource는 반복을 체크해준다. 
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

    //오디오를 Play해주는 함수
    //매개변수(플레이를 원하는 효과음의 경로, BGM인지 Effect인지, 볼륨, 속도)
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float volume = 1.0f, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, volume, pitch);
    }

    //매개변수(audioClip, BGM인지 Effect인지, 볼륨, 속도)
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float volume = 1.0f ,float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        //True -> 실행시켜주는 오디오가 BGM일때 
        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

            //다른 Bgm이 플레이 되고 있으면(사실 없어도 되지만 혹시 모르니 안전한 코드로 작성)
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); //기존에 있는것을 멈춰주고
            }
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;  //새로운 클립을 넣어준 후
            audioSource.Play(); //실행해준다.
        }
        //실행시켜주는 오디오가 Effect일때 
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    //최초 사용되는 효과음은 캐싱되며
    //두번 이상 사용되는 효과음일 경우 캐싱이 되어있는것을 가져와 빠른 접근을 해주는 함수입니다. 
    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm) 
            audioClip = Managers.Resource.Load<AudioClip>(path);
        else
        {   
            //소리의 빠른 접근을 위해 _audioClip 딕셔너리에 있는지 확인해줍니다.
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                //딕셔너리에 없다면(최초 사용) 오디오를 가져와 해당 딕셔너리에 추가시켜 줍니다.
                audioClip = Managers.Resource.Load<AudioClip>(path); 
                _audioClips.Add(path, audioClip);   //캐싱에 추가
            }
        }
        if (audioClip == null)
            Debug.Log("Audio Clip Missing! ");

        return audioClip;
    }
}
