using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("Fruit Prefab")]
    public GameObject _slicedFruitPrefab;
    
    
    public void CreateSlicedFruit()
    {
      GameObject _inst =  (GameObject)Instantiate(_slicedFruitPrefab, transform.position, transform.rotation);


        Rigidbody[] rbsOnSliced = _inst.transform.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody r in rbsOnSliced)
        {
            r.transform.rotation = Random.rotation;
            r.AddExplosionForce(Random.Range(500,1000), transform.position, 5f);

        }

        FindObjectOfType<GameManager>().IncreaseScore(3);

        Destroy(_inst.gameObject, 5);   
        Destroy(gameObject);
    }
    
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Blade b = other.GetComponent<Blade>();

        if(!b)
        {
            return;
        }

        CreateSlicedFruit();
    }
}
