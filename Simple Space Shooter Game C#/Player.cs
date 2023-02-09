using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Player Elements")]
   [SerializeField] float playerSpeed = 5.0f;
    [SerializeField] private int _lives = 3;
    float HorizontalMovement;
    float VerticalMovement;

    [Header("Laser Shot Elements")]
    [SerializeField] private  GameObject laserPrefab;
    [SerializeField] private GameObject TriplelaserPrefab;
    [SerializeField]  private float _fireRate = 0.5f;
    [SerializeField] private float _canFire = -1f;
    

    [SerializeField] private bool _tripleShotActive = false;

    private SpawnManager _spawnManager;

    void playerMovement()
                {
                    float HorizontalMovement = Input.GetAxis("Horizontal");
                    float VerticalMovement = Input.GetAxis("Vertical");

                        

                     Vector3 direction = new Vector3(HorizontalMovement, VerticalMovement, 0);

                     transform.Translate(direction * playerSpeed * Time.deltaTime);

       

                    if (transform.position.y >= 0)
                        {
                             transform.position = new Vector3(transform.position.x, 0, 0);
                        }
                    else if (transform.position.y <=-3.8f)
                        {
                            transform.position = new Vector3(transform.position.x, -3.8f, 0);
                        }
                    
                    if (transform.position.x >= 10.5f)
                    {
                        transform.position = new Vector3(-10.5f, transform.position.y, 0);
                    }
                    else if (transform.position.x <= -10.5f)
                    {
                        transform.position = new Vector3(10.5f, transform.position.y, 0);
                    }
    }

                void laser()
                {
                    if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
                    {
                        Debug.Log("Space Pressed");

                        _canFire = Time.time + _fireRate;

                        if (_tripleShotActive == true)
                        {
                            Instantiate(TriplelaserPrefab, transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
                        }
                        

                    }

                    
                }

    
    void Start()
    {
        
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager id NULL");
        }

    }

   
    void Update()
    {
        playerMovement();

        laser();
    }

    public void Damage()
    {
        _lives -= 1;
            

        if (_lives <1 )
        {
            _spawnManager.OnPlayerDeath();

            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _tripleShotActive = true;

        StartCoroutine(TripleShotPowerDownRoutine());

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }
}
