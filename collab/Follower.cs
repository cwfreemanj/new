using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float minDist = 1.1f;
    public float speed = 1;

    public GameObject main;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if (main && Vector2.Distance(transform.position, main.transform.position) > minDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, main.transform.position, speed * Time.deltaTime);
            }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !main)
        {
            main = collision.gameObject.GetComponent<SnakeController>().followerList.Count > 0 ? collision.gameObject.GetComponent<SnakeController>().followerList[collision.gameObject.GetComponent<SnakeController>().followerList.Count - 1]: collision.gameObject;
            collision.gameObject.GetComponent<SnakeController>().followerList.Add(gameObject);
        }
    }

}
