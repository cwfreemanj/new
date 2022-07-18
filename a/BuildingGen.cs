using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingGen : MonoBehaviour
{
    [SerializeField] int texWidth = 8;
    [SerializeField] int texHeight = 5;
    [SerializeField] int texMinHeight = 5;
    [SerializeField] int texMaxHeight = 10;
    [SerializeField] int texMaxWidth = 10;
    [SerializeField] int texMinWidth = 5;

    [SerializeField] Color[] colorPalette;

    public Texture2D mytex;

    public Tilemap topMap;
    public Tilemap botMap;

    public Tile[] topTiles;
    public Tile[] botTiles;
    

    private void Start()
    {
        texHeight = Random.Range(texMinHeight, texMaxHeight);
        texWidth = Random.Range(texMinWidth, texMaxWidth);
        //CreateBuild();
        CreateTileBuild();
    }

    public void CreateTileBuild()
    {
  
        var ran = Random.Range(0, 1);
        int ranTile = Random.Range(0, topTiles.Length);
        // Draw outer walls
        for (int i = 0; i <= texHeight - 1; i++)
        {
            if (ran == 0)
            {
                if (i+2 <= texHeight)
                {
                    topMap.SetTile(new Vector3Int(0, i + 2, 0), topTiles[ranTile]);
                }
                topMap.SetTile(new Vector3Int(texWidth, i, 0), topTiles[ranTile]);
                topMap.SetTile(new Vector3Int(texWidth, texHeight, 0), topTiles[ranTile]);
            }
            else
            {
                if (i + 2 <= texHeight)
                {
                    topMap.SetTile(new Vector3Int(texWidth, i + 2, 0), topTiles[ranTile]);
                }
                topMap.SetTile(new Vector3Int(0, i, 0), topTiles[ranTile]);
                topMap.SetTile(new Vector3Int(0, texHeight, 0), topTiles[ranTile]);

            }
            
        }

        // Draw floors
        for (int i = 3; i <= texHeight; i+=3)
        {
            for (int j = 0; j <= texWidth; j++)
            {
                topMap.SetTile(new Vector3Int(j, i, 0), topTiles[ranTile]);
                
                topMap.SetTile(new Vector3Int(j, texHeight + 1, 0), topTiles[ranTile]);
            }
        }

        int ranBotTile = Random.Range(0, botTiles.Length);
        for (int x = 1; x <= texWidth - 1; x++)
        {
            for (int y = 0; y <= texHeight; y++)
            {

                if (y % 3 != 0 || y == 0)
                {
                    botMap.SetTile(new Vector3Int(x, y, 0), botTiles[ranBotTile]);
                }
            }
        }
    }


    public void CreateBuild()
    {


        mytex = new Texture2D(texWidth, texHeight);
        mytex.filterMode = FilterMode.Point;

        for (int x = 0; x <= mytex.width; x++)
        {
            for (int y = 0; y <= mytex.height; y++)
            {
                mytex.SetPixel(x, y, new Color(1, 1, 1, 0));
            }
        }

        

        DrawBuilding();
    }

    public void DrawBuilding()
    {

        var ran = Random.Range(0, 1);
        DrawPixel(ran == 0 ? 0 : texWidth, 0, new Color(0, 0, 0, 0));
        DrawPixel(ran == 0 ? 0 : texWidth, 1, new Color(0, 0, 0, 0));

        var ranColor = Random.Range(0, colorPalette.Length);

        // Draw outer walls
        for (int i = 0; i < texHeight -1; i++)
        {
            DrawPixel(0, ran == 0 ? i + 2 : i, colorPalette[ranColor]);
            DrawPixel(texWidth, ran == 1 ? i + 2: i, colorPalette[ranColor]);
        }

        // Draw floors
        for (int i = 0; i < texHeight/3; i++)
        {
            for (int j = 0; j < texWidth; j++)
            {
                DrawPixel(j, i * 3, colorPalette[ranColor]);
            }
        }

        Texture2D newTex = new Texture2D(texWidth, texHeight);
        // Draw inner background (darker color)
        for (int x = 1; x < texWidth - 1; x++)
        {
            for (int y = 0; y < texHeight; y++)
            {

                if (y % 3 != 0) {
                    newTex.SetPixel(x, y, new Color(colorPalette[ranColor].r / 2, colorPalette[ranColor].g / 2, colorPalette[ranColor].b / 2, 1));
                }
            }
        }

        Sprite newBGSprite = Sprite.Create(newTex, new Rect(0, 0, mytex.width, mytex.height), new Vector2(.5f, .5f));
        GameObject newObj1 = Instantiate(new GameObject(), new Vector3(0, 0, 0), Quaternion.identity);
        newObj1.transform.localScale *= 25;
        newObj1.AddComponent<SpriteRenderer>();
        newObj1.GetComponent<SpriteRenderer>().sprite = newBGSprite;
        newObj1.GetComponent<SpriteRenderer>().sortingOrder = -1;
        // Create sprite
        Sprite newSprite = Sprite.Create(mytex, new Rect(0, 0, mytex.width, mytex.height), new Vector2(.5f, .5f));
        GameObject newObj = Instantiate(new GameObject(), new Vector3(0, 0, 0), Quaternion.identity);
        newObj.transform.localScale *= 25;
        newObj.AddComponent<SpriteRenderer>();
        newObj.GetComponent<SpriteRenderer>().sprite = newSprite;
        newObj.AddComponent<PolygonCollider2D>();






    }

    public void DrawPixel(int x, int y, Color col)
    {
        mytex.SetPixel(x, y, col);
    }
}
