using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCountDown : MonoBehaviour
{
    CreateStage _generator;

    // Start is called before the first frame update
    void Start()
    {
        _generator = GameObject.Find("StageGanarator").GetComponent<CreateStage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndCountDown()
    {
        _generator._start = true;
        Destroy(gameObject);
    }

}
