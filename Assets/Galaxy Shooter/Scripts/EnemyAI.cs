using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    private float speed = 5.0f;

    [SerializeField]
    private GameObject explosionPrefab;

    private UIManager uiManager;

    // Use this for initialization
    void Start () {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
 	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -7)
        {
            float randomX = Random.Range(-7, 7);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.name);

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                player.Damage();
            }

            Destroy(gameObject);
            Explode();
        }
        else if (other.tag == "Laser")
        {
            if (other.transform.parent)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Destroy(other.gameObject);
            uiManager.UpdateScore();
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
