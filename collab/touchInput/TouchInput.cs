using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public GameObject leftJoystick;
    public GameObject rightJoystick;
    public GameObject player;
    CircleCollider2D leftCircle;
    CircleCollider2D rightCircle;

    Vector2 lastMoveTouchPos = new Vector2(0,0);
    Vector2 lastGunTouchPos = new Vector2(0, 0);
    Touch moveJoystick;
    Touch gunJoystick;
    bool moveJoystickSet = false;
    bool gunJoystickSet = false;

    void Start()
    {
        leftCircle = leftJoystick.GetComponent<CircleCollider2D>();
        rightCircle = rightJoystick.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player.GetComponent<Rigidbody2D>().MovePosition(MovePlayer());
        ShootGun();


    }

    Vector2 MovePlayer()
    {
        if (Input.touchCount > 0 && leftCircle.bounds.Contains(Camera.main.ScreenToWorldPoint(moveJoystick.position)))
        {
            leftJoystick.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
            if (moveJoystickSet)
            {
                
            } 
            else if (!moveJoystickSet)
            {
                if (gunJoystickSet)
                {
                    moveJoystick = Input.GetTouch(1);
                }
                else
                {
                    moveJoystick = Input.GetTouch(0);
                }
            }

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(moveJoystick.position);
            Vector2 dir = (lastMoveTouchPos - touchPos).normalized;
            lastMoveTouchPos = touchPos;
            if (moveJoystick.phase == TouchPhase.Ended)
            {
                moveJoystickSet = false;
                moveJoystick = new Touch();
            }

            return dir;

        }else
        {
            leftJoystick.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        }
        return new Vector2(0,0);
    }
    public void ShootGun()
    {
        if (Input.touchCount > 0 && rightCircle.bounds.Contains(Camera.main.ScreenToWorldPoint(gunJoystick.position)))
        {
            rightJoystick.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
            if (gunJoystickSet)
            {

            }
            else if (!gunJoystickSet)
            {
                if (moveJoystickSet)
                {
                    gunJoystick = Input.GetTouch(1);
                }
                else
                {
                    gunJoystick = Input.GetTouch(0);
                }
            }

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(gunJoystick.position);
            Vector2 dir = (lastGunTouchPos - touchPos).normalized;
            lastGunTouchPos = touchPos;
            if (gunJoystick.phase == TouchPhase.Ended)
            {
                gunJoystickSet = false;
                gunJoystick = new Touch();
            }

            //return dir;

        }
        else
        {
            rightJoystick.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
        }
        //return new Vector2(0, 0);
    }
}
