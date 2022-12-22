using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStage : MonoBehaviour
{

    [Header("Stageの移動速度")]
    [SerializeField] float _scrollSpeed = 2f;

    [Tooltip("Stageのyのサイズ")]
    [Header("Stageのyのサイズを入力")]
    [SerializeField] int _stageSize = 15;

    [Tooltip("作成するStageのindex")]
    int _stageIndex;

    [Header("プレイヤーor自動スクロールするカメラ")]
    [SerializeField] Transform _target;

    [Header("配置するステージを全てここに入れる")]
    [SerializeField] GameObject[] _stageNum;

    [Header("どのくらい先までステージを生成しておくか")]
    [SerializeField] int _aheadStage = 3;

    [Tooltip("生成されているステージのリスト")]
    List<GameObject> _stageList = new List<GameObject>();

    [Header("初期地点のStage")]
    [SerializeField] GameObject _firstStage;

    [Header("Stageをスクロールさせる時はtrue、スクロールさせない時はfalse")]
    [SerializeField] bool _scroll = true;

    [Tooltip("最初に配置するステージを生成するためのbool型")]
    bool _firstIns;

    [Tooltip("一定の時間が経過したらスピードアップするためのbool型")]
    public bool _speedUp;

    [Tooltip("スタート時に実行するbool型")]
    public bool _start;

    void Start()
    {
        //自動生成の仕様上、スクロールするかしないかによってIndexを変えている。
        if (_scroll) _stageIndex = 0;
        else _stageIndex = -1;

        //ステージの生成
        StageManager(_aheadStage);
    }

    // Update is called once per frame
    void Update()
    {
        if (_start)
        {
            if (_scroll)
            {
                //stageのスクロール
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
                //プレイヤーまたはカメラの位置から、現在のステージのインデックスを計算する。
                int targetPosIndex = (int)(_target.position.y / _stageSize);

                //現在のステージの中間まで行ったら(処理としては次のステージに入ったら)ステージの更新処理を行う。
                if (targetPosIndex + _aheadStage > _stageIndex)
                {
                    StageManager(targetPosIndex + _aheadStage);
                }
            }
        }
    }

    /// <summary>ステージの更新をするメソッド</summary>
    /// <param name="map">更新するステージのインデックス</param>
    private void StageManager(int map)
    {
        if (_scroll)
        {
            //指定したステージまで作成
            for (int i = _stageIndex; i <= map; i++)
            {
                MakeStage(i);
            }
        }
        else
        {
            //更新する必要がなかったら何もせずに返す。
            if (map <= _stageIndex)
            {
                return;
            }

            //指定したステージまで作成
            for (int i = _stageIndex + 1; i <= map; i++)
            {
                MakeStage(i);
            }
        }

        //古いステージの削除
        while (_stageList.Count > _aheadStage + 1)
        {
            DestroyStage();
        }

        //stageindexの更新
        _stageIndex = map;
    }

    /// <summary>ステージを生成するメソッド</summary>
    /// <param name="index">生成する場所(インデックス)</param>
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
            //生成するステージをランダムで決める。
            int nextStage = Random.Range(0, _stageNum.Length);
            //ステージの生成
            stageObj = Instantiate(_stageNum[nextStage], new Vector2(0, index * _stageSize), Quaternion.identity);
        }
        //生成しているステージのリストに追加
        _stageList.Add(stageObj);
    }

    /// <summary>ステージを削除するメソッド</summary>
    private void DestroyStage()
    {
        //プレイヤーの後ろにあるステージを削除する。
        GameObject oldStage = _stageList[0];
        _stageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
