using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BButton : MonoBehaviourPunCallbacks
{
    public static bool bButtonInUse = false;

    public float miningAmplifier = 1;
    public GameObject player;
    //public Player player;
    public GameObject miningArrow;
    public GameObject bPressed;
    private GameObject currentBlock;
    public GameObject pickaxeSkin;
    public GameObject swordSkin;
    public GameObject highlight;
    public LayerMask layer;

    private float miningDistance = .5f;
    public AButton abutt;
    
    Vector3 ogPos;
    Vector3 oldTouch;
   
    private void Start()
    {
        //bPressed = transform.GetChild(0).gameObject;
       
    }
    public void OnPhotonInstantiate(GameObject p)
    {
       // PhotonView view = PhotonView.Find(id);
        player = p;
        Debug.Log("I was just spawned!" + gameObject.name);
    }
    private void Update()
    {
       
        if (!bButtonInUse)
        {
            GetComponent<SphereCollider>().transform.position = transform.parent.position;
            bButtonInUse = false;
            player.GetComponent<Player>().anim.SetBool("Pickaxing", false);
            bPressed.SetActive(false);
            miningArrow.SetActive(false);
        }

        //if (Input.GetMouseButton(0) && !AButton.aButtonInUse)
        //{
        //    bPressed.SetActive(true);
        //    bButtonInUse = true;

        //    player.transform.GetChild(3).gameObject.SetActive(true);

        //    player.transform.GetChild(4).gameObject.SetActive(false);
        //    swordSkin.SetActive(false);
        //    if (player.transform.GetChild(3).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.a == 0)
        //    {
        //        pickaxeSkin.SetActive(true);
        //    }
        //    player.GetComponent<Player>().anim.SetBool("Pickaxing", true);
        //    player.GetComponent<Player>().anim.SetBool("SwingSword", false);

        //    //GetComponent<SphereCollider>().transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //    Vector2 dir = Vector2.ClampMagnitude(Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position, 1.0f);

        //    RaycastHit2D hit2d = Physics2D.Raycast(player.transform.position, dir, miningDistance, layer);

        //    Vector3 pos = Camera.main.WorldToScreenPoint(miningArrow.transform.position);
        //    //Vector3 dir1 = point - pos

        //    float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        //    miningArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //    miningArrow.SetActive(true);

        //    if (hit2d)
        //    {
        //        GameObject temp = hit2d.collider.gameObject;
        //        if (temp.GetComponent<Block>())
        //        {
        //            currentBlock = temp;
        //            Block block = temp.GetComponent<Block>();

        //            block.SetHealth(-10 * player.GetComponent<Player>().miningAmplifier, gameObject);
        //        }
        //    }
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    GetComponent<SphereCollider>().transform.position = transform.parent.position;
        //    bButtonInUse = false;
        //    bPressed.SetActive(false);
        //    player.GetComponent<Player>().anim.SetBool("Pickaxing", false);

        //    player.GetComponent<Player>().anim.SetBool("SwingSword", false);

        //    miningArrow.SetActive(false);
        //}
    }

    void OnTouchDown(Vector3 point)
    {
        ogPos = point;
        oldTouch = point;
        bButtonInUse = true;
        bPressed.SetActive(true);
        highlight.SetActive(true);
    }
    void OnTouchUp()
    {
        GetComponent<SphereCollider>().transform.position = transform.parent.position;
        bButtonInUse = false;
        bPressed.SetActive(false);
        player.GetComponent<Player>().anim.SetBool("Pickaxing", false);

        player.GetComponent<Player>().anim.SetBool("SwingSword", false);

        miningArrow.SetActive(false);
        highlight.SetActive(false);
    }
    void OnTouchStay(Vector3 point)
    {
        bPressed.SetActive(true);
        bButtonInUse = true;

        player.transform.GetChild(3).gameObject.SetActive(true);
 
            player.transform.GetChild(4).gameObject.SetActive(false);

        swordSkin.SetActive(false);
        if (player.transform.GetChild(3).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.a == 0)
        {
            pickaxeSkin.SetActive(true);
        }

        player.GetComponent<Player>().anim.SetBool("Pickaxing", true);
            player.GetComponent<Player>().anim.SetBool("SwingSword", false);
            GetComponent<SphereCollider>().transform.position = new Vector3(point.x, point.y, 0);
            Vector2 dir = Vector2.ClampMagnitude(point - transform.parent.position, 1.0f);

            RaycastHit2D hit2d = Physics2D.Raycast(player.transform.position, dir, miningDistance, layer);

            Vector3 pos = Camera.main.WorldToScreenPoint(miningArrow.transform.position);
            //Vector3 dir1 = point - pos;
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            miningArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            miningArrow.SetActive(true);

            if (hit2d)
            {
                GameObject temp = hit2d.collider.gameObject;
                if (temp.GetComponent<Block>())
                {
                    currentBlock = temp;
                    Block block = temp.GetComponent<Block>();
                    
                    block.SetHealth(-10 * player.GetComponent<Player>().miningAmplifier, gameObject);
                }
            }

        oldTouch = point;

        highlight.SetActive(true);
    }
    void OnTouchExit()
    {
        GetComponent<SphereCollider>().transform.position = transform.parent.position;
        bButtonInUse = false;
        player.GetComponent<Player>().anim.SetBool("SwingSword", false);
        player.GetComponent<Player>().anim.SetBool("Pickaxing", false);
        bPressed.SetActive(false);
        miningArrow.SetActive(false);
        highlight.SetActive(false);
    }

    public GameObject GetCurrentBlock()
    {
        return currentBlock;
    }
}
