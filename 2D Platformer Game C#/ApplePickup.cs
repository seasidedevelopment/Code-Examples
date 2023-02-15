using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplePickup : MonoBehaviour
{

    [SerializeField]
    AudioClip _applePickupSFX;
    [SerializeField]
    int _pointsForApples = 100;

    bool _wasCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !_wasCollected)
        {
            _wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(_pointsForApples);
            AudioSource.PlayClipAtPoint(_applePickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
