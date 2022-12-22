using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D _rb;
    [SerializeField] GameManager _gameManager;
    [SerializeField]
    [Header("プレイヤーの速さ")]
    float _moveSpeed;

    public Vector2 _dir;
    public int _timeCount = 0;
    [SerializeField] 
    [Header("プレイヤーの初期位置")]
     Vector2 _initialPosition;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = 0.0f;
        if(this.transform.position.y <= -3)
        {
            v = 1f;
        }
        _dir = new Vector2(h, v);
        _rb.velocity = _dir * _moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == "Enemy")
        {
            _gameManager.GameOver();
        }
    }
}
