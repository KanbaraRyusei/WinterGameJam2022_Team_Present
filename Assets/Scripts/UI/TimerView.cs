using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI�ɓn���ꂽ�l�𔽉f����N���X
/// MVP�݌v��V(View)�̕���
/// </summary>
public class TimerView : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    /// <summary>
    /// Slider�̍ő�l��ݒ肷��
    /// </summary>
    /// <param name="value"></param>
    public void SetMaxValue(float value)
    {
        _slider.maxValue = value;
    }

    /// <summary>
    /// Slider�̌��݂̒l��ݒ肷��
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(float value)
    {
        _slider.value = value;
    }
}
