using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private GameObject laserPrefab;
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
            Instantiate(laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

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
}
