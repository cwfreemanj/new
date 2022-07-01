using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] Sprite helmSprites;
    [SerializeField] Sprite chestplateSprites;
    [SerializeField] Sprite pantsSprites;

    [SerializeField] int texHeight = 8;
    [SerializeField] int texWidth = 7;
    [SerializeField] float varitiveDefinition = .1f;

    [SerializeField] int minPlayerSize = 10;
    [SerializeField] int maxPlayerSize = 20;

    int ppu = 10;

    public Texture2D mytex;
    public Color helmBright;
    public Color helmDark;

    public Color skinBright;
    public Color skinDark;

    public Color chestBright;
    public Color chestDark;

    public Color pantsBright;
    public Color pantsDark;

    public Color shoesBright;
    public Color shoesDark;

    [SerializeField] int genHeight = 50;
    [SerializeField] int genWidth = 50;

    public bool canCreate = false;
    public bool isWalking = false;
    public float walkingSpeed = 1f;

    public SpriteRenderer spriteRen;

    private void Start()
    {
        if (canCreate)
        {
            CreatePlayer();
            spriteRen = GetComponent<SpriteRenderer>();
        }else if (GetComponent<SpriteRenderer>())
        {
            spriteRen = GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        if (isWalking)
        {
            walkingSpeed -= Time.deltaTime;
            Walking();
        }
    }

    public void CreatePlayer()
    {
        
        for (int x = 0; x < genWidth; x++)
        {
            for (int y = 0; y < genHeight; y++)
            {
                Awaken();
            }
        }
    }

    public void Walking()
    {
        Texture2D myTex = spriteRen.sprite.texture;

        if (walkingSpeed <= 0)
        {
            DrawPixel(1, 0, Color.clear);
            DrawPixel(4, 0, Color.clear);

            DrawPixel(2, 0, shoesBright);
            DrawPixel(3, 0, shoesDark);
        }

        if (walkingSpeed > 0)
        {
            DrawPixel(1, 0, shoesBright);
            DrawPixel(4, 0, shoesDark);

            DrawPixel(2, 0, Color.clear);
            DrawPixel(3, 0, Color.clear);
        }

        if (walkingSpeed <= -.5f)
        {
            walkingSpeed = .5f;
        }
        myTex.Apply();

        spriteRen.sprite = Sprite.Create(myTex, new Rect(0, 0, this.mytex.width, this.mytex.height), new Vector2(.5f, .5f));

    }

    public Color[] CreatePalette(Color col1, Color col2)
    {
        col1 = new Color(Random.Range(.5f, 1) + Random.Range(0, varitiveDefinition), Random.Range(.5f, 1) + Random.Range(0, varitiveDefinition), Random.Range(.5f, 1) + Random.Range(0, varitiveDefinition), 1);
        col2 = new Color(helmBright.r / 2, helmBright.g / 2, helmBright.b / 2, 1);
        Color[] col = { col1, col2 };
        return col;
    }

    public GameObject Awaken()
    {
        
        // Set colors for each body section
        helmBright =  CreatePalette(helmBright, helmDark)[0];
        helmDark = new Color(helmBright.r / 2, helmBright.g / 2, helmBright.b / 2, 1);

        skinBright = CreatePalette(skinBright, skinDark)[0];
        skinDark = new Color(skinBright.r / 2, skinBright.g / 2, skinBright.b / 2, 1);

        chestBright =CreatePalette(chestBright, chestDark)[0];
        chestDark = new Color(chestBright.r / 2, chestBright.g / 2, chestBright.b / 2, 1);

        pantsBright = CreatePalette(pantsBright, pantsDark)[0];
        pantsDark = new Color(pantsBright.r / 2, pantsBright.g / 2, pantsBright.b / 2, 1);


        shoesBright = CreatePalette(shoesBright, shoesDark)[0];
        shoesDark = new Color(shoesBright.r / 2, shoesBright.g / 2, shoesBright.b / 2, 1);


        //Create new texture for player
        mytex = new Texture2D(texWidth, texHeight);
        mytex.filterMode = FilterMode.Point;
        for (int x = 0; x <= mytex.width; x++)
        {
            for (int y =0; y <=mytex.height; y++)
            {
                mytex.SetPixel(x, y, new Color(1, 1, 1, 0));
            }
        }
  
                DrawPlayer();
  
        // Create sprite
        Sprite newSprite = Sprite.Create(mytex, new Rect(0, 0, mytex.width, mytex.height), new Vector2(.5f, .5f));
        
        // Create new player
       GetComponent<PlayerGenerator>().canCreate = false;
        GameObject newObj = Instantiate(gameObject, new Vector3(Random.Range(-8, 8), 15, 0),Quaternion.identity);
        newObj.transform.localScale *= Random.Range(minPlayerSize, maxPlayerSize);
        newObj.AddComponent<SpriteRenderer>();
        newObj.GetComponent<SpriteRenderer>().sprite = newSprite;
        newObj.AddComponent<PolygonCollider2D>();
        newObj.AddComponent<Rigidbody2D>();
        newObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        newObj.AddComponent<Enemy>();
        newObj.GetComponent<Enemy>().speed *= 3;

        return newObj;
          }

    public void DrawPlayer()
    {
        
       
            DrawPixel(0, 0, new Color(0, 0, 0, .1f));
        
        // Draw Specific pixels based on player template
            // Draw helm
            DrawPixel(4, 7, helmBright);
            DrawPixel(3, 7, helmBright);
            DrawPixel(2, 7, helmBright);
            DrawPixel(5, 7, helmDark);
      

            DrawPixel(5, 6, helmDark);
            DrawPixel(4, 6, helmDark);
            DrawPixel(3, 6, helmDark);
            DrawPixel(2, 6, helmDark);
        
    
            DrawPixel(1, 5, helmBright);
            DrawPixel(1, 6, helmBright);
      
      
            DrawPixel(1, 4, helmDark);
        

        //Draw Eyes
        
            DrawPixel(2, 5, new Color(0, 0, 0, 1));
        
      
            DrawPixel(4, 5, new Color(0, 0, 0, 1));
        

        // Draw Skin
       
            DrawPixel(2, 4, skinBright);

            DrawPixel(3, 4, skinBright);
        
       
            DrawPixel(3, 5, skinBright);
       
            DrawPixel(4, 4, skinDark);
            DrawPixel(0, 2, skinBright);
        

            DrawPixel(6, 3, skinDark);
        
        // Draw chest

            
            DrawPixel(1, 3, chestBright);
           DrawPixel(1, 4, chestBright);
            DrawPixel(3, 3, chestBright);
            DrawPixel(0, 3, chestBright);
     
            DrawPixel(1, 2, chestBright);
            DrawPixel(2, 2, chestBright);
            DrawPixel(3, 2, chestBright);
            DrawPixel(2, 3, chestBright);


        DrawPixel(4, 3, chestDark);
            DrawPixel(4, 2, chestDark);

            DrawPixel(5, 3, chestDark);
     

        // Draw pants
      
            DrawPixel(1, 1, pantsBright);
            DrawPixel(2, 1, pantsBright);
        DrawPixel(3, 1, pantsBright);

        DrawPixel(4, 1, pantsDark);
       

        // Draw shoes
        
            DrawPixel(1, 0, shoesBright);
        
            DrawPixel(4, 0, shoesDark);
        

        mytex.Apply();
    }

    public void DrawPixel(int x, int y, Color colur)
    {
        //for (int xi = 0; xi < 10; xi++)
        //{
        //    for (int yi = 0; yi < 10; yi++)
        //    {
        //        mytex.SetPixel(x + xi, y + yi, colur);
        //    }
        //}
        mytex.SetPixel(x, y, colur);
    }
}
