using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float _bulletSpeed = 20f;
    float _xSpeed;

    Rigidbody2D _rb;
    PlayerMovement _player;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement> ();
        _xSpeed = _player.transform.localScale.x * _bulletSpeed;
    }

    
    void Update()
    {
        _rb.velocity = new Vector2(_xSpeed, 0f);
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemies")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void onCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
