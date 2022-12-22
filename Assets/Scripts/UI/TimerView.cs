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
    private Slider _slider;

    /// <summary>
    /// Sliderの最大値を設定する
    /// </summary>
    /// <param name="value"></param>
    public void SetMaxValue(float value)
    {
        _slider.maxValue = value;
    }

    /// <summary>
    /// Sliderの現在の値を設定する
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(float value)
    {
        _slider.value = value;
    }
}
