using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItems : MonoBehaviour
{
    public bool addItems = false;
    public bool isPooring = false;
    public GameObject bag;
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move bag to mouse
        if (addItems)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bag.transform.position = mousePosition;
            if (Input.GetMouseButtonDown(0) && !isPooring)
            {
                isPooring = true;
                StartCoroutine("Poor");
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isPooring = false;
                StopCoroutine("Poor");
            }
        }
    }

    IEnumerator Poor()
    {
        for (; ; )
        {
            // Random Poor
            int randomAmount = Random.Range(0, 5);

            for (int i = 0; i < randomAmount; i++)
            {
                Instantiate(item, bag.transform.position + new Vector3(Random.Range(0, .05f), Random.Range(0, .05f), 0), Quaternion.identity);
            }

            yield return new WaitForSeconds(Random.Range(.1f, .3f));
        }
    }
}
