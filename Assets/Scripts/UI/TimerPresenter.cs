using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// UIに値を反映するクラスと値を持つクラスをつなぎ合わせるクラス
/// MVP設計のP(Presenter)の部分
/// </summary>
public class TimerPresenter : MonoBehaviour
{
    [SerializeField]
    private TimerView _view;

    [SerializeField]
    private GameManager _gameManager;

    private void Start()
    {
        _view.SetMaxValue(_gameManager.TimeLimit);// 最大値を設定

        // GameManagerのCurrentTimeが変わるごとに(時間が経過するごと)にViewのSliderの値を設定するように登録
        _gameManager.ObserveEveryValueChanged(x => x.CurrentTime)
            .Subscribe(x => _view.SetValue(x));
    }
}
