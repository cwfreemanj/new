﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public LayerMask touchInpMask;
    public GameObject gunStick;
    public GameObject visualGunStick;
    public GameObject canvas;

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;
    private RaycastHit hit;

    private Vector3 lastPos;
    public Vector3 posDifference;
    // Update is called once per frame
    void Update()
    {
        posDifference = transform.position - lastPos;
        lastPos = transform.position;
        //visualGunStick.transform.position += new Vector3(posDifference.x, posDifference.y, 0);
        //gunStick.transform.position += new Vector3(posDifference.x, posDifference.y, 0);


#if UNITY_EDITOR
        
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            {
                touchesOld = new GameObject[touchList.Count];
                touchList.CopyTo(touchesOld);
                touchList.Clear();

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.SphereCast(ray, .05f, out hit, touchInpMask))
                {
                    Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.ScreenToWorldPoint(Input.mousePosition) - Vector3.forward, Color.red);
                    GameObject recipient = hit.transform.gameObject;
                    touchList.Add(recipient);

                    if (Input.GetMouseButtonDown(0))
                    {
                        recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (Input.GetMouseButton(0))
                    {
                        recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                }


                foreach (GameObject g in touchesOld)
                {
                    if (!touchList.Contains(g))
                    {
                        g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }

#endif

            if (Input.touchCount > 0)
            {
                touchesOld = new GameObject[touchList.Count];
                touchList.CopyTo(touchesOld);
                touchList.Clear();

                foreach (Touch touch in Input.touches)
                {

                    Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out hit, touchInpMask))
                    {
                        GameObject recipient = hit.transform.gameObject;
                        touchList.Add(recipient);

                        if (touch.phase == TouchPhase.Began)
                        {
                            recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                        }
                        if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                        {
                            recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                        }
                        if (touch.phase == TouchPhase.Canceled)
                        {
                            recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                        }
                        if (touch.phase == TouchPhase.Ended)
                        {
                            recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                        }


                    }

                }
                foreach (GameObject g in touchesOld)
                {
                    if (!touchList.Contains(g))
                    {
                        g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        
    }
}

