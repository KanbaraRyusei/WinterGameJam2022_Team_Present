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
    private Text _text;

    private float _limit;

    /// <summary>
    /// ���Ԃ̍ő�l��ݒ肷��
    /// </summary>
    /// <param name="value"></param>
    public void SetMaxValue(float value)
    {
        _limit = value;
    }

    /// <summary>
    /// ���Ԃ̌��݂̒l��ݒ肷��
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(float value)
    {
        int time = (int)_limit - (int)value;
        _text.text = time.ToString();
    }
}
