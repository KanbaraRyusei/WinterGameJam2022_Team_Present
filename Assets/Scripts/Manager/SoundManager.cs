using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// �Q�[�����ł�BGM�ASE���Ǘ�����N���X
/// �C���X�y�N�^�[����f�[�^��o�^���A
/// ���ꂼ��N���b�v�܂��͖��O�w��ŉ���炷
/// �܂��A�o�^���ꂽ�f�[�^�����Ƃɉ��ʂ⃋�[�v���邩�������Őݒ肳���
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    #region Private Members

    [SerializeField]
    [Header("BGM�̃N���b�v")]
    private AudioData[] _bGMClips;

    [SerializeField]
    [Header("SE�̃N���b�v")]
    private AudioData[] _sEClips;

    [SerializeField]
    [Header("�}�X�^�[����")]
    [Range(0, 1)]
    private float _masterVolume = 1f;

    [SerializeField]
    [Header("���y�̉���")]
    [Range(0, 1)]
    private float _bGMVolume = 1f;

    [SerializeField]
    [Header("���ʉ��̉���")]
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
    /// BGM��炷�֐�
    /// �����ɖ炵����BGM��AudioClip��n��
    /// �{�����[���͎w�肪�Ȃ����1f�ɂȂ�
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
            Debug.LogError("BGM��������܂���ł���");
        }
        
    }

    /// <summary>
    /// BGM��炷�֐�
    /// �����ɖ炵����BGM�̖��O��n��
    /// �{�����[���͎w�肪�Ȃ����1f�ɂȂ�
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
            Debug.LogError("BGM��������܂���ł���");
        }
    }

    /// <summary>
    /// SE��炷�֐�
    /// �����ɖ炵����SE��AudioClip��n��
    /// �{�����[���͎w�肪�Ȃ����1f�ɂȂ�
    /// �����点��悤�ɂȂ��Ă���
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
            seSource.PlayOneShot(clip);// SE���󂢂Ă���AudioSource�ŉ���炷���ƂŁA��������点��
            seSource.loop = data.Loop;
        }
        else
        {
            Debug.LogError("SE��������܂���ł���");
        }
    }

    /// <summary>
    /// SE��炷�֐�
    /// �����ɖ炵����SE�̖��O��n��
    /// �{�����[���͎w�肪�Ȃ����1f�ɂȂ�
    /// �����点��悤�ɂȂ��Ă���
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
            seSource.PlayOneShot(data.Clip);// SE���󂢂Ă���AudioSource�ŉ���炷���ƂŁA��������点��
            seSource.loop = data.Loop;
        }
        else
        {
            Debug.LogError("SE��������܂���ł���");
        }
    }

    #endregion

    #region Volume Setter

    /// <summary>
    /// �}�X�^�[���ʂ�ݒ肷��
    /// </summary>
    /// <param name="value"></param>
    public void MasterVolumeSet(float value)
    {
        _masterVolume = value;
    }

    /// <summary>
    /// BGM�̉��ʂ�ݒ肷��
    /// </summary>
    /// <param name="value"></param>
    public void BGMVolumeSet(float value)
    {
        _bGMVolume = value;
    }

    /// <summary>
    /// SE�̉��ʂ�ݒ肷��
    /// </summary>
    /// <param name="value"></param>
    public void SEVolumeSet(float value)
    {
        _sEVolume = value;
    }

    #endregion

    #region FindData Functions

    /// <summary>
    /// ���O����AudioData��T���֐�
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
    /// AudioClip����Data��T���֐�
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
    /// ���O����AudioData��T���֐�
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
    /// AudioClip����Data��T���֐�
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
    /// �󂢂Ă���AudioSource��T���ĕԂ�(SE�p�̂�)
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

        // ������Βǉ�����
        var newAudioSource = gameObject.AddComponent<AudioSource>();
        _audioSourceSEs.Add(newAudioSource);
        return newAudioSource;
    }

    #endregion
}

[Serializable]
public class AudioData
{
    /// <summary>���O(�C���X�y�N�^�[����o�^)</summary>
    public string Name => _name;

    /// <summary>�N���b�v��ݒ�</summary>
    public AudioClip Clip => _clip;

    /// <summary>�eAudioClip�̉��ʐݒ�</summary>
    public float Volume => _volume;

    /// <summary>���[�v���邩�ݒ�</summary>
    public bool Loop => _loop;

    [SerializeField]
    [Header("���O��o�^")]
    private string _name = "";

    [SerializeField]
    [Header("�N���b�v��ݒ�")]
    private AudioClip _clip = null;

    [SerializeField]
    [Header("���ʐݒ�")]
    private float _volume = 1f;

    [SerializeField]
    [Header("���[�v���邩�ݒ�")]
    private bool _loop = false;
}
