using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ゲーム内でのBGM、SEを管理するクラス
/// インスペクターからデータを登録し、
/// それぞれクリップまたは名前指定で音を鳴らす
/// また、登録されたデータをもとに音量やループするかが自動で設定される
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    #region Private Members

    [SerializeField]
    [Header("BGMのクリップ")]
    private AudioData[] _bGMClips;

    [SerializeField]
    [Header("SEのクリップ")]
    private AudioData[] _sEClips;

    [SerializeField]
    [Header("マスター音量")]
    [Range(0, 1)]
    private float _masterVolume = 1f;

    [SerializeField]
    [Header("音楽の音量")]
    [Range(0, 1)]
    private float _bGMVolume = 1f;

    [SerializeField]
    [Header("効果音の音量")]
    [Range(0, 1)]
    private float _sEVolume = 1f;

    private AudioSource _audioSourceBGM;
    private List<AudioSource> _audioSourceSEs;

    #endregion

    #region Unity Events

    protected override void Awake()
    {
        base.Awake();
        _audioSourceSEs = new List<AudioSource>(_sEClips.Length);
        for (int i = 0; i < _audioSourceSEs.Count; i++)
        {
            _audioSourceSEs[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    #endregion

    #region UsePlayFunctions

    /// <summary>
    /// BGMを鳴らす関数
    /// 引数に鳴らしたいBGMのAudioClipを渡す
    /// ボリュームは指定がなければ1fになる
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="volume"></param>
    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        _audioSourceBGM.Stop();
        var data = FindBGMClip(clip);

        if(data != null)
        {
            _audioSourceBGM.volume = volume * _masterVolume * _bGMVolume * data.Volume;
            _audioSourceBGM.PlayOneShot(clip);
            _audioSourceBGM.loop = data.Loop;
        }
        else
        {
            Debug.LogError("BGMが見つかりませんでした");
        }
        
    }

    /// <summary>
    /// BGMを鳴らす関数
    /// 引数に鳴らしたいBGMの名前を渡す
    /// ボリュームは指定がなければ1fになる
    /// </summary>
    /// <param name="name"></param>
    /// <param name="volume"></param>
    public void PlayBGM(string name, float volume = 1f)
    {
        _audioSourceBGM.Stop();
        var data = FindBGMClip(name);
        _audioSourceBGM.volume = volume * _masterVolume * _bGMVolume * data.Volume;

        if(data != null)
        {
            _audioSourceBGM.PlayOneShot(data.Clip);
            _audioSourceBGM.loop = data.Loop;
        }
        else
        {
            Debug.LogError("BGMが見つかりませんでした");
        }
    }

    /// <summary>
    /// SEを鳴らす関数
    /// 引数に鳴らしたいSEのAudioClipを渡す
    /// ボリュームは指定がなければ1fになる
    /// 複数鳴らせるようになっている
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="volume"></param>
    public void PlaySE(AudioClip clip, float volume = 1f)
    {
        var seSource = SearchAudioSourceSE();
        var data = FindSEClip(clip);
        
        if(data != null)
        {
            seSource.volume = volume * _masterVolume * _sEVolume * data.Volume;
            seSource.PlayOneShot(clip);// SEが空いているAudioSourceで音を鳴らすことで、複数音を鳴らせる
            seSource.loop = data.Loop;
        }
        else
        {
            Debug.LogError("SEが見つかりませんでした");
        }
    }

    /// <summary>
    /// SEを鳴らす関数
    /// 引数に鳴らしたいSEの名前を渡す
    /// ボリュームは指定がなければ1fになる
    /// 複数鳴らせるようになっている
    /// </summary>
    /// <param name="name"></param>
    /// <param name="volume"></param>
    public void PlaySE(string name, float volume = 1f)
    {
        var seSource = SearchAudioSourceSE();
        var data = FindSEClip(name);

        if(data != null)
        {
            seSource.volume = volume * _masterVolume * _sEVolume * data.Volume;
            seSource.PlayOneShot(data.Clip);// SEが空いているAudioSourceで音を鳴らすことで、複数音を鳴らせる
            seSource.loop = data.Loop;
        }
        else
        {
            Debug.LogError("SEが見つかりませんでした");
        }
    }

    #endregion

    #region Volume Setter

    /// <summary>
    /// マスター音量を設定する
    /// </summary>
    /// <param name="value"></param>
    public void MasterVolumeSet(float value)
    {
        _masterVolume = value;
    }

    /// <summary>
    /// BGMの音量を設定する
    /// </summary>
    /// <param name="value"></param>
    public void BGMVolumeSet(float value)
    {
        _bGMVolume = value;
    }

    /// <summary>
    /// SEの音量を設定する
    /// </summary>
    /// <param name="value"></param>
    public void SEVolumeSet(float value)
    {
        _sEVolume = value;
    }

    #endregion

    #region FindData Functions

    /// <summary>
    /// 名前からAudioDataを探す関数
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private AudioData FindBGMClip(string name)
    {
        foreach(var c in _bGMClips)
        {
            if(c.Name == name)
            {
                return c;
            }
        }

        return null;
    }

    /// <summary>
    /// AudioClipからDataを探す関数
    /// </summary>
    /// <param name="clip"></param>
    /// <returns></returns>
    private AudioData FindBGMClip(AudioClip clip)
    {
        foreach(var c in _bGMClips)
        {
            if(c.Clip == clip)
            {
                return c;
            }
        }

        return null;
    }

    /// <summary>
    /// 名前からAudioDataを探す関数
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private AudioData FindSEClip(string name)
    {
        foreach (var c in _sEClips)
        {
            if (c.Name == name)
            {
                return c;
            }
        }

        return null;
    }

    /// <summary>
    /// AudioClipからDataを探す関数
    /// </summary>
    /// <param name="clip"></param>
    /// <returns></returns>
    private AudioData FindSEClip(AudioClip clip)
    {
        foreach (var c in _sEClips)
        {
            if (c.Clip == clip)
            {
                return c;
            }
        }

        return null;
    }

    #endregion

    #region Search AudioSource

    /// <summary>
    /// 空いているAudioSourceを探して返す(SE用のみ)
    /// </summary>
    private AudioSource SearchAudioSourceSE()
    {
        for (int i = 0; i < _audioSourceSEs.Count; i++)
        {
            if (!_audioSourceSEs[i].isPlaying)
            {
                return _audioSourceSEs[i];
            }
        }

        // 無ければ追加する
        var newAudioSource = gameObject.AddComponent<AudioSource>();
        _audioSourceSEs.Add(newAudioSource);
        return newAudioSource;
    }

    #endregion
}

[Serializable]
public class AudioData
{
    /// <summary>名前(インスペクターから登録)</summary>
    public string Name => _name;

    /// <summary>クリップを設定</summary>
    public AudioClip Clip => _clip;

    /// <summary>各AudioClipの音量設定</summary>
    public float Volume => _volume;

    /// <summary>ループするか設定</summary>
    public bool Loop => _loop;

    [SerializeField]
    [Header("名前を登録")]
    private string _name = "";

    [SerializeField]
    [Header("クリップを設定")]
    private AudioClip _clip = null;

    [SerializeField]
    [Header("音量設定")]
    private float _volume = 1f;

    [SerializeField]
    [Header("ループするか設定")]
    private bool _loop = false;
}
