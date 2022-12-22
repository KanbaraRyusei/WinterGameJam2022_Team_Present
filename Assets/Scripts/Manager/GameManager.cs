using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    /// <summary>��������</summary>
    public int TimeLimit => _timeLimit;

    /// <summary>���݂̌o�ߎ���</summary>
    public float CurrentTime => _time;

    [SerializeField]
    [Header("��������")]
    private int _timeLimit = 60;

    [SerializeField]
    [Header("�Q�[���N���A�V�[���̖��O")]
    private string _gameClearSceneName = "GameClear";

    [SerializeField]
    [Header("�Q�[���I�[�o�[�V�[���̖��O")]
    private string _gameOverSceneName = "GameOver";

    private float _time = 0f;// ���݂̌o�ߎ���

    /// <summary>�Q�[���N���A���ɌĂ΂��f���Q�[�g �Q�[���N���A���ɉ�������������Ƃ��ɂǂ���</summary>
    public Action OnGameClear;

    /// <summary>�Q�[���I�[�o�[���ɌĂ΂��f���Q�[�g �Q�[���I�[�o�[���ɉ�������������Ƃ��ɂǂ���</summary>
    public Action OnGameOver;

    private void Update()
    {
        _time += Time.deltaTime;
        if(_timeLimit < _time)// �������ԂɂȂ�����
        {
            Debug.Log("LimitTime!!");
            GameClear();
        }
    }

    /// <summary>
    /// �Q�[���N���A���ɌĂ΂��֐�
    /// </summary>
    public void GameClear()
    {
        OnGameClear?.Invoke();
        SceneLoder.LoadScene(_gameClearSceneName);// �Q�[���N���A�V�[���ɑJ��
    }

    /// <summary>
    /// �Q�[���I�[�o�[���ɌĂ΂��֐�
    /// </summary>
    public void GameOver()
    {
        OnGameOver?.Invoke();
        SceneLoder.LoadScene(_gameOverSceneName);// �Q�[���I�[�o�[�V�[���ɑJ��
    }
}
