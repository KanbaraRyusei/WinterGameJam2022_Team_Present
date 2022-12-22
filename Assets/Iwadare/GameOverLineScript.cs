using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLineScript : MonoBehaviour
{
    [SerializeField]string _sceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("ƒ„ƒ‰ƒŒƒ`ƒƒƒbƒ^");
            SceneLoder.LoadScene(_sceneName);
        }
    }
}
