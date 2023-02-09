using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Elements")]
    [SerializeField] float _EnemySpeed = 4.0f;

    void EnemyMovement()
    {
        System.Random random = new System.Random();
        transform.Translate(Vector3.down * _EnemySpeed * Time.deltaTime);


        if (transform.position.y <= -5.5f)
        {
            transform.position = new Vector3(random.Next(-9, 9), 11, 0);
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        EnemyMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.transform.name);

        if (other.gameObject.tag == "Player")
        {

            
            Player player = other.transform.GetComponent<Player>();

                    if (player != null)
                    {
                        player.Damage();
                    }

            
            Destroy(this.gameObject);
        }
        
       if (other.gameObject.tag == "Laser")
        {
            Destroy(GameObject.FindWithTag("Laser"));
            Destroy(this.gameObject);
        }
    }
}
