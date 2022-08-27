using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject friend;
    public GameObject enemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for (; ; )
        {
            Instantiate(friend, player.transform.position + new Vector3(Random.Range(4, 8), Random.Range(4, 8), 0), Quaternion.identity);
            Instantiate(enemy, player.transform.position + new Vector3(Random.Range(4, 8), Random.Range(4, 8), 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2, 6));
        }
    }
}
