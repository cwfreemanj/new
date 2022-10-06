using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUnit : MonoBehaviour
{
    public GameObject unit;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 2, 2); 
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void Spawn()
    {
        Instantiate(unit, gameObject.transform.position, Quaternion.identity);
    }
}
