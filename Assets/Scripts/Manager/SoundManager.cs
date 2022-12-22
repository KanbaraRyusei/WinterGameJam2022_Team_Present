using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    [Header("BGM�̃N���b�v")]
    private AudioData[] _bGMClips;

    [SerializeField]
    [Header("SE�̃N���b�v")]
    private AudioData[] _sEClips;

    [SerializeField]
    [Header("�}�X�^�[����")]
    [Range(0, 1)]
    private float _masterVolume;

    [SerializeField]
    [Header("���y�̉���")]
    [Range(0, 1)]
    private float _bGMVolume;

    [SerializeField]
    [Header("���ʉ��̉���")]
    [Range(0, 1)]
    private float _sEVolume;

    private AudioSource _audioSourceBGM;
    private List<AudioSource> _audioSourceSEs;

    protected override void Awake()
    {
        base.Awake();
        _audioSourceSEs = new List<AudioSource>(_sEClips.Length);
        for (int i = 0; i < _audioSourceSEs.Count; i++)
        {
            _audioSourceSEs[i] = gameObject.AddComponent<AudioSource>();
        }
    }

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
        }
        else
        {
            Debug.LogError("SE��������܂���ł���");
        }
    }

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
}

[Serializable]
public class AudioData
{

    /// <summary>���O(�C���X�y�N�^�[����o�^)</summary>
    public string Name = "";

    public AudioClip Clip = null;

    /// <summary>�eAudioClip�̉��ʐݒ�</summary>
    public float Volume = 1f;
}
