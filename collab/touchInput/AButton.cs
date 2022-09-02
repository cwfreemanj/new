using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AButton : MonoBehaviour
{
    public static bool aButtonInUse = false;

    public GameObject player;
    public float jumpForce = .1f;
    private GameObject aPressed;
    public GameObject swordSkin;
    public GameObject pickaxeSkin;
    public GameObject highlight;

    private void Start()
    {
        aPressed = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        //if (Input.GetKey("space"))
        //{
        //    aButtonInUse = true;
        //    aPressed.SetActive(true);
        //    player.transform.GetChild(4).gameObject.SetActive(true);
        //    player.transform.GetChild(3).gameObject.SetActive(false);

        //    player.GetComponent<Player>().anim.SetBool("SwingSword", true);
        //    player.GetComponent<Player>().anim.SetBool("Pickaxing", false);

        //    if (player.transform.GetChild(4).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.a == 0)
        //    {
        //        swordSkin.SetActive(true);
        //    }
        //}
        //else if (Input.GetKeyUp("space"))
        //{
        //    aPressed.SetActive(false);
        //    aButtonInUse = false;
        //    player.GetComponent<Player>().anim.SetBool("SwingSword", false);
        //    player.GetComponent<Player>().anim.SetBool("Pickaxing", false);
        //}
    }

    void OnTouchDown(Vector3 point)
    {
        //player.GetComponent<Player>().Jump(jumpForce);
        aPressed.SetActive(true);
        aButtonInUse = true;
        // Disable pickaxe/pickaxe skin and enable sword/swordskin
        player.transform.GetChild(4).gameObject.SetActive(true);
        player.transform.GetChild(3).gameObject.SetActive(false);
        pickaxeSkin.SetActive(false);
        if (player.transform.GetChild(4).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.a == 0)
        {
            swordSkin.SetActive(true);
        }
        player.GetComponent<Player>().anim.SetBool("SwingSword", true);
            player.GetComponent<Player>().anim.SetBool("Pickaxing", false);

        highlight.SetActive(true);
        

    }
    void OnTouchUp()
    {
        aPressed.SetActive(false);
        aButtonInUse = false;
        player.GetComponent<Player>().anim.SetBool("SwingSword", false);
        player.GetComponent<Player>().anim.SetBool("Pickaxing", false);
        highlight.SetActive(false);
    }
    void OnTouchStay(Vector3 point)
    {
        aButtonInUse = true;
        aPressed.SetActive(true);
        player.transform.GetChild(4).gameObject.SetActive(true);
        player.transform.GetChild(3).gameObject.SetActive(false);
        pickaxeSkin.SetActive(false);
        if (player.transform.GetChild(4).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.a == 0)
        {
            swordSkin.SetActive(true);
        }

        player.GetComponent<Player>().anim.SetBool("SwingSword", true);
            player.GetComponent<Player>().anim.SetBool("Pickaxing", false);

        highlight.SetActive(true);

    }
    void OnTouchExit()
    {
        aButtonInUse = false;
        aPressed.SetActive(false);
        player.GetComponent<Player>().anim.SetBool("SwingSword", false);
        player.GetComponent<Player>().anim.SetBool("Pickaxing", false);
        highlight.SetActive(false);
    }
}
