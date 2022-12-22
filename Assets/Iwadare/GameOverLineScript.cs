using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLineScript : MonoBehaviour
{
    [SerializeField] GameManager _gM;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("ƒ„ƒ‰ƒŒƒ`ƒƒƒbƒ^");
            _gM?.OnGameOver();
        }
    }
}
