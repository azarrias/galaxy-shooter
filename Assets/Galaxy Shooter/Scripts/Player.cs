using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool canTripleShot = false;
    public bool hasSpeedBoost = false;

    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleLaserPrefab;
    [SerializeField]
    private float fireRate = 0.25f;
    private float nextFire = 0.0f;

    [SerializeField]
    private float speed = 5.0f;

	void Start () {
        transform.position = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();

        // if space key pressed spawn laser at player position
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Cool down system
        if (Time.time > nextFire)
        {
            if (canTripleShot)
            {
                Instantiate(tripleLaserPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            }

            nextFire = Time.time + fireRate;
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (hasSpeedBoost)
        {
            transform.Translate(Vector3.right * speed * 1.5f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * speed * 1.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        }

        if (transform.position.x > 8.15f)
        {
            transform.position = new Vector3(8.15f, transform.position.y, 0);
        }
        else if (transform.position.x < -8.15f)
        {
            transform.position = new Vector3(-8.15f, transform.position.y, 0);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
    }

    public void TripleShotPowerupEnable()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostEnable()
    {
        hasSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        // Power down system
        // Wait for 5 seconds, then disable the power up
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        // Power down system
        // Wait for 5 seconds, then disable the power up
        yield return new WaitForSeconds(5.0f);
        hasSpeedBoost = false;
    }

}
