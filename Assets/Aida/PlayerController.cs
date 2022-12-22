using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D _rb;
    [SerializeField] float _moveSpeed;
    Vector2 _dir;
    Vector2 _initialPosition;
    void Start()
    {
        _initialPosition = transform.position;
        _rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        _dir = new Vector2(h, 0);
        _rb.velocity = _dir * _moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("è’ìÀ");
            StartCoroutine(PositionCoroutine());
        }
    }

    public IEnumerator PositionCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        transform.position = _initialPosition;
    }
}
