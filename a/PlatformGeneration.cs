using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] int minHeight, maxHeight;
    [SerializeField] int repeatNum;//5
    [SerializeField] int spikeSpawnHeight;
    [SerializeField] GameObject dirt, grass, spike;
    void Start()
    {
        Generation();
    }

    void Generation()
    {
        GameObject temp = new GameObject();
        int repeatValue = 1;
        for (int x = 0; x < width; x++)//This will help spawn a tile on the x axis
        {
            if (repeatValue == 1)
            {
                height = Random.Range(minHeight, maxHeight);
                GenerateFlatPlatform(x, temp);
                repeatValue = repeatNum;
            }
            else
            {
                GenerateFlatPlatform(x, temp);
                repeatValue--;
            }



        }
        temp.transform.position = new Vector3((int)Random.Range(-5, 7), (int)Random.Range(2, 4) + .3f, 0);
    }

    void GenerateFlatPlatform(int x, GameObject temp)
    {
        
        for (int y = 0; y < height; y++)//This will help spawn a tile on the y axis
        {
            spawnObj(dirt, x, y, temp);
        }
        if (height < spikeSpawnHeight)
        {
            spawnObj(grass, x, height, temp);
            spawnObj(spike, x, height + 1, temp);
        }
        else
        {
            spawnObj(grass, x, height, temp);
        }
        


    }

    void spawnObj(GameObject obj, int width, int height, GameObject tem)//What ever we spawn will be a child of our procedural generation gameObj
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = tem.transform;
    }
}
