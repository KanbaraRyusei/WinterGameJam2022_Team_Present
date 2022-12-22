using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStage : MonoBehaviour
{

    [Header("Stage�̈ړ����x")]
    [SerializeField] float _scrollSpeed = 2f;

    [Tooltip("Stage��y�̃T�C�Y")]
    [Header("Stage��y�̃T�C�Y�����")]
    [SerializeField] int _stageSize = 15;

    [Tooltip("�쐬����Stage��index")]
    int _stageIndex;

    [Header("�v���C���[or�����X�N���[������J����")]
    [SerializeField] Transform _target;

    [Header("�z�u����X�e�[�W��S�Ă����ɓ����")]
    [SerializeField] GameObject[] _stageNum;

    [Header("�ǂ̂��炢��܂ŃX�e�[�W�𐶐����Ă�����")]
    [SerializeField] int _aheadStage = 3;

    [Tooltip("��������Ă���X�e�[�W�̃��X�g")]
    List<GameObject> _stageList = new List<GameObject>();

    [Header("�����n�_��Stage")]
    [SerializeField] GameObject _firstStage;

    [Header("Stage���X�N���[�������鎞��true�A�X�N���[�������Ȃ�����false")]
    [SerializeField] bool _scroll = true;

    [Tooltip("�ŏ��ɔz�u����X�e�[�W�𐶐����邽�߂�bool�^")]
    bool _firstIns;

    [Tooltip("���̎��Ԃ��o�߂�����X�s�[�h�A�b�v���邽�߂�bool�^")]
    public bool _speedUp;

    [Tooltip("�X�^�[�g���Ɏ��s����bool�^")]
    public bool _start;

    void Start()
    {
        //���������̎d�l��A�X�N���[�����邩���Ȃ����ɂ����Index��ς��Ă���B
        if (_scroll) _stageIndex = 0;
        else _stageIndex = -1;

        //�X�e�[�W�̐���
        StageManager(_aheadStage);
    }

    // Update is called once per frame
    void Update()
    {
        if (_start)
        {
            if (_scroll)
            {
                //stage�̃X�N���[��
                foreach (GameObject stage in _stageList)
                {
                    if (!_speedUp)
                    {
                        stage.transform.Translate(0f, Time.deltaTime * _scrollSpeed, 0f);
                    }
                    else
                    {
                        stage.transform.Translate(0f, Time.deltaTime * _scrollSpeed * 1.5f, 0f);
                    }
                }

                if (_stageList[1].transform.position.y <= 0)
                {
                    StageManager(_aheadStage);
                }
            }
            else
            {
                //�v���C���[�܂��̓J�����̈ʒu����A���݂̃X�e�[�W�̃C���f�b�N�X���v�Z����B
                int targetPosIndex = (int)(_target.position.y / _stageSize);

                //���݂̃X�e�[�W�̒��Ԃ܂ōs������(�����Ƃ��Ă͎��̃X�e�[�W�ɓ�������)�X�e�[�W�̍X�V�������s���B
                if (targetPosIndex + _aheadStage > _stageIndex)
                {
                    StageManager(targetPosIndex + _aheadStage);
                }
            }
        }
    }

    /// <summary>�X�e�[�W�̍X�V�����郁�\�b�h</summary>
    /// <param name="map">�X�V����X�e�[�W�̃C���f�b�N�X</param>
    private void StageManager(int map)
    {
        if (_scroll)
        {
            //�w�肵���X�e�[�W�܂ō쐬
            for (int i = _stageIndex; i <= map; i++)
            {
                MakeStage(i);
            }
        }
        else
        {
            //�X�V����K�v���Ȃ������牽�������ɕԂ��B
            if (map <= _stageIndex)
            {
                return;
            }

            //�w�肵���X�e�[�W�܂ō쐬
            for (int i = _stageIndex + 1; i <= map; i++)
            {
                MakeStage(i);
            }
        }

        //�Â��X�e�[�W�̍폜
        while (_stageList.Count > _aheadStage + 1)
        {
            DestroyStage();
        }

        //stageindex�̍X�V
        _stageIndex = map;
    }

    /// <summary>�X�e�[�W�𐶐����郁�\�b�h</summary>
    /// <param name="index">��������ꏊ(�C���f�b�N�X)</param>
    private void MakeStage(int index)
    {
        GameObject stageObj;
        if (!_firstIns)
        {
            stageObj = Instantiate(_firstStage, new Vector2(0, index * _stageSize), Quaternion.identity);
            _firstIns = true;
        }
        else
        {
            //��������X�e�[�W�������_���Ō��߂�B
            int nextStage = Random.Range(0, _stageNum.Length);
            //�X�e�[�W�̐���
            stageObj = Instantiate(_stageNum[nextStage], new Vector2(0, index * _stageSize), Quaternion.identity);
        }
        //�������Ă���X�e�[�W�̃��X�g�ɒǉ�
        _stageList.Add(stageObj);
    }

    /// <summary>�X�e�[�W���폜���郁�\�b�h</summary>
    private void DestroyStage()
    {
        //�v���C���[�̌��ɂ���X�e�[�W���폜����B
        GameObject oldStage = _stageList[0];
        _stageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
