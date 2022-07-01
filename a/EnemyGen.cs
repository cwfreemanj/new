using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGen : MonoBehaviour
{
    public PlayerGenerator enemy;
    public float spawnEvery = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemy()
    {
        for (; ; )
        {
            GameObject temp = enemy.Awaken();
            yield return new WaitForSeconds(spawnEvery);
        }
    }
}

