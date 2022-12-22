using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    /// <summary>制限時間</summary>
    public int TimeLimit => _timeLimit;

    /// <summary>現在の経過時間</summary>
    public float CurrentTime => _time;

    [SerializeField]
    [Header("制限時間")]
    private int _timeLimit = 60;

    [SerializeField]
    [Header("ゲームクリアシーンの名前")]
    private string _gameClearSceneName = "GameClear";

    [SerializeField]
    [Header("ゲームオーバーシーンの名前")]
    private string _gameOverSceneName = "GameOver";

    private float _time = 0f;// 現在の経過時間
    private bool _isTimerStop = true;

    /// <summary>ゲームスタート時に呼ばれるデリゲート ゲームスタート時に何か処理をするときにどうぞ</summary>
    public Action OnGameStart;
    /// <summary>ゲームクリア時に呼ばれるデリゲート ゲームクリア時に何か処理をするときにどうぞ</summary>
    public Action OnGameClear;

    /// <summary>ゲームオーバー時に呼ばれるデリゲート ゲームオーバー時に何か処理をするときにどうぞ</summary>
    public Action OnGameOver;

    private void Update()
    {
        if (_isTimerStop) return;// 始まるまでカウントしない
        _time += Time.deltaTime;
        if(_timeLimit < _time)// 制限時間になったら
        {
            Debug.Log("LimitTime!!");
            GameClear();
            _isTimerStop = true;
        }
    }

    /// <summary>
    /// ゲームスタート時に呼ばれる関数
    /// </summary>
    public void GameStart()
    {
        OnGameStart?.Invoke();
        _isTimerStop = false;
    }

    /// <summary>
    /// ゲームクリア時に呼ばれる関数
    /// </summary>
    public void GameClear()
    {
        OnGameClear?.Invoke();
        SceneLoder.LoadScene(_gameClearSceneName);// ゲームクリアシーンに遷移
    }

    /// <summary>
    /// ゲームオーバー時に呼ばれる関数
    /// </summary>
    public void GameOver()
    {
        OnGameOver?.Invoke();
        SceneLoder.LoadScene(_gameOverSceneName);// ゲームオーバーシーンに遷移
    }
}
