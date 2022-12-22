using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIに渡された値を反映するクラス
/// MVP設計のV(View)の部分
/// </summary>
public class TimerView : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    private float _limit;

    /// <summary>
    /// 時間の最大値を設定する
    /// </summary>
    /// <param name="value"></param>
    public void SetMaxValue(float value)
    {
        _limit = value;
    }

    /// <summary>
    /// 時間の現在の値を設定する
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(float value)
    {
        int time = (int)_limit - (int)value;
        _text.text = time.ToString();
    }
}
