using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 _moveInput;
    Rigidbody2D _rb;

    [SerializeField]
    private float _playerSpeed = 5;
    [SerializeField]
    private float _jumpSpeed = 10f;
    [SerializeField]
    private float _climbSpeed = 5f;
    [SerializeField]
    Vector2 _deathKick = new Vector2(10f, 10f);
    [SerializeField]
    GameObject _bullet;
    [SerializeField]
    Transform _gun;

    Animator _anim;
    CapsuleCollider2D _bodyCapsuleCollider;
    BoxCollider2D _myFeetCollider;

    float _gravityScaleAtStart;

    bool _isAlive = true;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _bodyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        _myFeetCollider = GetComponent<BoxCollider2D>();
        _gravityScaleAtStart = _rb.gravityScale;
    }

    
    void Update()
    {
        if (!_isAlive) { return;  }
        
        Run();
        FlipSprite();
        ClimbLadder();
        PlayerDeath();
    }

    void OnMove(InputValue _value)
    {
        if (!_isAlive) { return; }

        _moveInput = _value.Get<Vector2>();
        Debug.Log(_moveInput);
    }

    void OnJump(InputValue _value)
    {
        if (!_isAlive) { return; }

        if (!_bodyCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        
        if (_value.isPressed)
        {
            _rb.velocity += new Vector2(0f, _jumpSpeed);
        }
    }

    void Run() 
    {
        Vector2 _playerVelocity = new Vector2(_moveInput.x * _playerSpeed, _rb.velocity.y);
        _rb.velocity = _playerVelocity;

        bool _playerHasHorizontalSpeed = Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon;

      
            _anim.SetBool("IsRunning", _playerHasHorizontalSpeed);
      
    }

    void FlipSprite()
    {
        bool _playerHasHorizontalSpeed = Mathf.Abs(_rb.velocity.x) > Mathf.Epsilon;

        if (_playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rb.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!_bodyCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            _rb.gravityScale = +_gravityScaleAtStart;
            _anim.SetBool("IsClimbing", false);

            return;
        }

        Vector2 _climbVelocity = new Vector2(_rb.velocity.x, _moveInput.y * _climbSpeed);
        _rb.velocity = _climbVelocity;
        _rb.gravityScale = 0f;

        bool _playerHasVerticalSpeed = Mathf.Abs(_rb.velocity.y) > Mathf.Epsilon;
        _anim.SetBool("IsClimbing", _playerHasVerticalSpeed);
    }

    void PlayerDeath()
    {
        if(_bodyCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            _isAlive = false;
            _anim.SetTrigger("Dying");
            _rb.velocity = _deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void OnFire(InputValue _value)
    {
        if (!_isAlive) { return; }

        Instantiate(_bullet, _gun.position, transform.rotation);
    }
}
