using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class TilemapExplorer : MonoBehaviour
{
    public TileBase tilebase;
    Tilemap tilemap;

    private GameObject player;
    private Player playerScript;

    private float playerLastPos;
    private float maxPlayerDistToTravel= 1;
    private float lastBuildX = 0;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        player = GameObject.Find("Player");
        lastBuildX = player.transform.position.x;
        CheckAllTiles();
    }

    // Update is called once per frame
    void Update()
    {
        //Player went left
        /*
        if (player.transform.position.x < playerLastPos)
        {
            // Player has traveled 1 units to the left from the last build
            if(player.transform.position.x - lastBuildX > maxPlayerDistToTravel)
            {
                //Build Here
                BuildNewLine((int)(player.transform.position.x - lastBuildX));
                lastBuildX = player.transform.position.x;
               
            }


        }
        
        //Player went right
        if (player.transform.position.x > playerLastPos)
        {
            // Player has traveled 25 units to the right from the last build
            if (player.transform.position.x - lastBuildX < maxPlayerDistToTravel)
            {
                //Build Here
                BuildNewLine((int)(player.transform.position.x - lastBuildX));
                lastBuildX = player.transform.position.x;
                
            }
        }*/

        
            playerLastPos = player.transform.position.x;
        DeleteOuterWarehouseTiles();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = tilemap.WorldToCell(worldPoint);

        TileBase tile = tilemap.GetTile(position);
        Debug.Log(position);
        //tilemap.SetTile(position, null);




    }


    public void BuildNewLine(int cellDistanceFromLast)
    {
        for (int i = -24; i < 10; i++)
        {
            tilemap.SetTile(new Vector3Int(i + cellDistanceFromLast, 0, 0), tilebase);
            
        }
    }

    public void CheckAllTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        for (int i = 0; i < allTiles.Length; i++)
        {
            Debug.Log(i + " : " + allTiles[i]);
        }

        /*for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                    
                }

            }
        }*/
    }

    // One Time Use
    public void DeleteOuterWarehouseTiles()
    {
        //Find Tiles to delete with CheckAllTiles(), then find the blocks to delete
        // x: 65, y : 15 - 18

        //BoundsInt bounds = tilemap.cellBounds;
        //Debug.Log(tilemap.GetTile(new Vector3Int(65, 17, 0)));
        tilemap.SetTile(new Vector3Int(29, 8, 0), null);
        tilemap.SetTile(new Vector3Int(29, 7, 0), null);
        tilemap.SetTile(new Vector3Int(29, 6, 0), null);
        
    }

}
