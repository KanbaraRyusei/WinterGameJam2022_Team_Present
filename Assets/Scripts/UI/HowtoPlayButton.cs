using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoPlayButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            _panel.SetActive(false);
        }
    }

    public void OnClick()
    {
        _panel.SetActive(true);
    }
}
