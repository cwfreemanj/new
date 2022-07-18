using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        BASIC,
        BOSS,
    }

    public EnemyType enemyType;
    PlayerGenerator playerGen;

    public int deathValue = 100;

    public int attackDamage = 10;
    public int shieldBreak = 5;
    public Vector2 attackForce = new Vector2(2, 2);
    public float currentHealth = 100;
    public float maxHealth = 100;
    public int armor = 0;

    public float miningDistance = 1;
    public float miningStrength = 1;
    public float jumpForce = 6;

    // Timers
    public float jumpTimer = 3;
    public float initialJumpTime = 3;
    public float attackTimer = 2;
    public float initialAttackTime = 2;
    public float burnTime = 0;

    public float speed = .5f;
    public float stunTime = 1;
    public float burnDamage = 0;

    public int enemyCost = 50;
    public int lootAmount;

    public GameObject bloodSplash;
    //public GameObject healthBar;
    public GameObject damagePopUp;
    public GameObject lastHit;
    //public GameObject burnEffect;
    //public GameObject popUpText;

    public GameObject[] lootCache;

    public LayerMask layerMask;

    public bool isStunned = false;
    public bool isBurning = false;

    private GameObject player;

    private Rigidbody2D rb2d;

    private Animator anim;

    public GameObject currentHelm;
    public GameObject currentChestPlate;
    public GameObject currentPants;
    public GameObject currentSword;

    //public Shop shop;

    private void Start()
    {
        playerGen = GetComponent<PlayerGenerator>();
        damagePopUp = GameObject.Find("DamagePopUp");
        player = GameObject.Find("Player");
        rb2d = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        lastHit = new GameObject();

        switch (enemyType)
        {
            case EnemyType.BASIC:
                break;
            case EnemyType.BOSS:
                //GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


                //Armor helm = shop.helms[Random.Range((gameManager.currentLevel > 20? 9: 0) + (gameManager.difficultyLevel/2 - 1), (gameManager.currentLevel > 20 ? 9 : 0) + (gameManager.currentBlockLevel/2 - 1) < shop.helms.Length ? 
                //    (gameManager.currentLevel > 20 ? 9 : 0) + (gameManager.currentBlockLevel/2 - 1) : shop.helms.Length)];
                //Armor chestPlate = shop.chestPlates[Random.Range((gameManager.currentLevel > 20 ? 9 : 0) + (gameManager.difficultyLevel / 2 - 1), (gameManager.currentLevel > 20 ? 9 : 0) + (gameManager.currentBlockLevel/2 - 1) < shop.helms.Length ? 
                //    (gameManager.currentLevel > 20 ? 9 : 0) + (gameManager.currentBlockLevel/ 2 - 1) : shop.chestPlates.Length)];
                
                //Armor pants = shop.pants[Random.Range((gameManager.currentLevel > 20 ? 8 : 0) + (gameManager.difficultyLevel / 2 - 1) < shop.pants.Length - 1 ? (gameManager.currentLevel > 20 ? 8 : 0) + (gameManager.difficultyLevel / 2 - 1) : 8 , 
                //    (gameManager.currentLevel > 20 ? 8 : 0) + (gameManager.currentBlockLevel/ 2 -1) < shop.pants.Length ?
                //    (gameManager.currentLevel > 20 ? 8 : 0) + (gameManager.currentBlockLevel/2 - 1) : shop.pants.Length)];

                //Debug.Log("length: " + (gameManager.difficultyLevel / 2 - 1));
                //Debug.Log("Sword length: " + shop.swords.Length);
                //Sword sword = shop.swords[Random.Range((gameManager.currentLevel > 20 ? 9 : 0) + (gameManager.difficultyLevel / 2 - 1) < shop.swords.Length ? (gameManager.currentLevel > 20 ? 9 : 0) + (gameManager.difficultyLevel / 2 - 1) : shop.swords.Length,
                //    (gameManager.currentLevel > 20 ? 9 : 0) + (gameManager.currentBlockLevel/ 2 - 1) < shop.swords.Length ?
                //    (gameManager.currentLevel > 20 ? 9 : 0) + (gameManager.currentBlockLevel/2 - 1) : shop.swords.Length)];

                //currentHelm.GetComponent<SpriteRenderer>().sprite = helm.gameObject.GetComponent<SpriteRenderer>().sprite;
                //currentHelm.GetComponent<SpriteRenderer>().color = helm.gameObject.GetComponent<SpriteRenderer>().color;

                //currentChestPlate.GetComponent<SpriteRenderer>().sprite = chestPlate.gameObject.GetComponent<SpriteRenderer>().sprite;
                //currentChestPlate.GetComponent<SpriteRenderer>().color = chestPlate.gameObject.GetComponent<SpriteRenderer>().color;

                //currentPants.GetComponent<SpriteRenderer>().sprite = pants.gameObject.GetComponent<SpriteRenderer>().sprite;
                //currentPants.GetComponent<SpriteRenderer>().color = pants.gameObject.GetComponent<SpriteRenderer>().color;

                //currentSword.GetComponent<SpriteRenderer>().sprite = sword.gameObject.GetComponent<SpriteRenderer>().sprite;
                //currentSword.GetComponent<SpriteRenderer>().color = sword.gameObject.GetComponent<SpriteRenderer>().color;

                //currentHelm.name = helm.name;
                //currentPants.name = pants.name;
                //currentChestPlate.name = chestPlate.name;
                //currentSword.name = sword.name;

                //attackDamage += (int)sword.attackDamage;
                //if (gameManager.currentLevel <= 40 && attackDamage > 200) 
                //{
                //    attackDamage = 200;
                //}
                //currentHealth += helm.health;
                //maxHealth += helm.health;

                //armor += (int)chestPlate.defense;

                //if (speed + pants.speed <= 1)
                //{
                //    speed += pants.speed;
                //}
                //else
                //{
                //    speed = 1;
                //}
                //jumpForce += pants.speed;

               

                break;
        }
    }


    private void Update()
    {
        if (enemyType == EnemyType.BOSS)
        {
            if (currentChestPlate.name.Contains("Full_"))
            {

                currentChestPlate.transform.localPosition = new Vector3(0, -0.028f, 0);
            }
            if (currentHelm.name.Contains("Full_"))
            {
                Debug.Log("Has full helm");
                currentHelm.transform.localPosition = new Vector3(-0.014f, 0.084f, 0);
                currentHelm.transform.localScale = new Vector3(1.12f, 1.12f, 1f);
            }
            if (currentSword.name.Contains("_2"))
            {
                currentSword.transform.localRotation = Quaternion.Euler(0, 0, 45);
            }
        }
        if (currentHealth <= 0)
        {
            Die();
        }

       // healthBar.transform.localScale = new Vector3((currentHealth / maxHealth) / 2, .15f, 1);

        if (isBurning)
        {
            burnTime -= Time.deltaTime;
            if (burnTime <= 0)
            {
                isBurning = false;
                StopCoroutine("Burn");
                burnTime = 0;
            }
        }

    }

    private void FixedUpdate()
    {
        jumpTimer -= Time.deltaTime;

        if (!isStunned)
        {
            DecideMovement();
        }
        else if (isStunned)
        {
            stunTime -= Time.deltaTime;

            if (stunTime <= 0)
            {
                stunTime = 1;
                isStunned = false;
            }
        }
    }

    public void DecideMovement()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position, miningDistance, layerMask);
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);

        if (ray && Vector2.Distance(ray.collider.gameObject.transform.position, transform.position) <= miningDistance && transform.position.y < player.transform.position.y)
        {
            playerGen.isWalking = false;//anim.SetBool("Walking", false);
            MineBlock(ray.collider.gameObject);

        }
        else if (ray && Vector2.Distance(ray.collider.gameObject.transform.position, transform.position) <= miningDistance &&
            player.transform.position.y < transform.position.y && (Mathf.Abs(player.transform.position.x - transform.position.x) < .05f))
        {
            playerGen.isWalking = false;//anim.SetBool("Walking", false);
            MineBlock(ray.collider.gameObject);
        }
        else
        {

            if (transform.localEulerAngles.y == 180)
            {
                RaycastHit2D leftRay = Physics2D.Raycast(transform.position, Vector2.left, .15f, layerMask);

                Debug.DrawRay(transform.position, Vector2.left, Color.red);
                if (leftRay && player.transform.position.y > transform.position.y)
                {
                    playerGen.isWalking = false;//anim.SetBool("Walking", false);

                    
                }
                else if (leftRay && player.transform.position.y < transform.position.y)
                {
                    playerGen.isWalking = false;//anim.SetBool("Walking", false);
                    MineBlock(leftRay.collider.gameObject);
                }
                else if (jumpTimer < 0)
                {
                    RandomJump();

                }
                else
                {
                    MoveTowardPlayer();
                }
            }
            else
            {
                Debug.DrawRay(transform.position, Vector2.right, Color.red);
                RaycastHit2D rightRay = Physics2D.Raycast(transform.position, Vector2.right, .15f, layerMask);

                if (rightRay && player.transform.position.y > transform.position.y)
                {
                    playerGen.isWalking = false;//anim.SetBool("Walking", false);
                   
                }
                else if (rightRay && player.transform.position.y < transform.position.y)
                {
                    playerGen.isWalking = false;//SetBool("Walking", false);
                    MineBlock(rightRay.collider.gameObject);
                }
                else if (jumpTimer < 0)
                {
                    RandomJump();

                }
                else
                {
                    MoveTowardPlayer();
                }
            }

        }

    }

    public void RandomJump()
    {
        if (player.transform.position.y > transform.position.y)
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        jumpTimer = Random.Range(1, initialJumpTime);

    }

    public void MoveTowardPlayer()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        playerGen.isWalking = true;//anim.SetBool("Walking", true);
        Vector2 dir = (player.transform.position - transform.position).normalized;

        rb2d.velocity = new Vector2(dir.x * speed, rb2d.velocity.y);
    }

    public void Walking()
    {
        Texture2D myTex = playerGen.spriteRen.sprite.texture;

        if (playerGen.walkingSpeed <= 0)
        {
            playerGen.DrawPixel(10, 1, Color.clear);
            playerGen.DrawPixel(40, 1, Color.clear);

            playerGen.DrawPixel(20, 1, playerGen.shoesBright);
            playerGen.DrawPixel(30, 1, playerGen.shoesDark);
        }

        if (playerGen.walkingSpeed > 0)
        {
            playerGen.DrawPixel(10, 1, playerGen.shoesBright);
            playerGen.DrawPixel(40, 1, playerGen.shoesDark);

            playerGen.DrawPixel(20, 1, Color.clear);
            playerGen.DrawPixel(30, 1, Color.clear);
        }

        if (playerGen.walkingSpeed <= -.5f)
        {
            playerGen.walkingSpeed = .5f;
        }
        myTex.Apply();

        playerGen.spriteRen.sprite = Sprite.Create(myTex, new Rect(1, 1, playerGen.mytex.width - 1, playerGen.mytex.height - 1), new Vector2(.5f, .5f));

    }


    public void MineBlock(GameObject block)
    {

        Block blockScript = block.GetComponent<Block>();

        if (blockScript)
            blockScript.SetHealth(-miningStrength, gameObject);

        if (block.transform.position.x < transform.position.x)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void Die()
    {
        if (gameObject.name == "RedDragon")
        {

        }
        else if (gameObject.name == "PurpleDragon")
        {

        }
        else
        {

        }

        for (int i = 0; i < lootAmount; i++)
        {
           // GameObject newItem = Instantiate(lootCache[Random.Range(0, lootCache.Length)], transform.position, Quaternion.identity, GameManager.blockCleaner.transform);
        }

       // player.GetComponent<Player>().UpdateScore(deathValue);
       // player.GetComponent<Player>().achieve.EnemiesKilled(gameObject.name, 1);
        //player.GetComponent<Player>().gameManager.enemiesKilledThisLevel++;
        Destroy(gameObject);
    }

    public void SetHealth(float value)
    {
        currentHealth += value;
    }

    public void Stun(float stunTime)
    {
        isStunned = true;
        stunTime *= stunTime;
        attackTimer = Random.Range(1, initialAttackTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Cred")
        {
            if (attackTimer < 0)
            {
                // Play Sound
                //GameController.control.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                //GameController.control.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();
                try
                {
                    //anim.SetTrigger("SwingWeapon");
                }
                catch
                {

                }

                if (Random.Range(0, collision.gameObject.GetComponent<Player>().block) < shieldBreak)
                {
                    float attDamage = Random.Range(-attackDamage + 5, -attackDamage) + collision.gameObject.GetComponent<Player>().armor;
                    collision.gameObject.GetComponent<Player>().SetHealth((int)attDamage <= 0 ? (int)attDamage : -1);
                    Instantiate(collision.gameObject.GetComponent<Player>().bloodSplash, collision.gameObject.transform.position, Quaternion.identity);
                    GameObject dpp = Instantiate(damagePopUp, new Vector3(collision.gameObject.transform.localPosition.x, collision.gameObject.transform.localPosition.y + .1f,
                        collision.gameObject.transform.localPosition.z), Quaternion.identity, collision.gameObject.transform);
                    dpp.GetComponent<TextMeshPro>().text = "" + (attDamage <= 0 ? attDamage : -1);

                    if (transform.localEulerAngles.y == 180)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-attackForce.x, attackForce.y), ForceMode2D.Impulse);
                    }
                    else
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(attackForce, ForceMode2D.Impulse);
                    }

                
                    attackTimer = Random.Range(1, initialAttackTime);
                }
               
                else
                {
                    GameObject dpp = Instantiate(damagePopUp, new Vector3(collision.gameObject.transform.localPosition.x, collision.gameObject.transform.localPosition.y + .1f,
                       collision.gameObject.transform.localPosition.z), Quaternion.identity, collision.gameObject.transform);
                    dpp.GetComponent<TextMeshPro>().text = "BLOCK";

                    attackTimer = Random.Range(1, initialAttackTime);

                    if (transform.localEulerAngles.y == 180)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-attackForce.x, attackForce.y), ForceMode2D.Impulse);
                    }
                    else
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(attackForce, ForceMode2D.Impulse);
                    }

                    attackTimer = Random.Range(1, initialAttackTime);
                }

            }
        }


    }

    public void ApplyBurn(float burnTime, float burnDamage, bool isBurning)
    {
        this.burnTime = burnTime;
        this.isBurning = isBurning;
        this.burnDamage = burnDamage;
        //GameObject burnEff = Instantiate(burnEffect, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        StartCoroutine("Burn");
    }

    IEnumerator Burn()
    {
        for (; ; )
        {
      
                Debug.Log("Burn applied");
              // GameObject popUp =  Instantiate(popUpText, gameObject.transform.position + new Vector3(0,.1f,0), Quaternion.identity, gameObject.transform);
            SetHealth(-burnDamage);
           // popUp.GetComponent<TextMeshPro>().text = "" + burnDamage;

            yield return new WaitForSeconds(.75f);
        }

        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isStunned && collision.gameObject.name == "Cred")
            attackTimer -= Time.deltaTime;

        if (collision.gameObject.name == "Cred")
        {
            if (attackTimer < 0)
            {
                // Play Sound
//GameController.control.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                //GameController.control.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().Play();

                try
                {
                    //anim.SetTrigger("SwingWeapon");
                }
                catch
                {

                }


                if (Random.Range(0, collision.gameObject.GetComponent<Player>().block) < shieldBreak)
                {
                    float attDamage = Random.Range(-attackDamage + 5, -attackDamage) + collision.gameObject.GetComponent<Player>().armor;
                    collision.gameObject.GetComponent<Player>().SetHealth((int)attDamage <= 0 ? (int)attDamage : -1);
                    Instantiate(collision.gameObject.GetComponent<Player>().bloodSplash, collision.gameObject.transform.position, Quaternion.identity);
                    GameObject dpp = Instantiate(damagePopUp, new Vector3(collision.gameObject.transform.localPosition.x, collision.gameObject.transform.localPosition.y + .1f,
                        collision.gameObject.transform.localPosition.z), Quaternion.identity, collision.gameObject.transform);
                    dpp.GetComponent<TextMeshPro>().text = "" + (attDamage <= 0 ? attDamage : -1);

                    if (transform.localEulerAngles.y == 180)
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-attackForce.x, attackForce.y), ForceMode2D.Impulse);
                    }
                    else
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(attackForce, ForceMode2D.Impulse);
                    }

                    attackTimer = Random.Range(1, initialAttackTime);
                }
                else
                {
                    GameObject dpp = Instantiate(damagePopUp, new Vector3(collision.gameObject.transform.localPosition.x, collision.gameObject.transform.localPosition.y + .1f,
                      collision.gameObject.transform.localPosition.z), Quaternion.identity, collision.gameObject.transform);
                    dpp.GetComponent<TextMeshPro>().text = "BLOCK";

                    attackTimer = Random.Range(1, initialAttackTime);

                    if (transform.localEulerAngles.y == 180)
                    { 
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-attackForce.x, attackForce.y), ForceMode2D.Impulse);
                    }
                    else
                    {
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(attackForce, ForceMode2D.Impulse);
                    }

                    attackTimer = Random.Range(1, initialAttackTime);
                }
            }
        }
    }

    public void Attack()
    {
        // Apply damage based on this enemy
    }
}
