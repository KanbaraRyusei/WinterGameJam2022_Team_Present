using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCountDown : MonoBehaviour
{
    
    [SerializeField] CreateStage _generator;
    [SerializeField] GameManager _gM;

    private void Update()
    {
        if (_gM.CurrentTime >= 30)
        {
            _generator._speedUp = true;
            Destroy(gameObject);
        }
    }


    public void EndCountDown()
    {
        _generator._start = true;
        _gM.GameStart();
    }

}
