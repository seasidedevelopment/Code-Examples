using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Laser Elements")]
    [SerializeField] float _Speed = 5.0f;

    void laserMovement()
    {
        transform.Translate(Vector3.up * _Speed * Time.deltaTime);

        if (transform.position.y >= 8.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    
    void Start()
    {
        
    }

    
    void Update()
    {
        laserMovement();
    }

}
