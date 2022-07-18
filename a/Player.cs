using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;

    public int armor = 10;
    public int attack = 10;
    public int speed = 1;
    public int cappedVelocity = 2;
    public Rigidbody2D rb2d;

    public int block = 5;
    public GameObject bloodSplash;
    public GameObject currentBlock;
    public int miningAmplifier = 1;

    public LayerMask layer;
    public float miningDistance = 2;

    public bool canJump = false;
    public float jumpForce = 5;

    public Tilemap tilemap;

    public void SetHealth(int health)
    {
        this.health += health;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = tilemap.WorldToCell(worldPoint);

            TileBase tile = tilemap.GetTile(position);
            Debug.Log(position);
            tilemap.SetTile(position, null);
            //bPressed.SetActive(true);
            //bButtonInUse = true;

            //transform.GetChild(3).gameObject.SetActive(true);

            //transform.GetChild(4).gameObject.SetActive(false);
            //swordSkin.SetActive(false);
            /* if (transform.GetChild(3).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.a == 0)
             {
                 pickaxeSkin.SetActive(true);
             }*/
            //player.GetComponent<Player>().anim.SetBool("Pickaxing", true);
            // player.GetComponent<Player>().anim.SetBool("SwingSword", false);

            //GetComponent<SphereCollider>().transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector2 dir = Vector2.ClampMagnitude(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, 1.0f);

            RaycastHit2D hit2d = Physics2D.Raycast(transform.position, dir, miningDistance, layer);

            //Vector3 pos = Camera.main.WorldToScreenPoint(miningArrow.transform.position);
            //Vector3 dir1 = point - pos

            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            //miningArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
           // miningArrow.SetActive(true);

            if (hit2d)
            {
                GameObject temp = hit2d.collider.gameObject;
                if (temp.GetComponent<Block>())
                {
                    currentBlock = temp;
                    Block block = temp.GetComponent<Block>();

                    block.SetHealth(-10 * miningAmplifier, gameObject);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //GetComponent<SphereCollider>().transform.position = transform.parent.position;
            //bButtonInUse = false;
            //bPressed.SetActive(false);
            //player.GetComponent<Player>().anim.SetBool("Pickaxing", false);

           // player.GetComponent<Player>().anim.SetBool("SwingSword", false);

            //miningArrow.SetActive(false);
        }

        if (Physics2D.OverlapCircle(transform.position + new Vector3(0, -.75f, 0), .1f, layer))
        {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            canJump = false;
            rb2d.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
        }
    }

    public void MovePlayer(Vector2 dir)
    {
        dir.y = 0;

        if (dir.x < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            try
            {
                //currentAfterImage.GetComponent<ParticleSystemRenderer>().flip = new Vector3(1, 0, 0);
            }
            catch
            {

            }
            dir.x = -1;
            //anim.SetBool("Walking", true);
        }
        else if (dir.x > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            try
            {
                //currentAfterImage.GetComponent<ParticleSystemRenderer>().flip = new Vector3(0, 0, 0);
            }
            catch
            {

            }
            dir.x = 1;
            //anim.SetBool("Walking", true);
        }
        else
        {

        }

        if (rb2d.velocity.x < cappedVelocity && rb2d.velocity.x > -cappedVelocity)
            rb2d.velocity += (dir * speed * Time.deltaTime);
    }

}
