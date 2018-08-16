using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private int powerupID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.name);

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player)
            {
                if (powerupID == 0)
                {
                    player.TripleShotPowerupEnable();
                }
                else if (powerupID == 1)
                {
                    player.SpeedBoostEnable();
                }
                else if (powerupID == 2)
                {
                    // enable shields
                }
            }

            Destroy(gameObject);
        }
    }
}
