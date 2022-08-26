using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleGuys : MonoBehaviour
{
    public GameObject gameOverScreen;
    GameObject player;
    public int health = 5;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.gameObject.transform.position, speed * Time.deltaTime);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //GameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnMouseDown()
    {
        health -= 1;
    }
}
