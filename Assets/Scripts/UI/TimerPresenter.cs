using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// UI�ɒl�𔽉f����N���X�ƒl�����N���X���Ȃ����킹��N���X
/// MVP�݌v��P(Presenter)�̕���
/// </summary>
public class TimerPresenter : MonoBehaviour
{
    [SerializeField]
    private TimerView _view;

    [SerializeField]
    private GameManager _gameManager;

    private void Start()
    {
        _view.SetMaxValue(_gameManager.TimeLimit);// �ő�l��ݒ�

        // GameManager��CurrentTime���ς�邲�Ƃ�(���Ԃ��o�߂��邲��)��View��Slider�̒l��ݒ肷��悤�ɓo�^
        _gameManager.ObserveEveryValueChanged(x => x.CurrentTime)
            .Subscribe(x => _view.SetValue(x));
    }
}
