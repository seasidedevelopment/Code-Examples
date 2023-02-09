using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float _minVelo = 0.1f;


    private Rigidbody2D _rb;

    private Vector3 _lastMousePos;
    private Vector3 _mouseVelo;

    private Collider2D _col;

    
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    
    void FixedUpdate()
    {
        _col.enabled = IsMouseMoving();
    }
    
    void Update()
    {
        
        
        SetBladeToMouse();
    }

    private void SetBladeToMouse()
    {
        var _mousePos = Input.mousePosition;
        _mousePos.z = 10;
        _rb.position = Camera.main.ScreenToWorldPoint(_mousePos);
    }

    private bool IsMouseMoving()
    {
        Vector3 _curMousePos = transform.position;
        float _travelled = (_lastMousePos - _curMousePos).magnitude;
        _lastMousePos = _curMousePos;

        if(_travelled > _minVelo)
        
            return true;
        
        else
        
            return false;
        
    }
}
