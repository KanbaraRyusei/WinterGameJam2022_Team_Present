using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLineScript : MonoBehaviour
{
    [SerializeField] GameManager _gM;
    [SerializeField] CreateStage _create;
    [SerializeField] float _gameOverTime = 2f;
    AudioSource _audio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("ƒ„ƒ‰ƒŒƒ`ƒƒƒbƒ^");
            _create._start = false;
            _audio = GetComponent<AudioSource>();
            _audio.Play();
            StartCoroutine(GameOverTime());
        }
    }

    IEnumerator GameOverTime()
    {
        yield return new WaitForSeconds(_gameOverTime);
        _gM.GameOver();
    }
}
