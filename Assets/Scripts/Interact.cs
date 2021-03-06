﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject mainCam;
    public Camera cam;
    public CharacterMovement playerMovement;
    public MouseLook characterLook;
    // Use this for initialization
    void Start()
    {
        //set cursor lock state to locked
        Cursor.lockState = CursorLockMode.Locked;
        //hide cursor
        Cursor.visible = false;
        //finding by name, creates a problem when you are looking for a specific object sharing the same name.
        player = GameObject.Find("Player");
        //finding by tag
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        /*two ways of referencing 
        cam = mainCam.GetComponent<Camera>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        */
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        characterLook = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray interact;
                                                               //makes it in the middle.
            interact = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            //when the Raycast hits an object with collidor, Ray cast only work with collider
            RaycastHit hitInfo;
                                          //put f at the end if it is decimal, e.g. 10.5f
            if(Physics.Raycast(interact,out hitInfo, 10))
            {

                //manually creates a region so you can collapse things.
                #region NPC Dialogue
                if (hitInfo.collider.CompareTag("NPC"))
                {   //dialogue = hitinfo check for dialogue
                    Dialogue dlg = hitInfo.transform.GetComponent<Dialogue>();
                    //if player has dialogue
                    if (dlg != null)
                    {
                        //show dialogue
                        dlg.showDialogue = true;
                        //turn off camera look and player movement
                        playerMovement.enabled = false;
                        characterLook.enabled = false;
                        //set the cursor to unlocked
                        Cursor.lockState = CursorLockMode.Confined;
                        //set the cursor to visible
                        Cursor.visible = true;
                        Debug.Log("Talk to NPC");
                    }
                }
               
                #endregion
                #region Chest
                if (hitInfo.collider.CompareTag("Chest"))
                {
                    Debug.Log("Open Chest");
                }

                //content
                #endregion
                #region Item
                if (hitInfo.collider.CompareTag("Item"))
                {
                    Debug.Log("Pick up Item");
                }
                #endregion

            }
        }
    }
}
