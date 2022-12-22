using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] GameManager _gameManager;
    [SerializeField] float _moveSpeed = 10f;
    Vector2 _dir;
    BoxCollider2D _collider;
    CircleCollider2D _circle;
    AudioSource _audio;
    [SerializeField] CreateStage _create;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _audio = GetComponent<AudioSource>();
        _circle = GetComponent<CircleCollider2D>();
    }

  
    void Update()
    {
        if (_create._start)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = 0.0f;
            if (this.transform.position.y <= -3)
            {
                v = 1f;
            }
            _dir = new Vector2(h, v);
            _rb.velocity = _dir * _moveSpeed;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _circle.enabled = false;
            _create._start = false;
            _gameManager.GameOver();
        }
        else
        {
            _create._start = true;
        }
    }
    public IEnumerator GameOverCoroutine()
    {
        _audio.Play();
        yield return new WaitForSeconds(1.0f);
    }
}
