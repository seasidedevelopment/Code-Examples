using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] _objsToSpawn;
    public Transform[] _spawnPlaces;

    public float _minWait = 0.3f;
    public float _maxWait = 1.0f;

    public float _minForce = 12;
    public float _maxForce = 17;

  
    void Start()
    {
        StartCoroutine(SpawnFruits());
    }

    private IEnumerator SpawnFruits()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(_minWait, _maxWait));

            Transform _t = _spawnPlaces[Random.Range(0, _spawnPlaces.Length)];

            GameObject _go = null;
            float _p = Random.Range(0, 100);

            if(_p < 10)
            {
                _go = _objsToSpawn[0];
            }
            else
            {
                _go = _objsToSpawn[Random.Range(1, _objsToSpawn.Length)];
            }

            GameObject _fruit = Instantiate(_go, _t.position, _t.rotation);

            _fruit.GetComponent<Rigidbody2D>().AddForce(_t.transform.up * Random.Range(_minForce, _maxForce), ForceMode2D.Impulse);

            Debug.Log("fruit gets spawned now");

         
            Destroy(_fruit, 5);
        }
    }
    
}
