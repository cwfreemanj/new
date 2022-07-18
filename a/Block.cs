using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    public float miningTimer = .25f;
    public float healthBarTimer = 1.5f;
    public float blockValue;

    public GameObject healthBar;
    private GameObject lastHitObject;
    public int blockType = 1;

    public GameObject itemDrop;

    public int blockLevel = 1;

    private GameObject bButtonObj;
    //private BButton bButton;
    

    private void Start()
    {
        health = maxHealth;
        bButtonObj = GameObject.Find("BButton");
        //bButton = bButtonObj.GetComponent<BButton>();

        blockValue = maxHealth / 100;
       
    }

    public void Update()
    {

       // if (healthBarTimer < 0 && bButton.GetCurrentBlock() != gameObject)
       // {
         //   healthBar.SetActive(false);
        //    healthBarTimer = 1f;
      //  }

        if (health <= 0)
        {
            /*if (itemDrop != null)
            {
                SpawnDrop();
                //if (lastHitObject && lastHitObject.name == "BButton") 
                //{
                //GameObject cred = GameObject.Find("Cred");
                //cred.GetComponent<Player>().UpdateScore(blockValue);

                //    cred.GetComponent<Player>().achieve.MatterMined(gameObject.transform.GetChild(1).gameObject.name.Substring(7), 1);
                //    cred.GetComponent<Player>().achieve.MatterMined("All_Matter_Drop", 1);
                //    cred.GetComponent<Player>().gameManager.blocksMinedThisLevel++;
                //}
            }*/

            

            Destroy(gameObject);
        }

        miningTimer -= Time.deltaTime;
        //if (healthBar.activeSelf && bButton.GetCurrentBlock() != gameObject)
            //healthBarTimer -= Time.deltaTime;
    }

    public void SetHealth(float value, GameObject lastHit)
    {
        if (miningTimer < 0)
        {
            health += value;

            miningTimer = .1f;

            //if (lastHit.name == "BButton" && GameController.control)
            //{
            //    //Play sound
            //    GameController.control.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            //    GameController.control.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
            //}

            //healthBar.transform.localScale = new Vector3((health / maxHealth) / 2, .25f, 1);
            //.SetActive(true);
            lastHitObject = lastHit;
        }
    }

    public void SetMaxHealth(float value)
    {
        maxHealth = value;
    }

    

    public void SpawnDrop()
    {
        // Check to see level
       /* if (blockType == 4)
        {
            // Spawn Four Count with random offsets from center

            if (lastHitObject.GetComponent<BButton>())
            {
                for (int i = 0; i < 4 * (lastHitObject.GetComponent<BButton>().player.GetComponent<Player>().increaseDropRate ? lastHitObject.GetComponent<BButton>().player.GetComponent<Player>().dropRate : 1); i++)
                {
                    GameObject temp1 = Instantiate(itemDrop, transform.position + new Vector3(Random.Range(.01f, .01f), Random.Range(.01f, .01f)), Quaternion.identity, GameManager.blockCleaner.transform);
                    temp1.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    GameObject temp1 = Instantiate(itemDrop, transform.position + new Vector3(Random.Range(.01f, .01f), Random.Range(.01f, .01f)), Quaternion.identity, GameManager.blockCleaner.transform);
                    temp1.SetActive(true);
                }
            }

          
        }
        else if (blockType == 3)
        {
            // Spawn Three Count with random offsets from center


            if (lastHitObject.GetComponent<BButton>())
            {
                Debug.Log(lastHitObject.name);
                for (int i = 0; i < 3 * (lastHitObject.GetComponent<BButton>().player.GetComponent<Player>().increaseDropRate ? lastHitObject.GetComponent<BButton>().player.GetComponent<Player>().dropRate : 1); i++)
                {
                    GameObject temp1 = Instantiate(itemDrop, transform.position + new Vector3(Random.Range(.01f, .01f), Random.Range(.01f, .01f)), Quaternion.identity, GameManager.blockCleaner.transform);
                    temp1.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    GameObject temp1 = Instantiate(itemDrop, transform.position + new Vector3(Random.Range(.01f, .01f), Random.Range(.01f, .01f)), Quaternion.identity, GameManager.blockCleaner.transform);
                    temp1.SetActive(true);
                }
            }
        }
        else if (blockType == 2)
        {
            // Spawn Two Count with random offsets from center

            Debug.Log(lastHitObject.name);
            if (lastHitObject.GetComponent<BButton>())
            {
                for (int i = 0; i < 2 * (lastHitObject.GetComponent<BButton>().player.GetComponent<Player>().increaseDropRate ? lastHitObject.GetComponent<BButton>().player.GetComponent<Player>().dropRate : 1); i++)
                {
                    GameObject temp1 = Instantiate(itemDrop, transform.position + new Vector3(Random.Range(.01f, .01f), Random.Range(.01f, .01f)), Quaternion.identity, GameManager.blockCleaner.transform);
                    temp1.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject temp1 = Instantiate(itemDrop, transform.position + new Vector3(Random.Range(.01f, .01f), Random.Range(.01f, .01f)), Quaternion.identity, GameManager.blockCleaner.transform);
                    temp1.SetActive(true);
                }
            }

        }
        else
        {
            Debug.Log(lastHitObject.name);
            if (lastHitObject.GetComponent<BButton>())
            {
                for (int i = 0; i < 1 * (lastHitObject.GetComponent<BButton>().player.GetComponent<Player>().increaseDropRate ? lastHitObject.GetComponent<BButton>().player.GetComponent<Player>().dropRate : 1); i++)
                {
                    GameObject temp1 = Instantiate(itemDrop, transform.position + new Vector3(Random.Range(.01f, .01f), Random.Range(.01f, .01f)), Quaternion.identity, GameManager.blockCleaner.transform);
                    temp1.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i <= 1; i++)
                {
                    GameObject temp1 = Instantiate(itemDrop, transform.position + new Vector3(Random.Range(.01f, .01f), Random.Range(.01f, .01f)), Quaternion.identity, GameManager.blockCleaner.transform);
                    temp1.SetActive(true);
                }
            }
        }
       */


    }

}
