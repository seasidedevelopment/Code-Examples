using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [Header("Power Ups")]
    [SerializeField] float _PowerupSpeed = 5.0f;

    

    [SerializeField]
    private int powerupID;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.down * _PowerupSpeed * Time.deltaTime);

        if (transform.position.y <= -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player !=null)
            {
                if (powerupID == 0)
                {
                    player.TripleShotActive();
                }
                else if (powerupID == 1)
                {
                    Debug.Log("Collected Speed Boost");
                }
                else if (powerupID == 2)
                {
                    Debug.Log("Collected Sheilds");
                }

            }

            Destroy(this.gameObject);
        }
    }
}
