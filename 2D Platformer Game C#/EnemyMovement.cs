using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header ("Enemy Info")]
    [SerializeField] 
    float _enemyMoveSpeed = 1f;

    Rigidbody2D _enemyRigidBody;


    void Start()
    {
        _enemyRigidBody = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        _enemyRigidBody.velocity = new Vector2(_enemyMoveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D _other)
    {
        _enemyMoveSpeed = -_enemyMoveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_enemyRigidBody.velocity.x)), 1f);
    }
}
