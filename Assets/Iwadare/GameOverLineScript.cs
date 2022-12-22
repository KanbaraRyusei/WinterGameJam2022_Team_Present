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
            Debug.Log("�������`���b�^");
            SceneLoder.LoadScene(_sceneName);
        }
    }
}
