﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//this script can be found in the Component section under the option Character Set Up 
//Character Handler
[AddComponentMenu("FirstPerson/Stats")]
public class CharacterHandler : MonoBehaviour
{
    public ObjectStats objectstats;
    public GameObject objects;
    #region Variables


    [Header("Character")]
    #region Character 
    public bool alive;
    //bool to tell if the player is alive
    //connection to players character controller
    public CharacterController controller;
    CharacterMovement movement;
    MouseLook look;

    #endregion
    [Header("Health")]
    #region Health
    //max and current health
    public float maxHealth;
    public float curHealth;
    public float healOvertime = 10;
    public float waitingTime = 3;
    public Slider newHealthBar;
    public Slider secondaryHealthBar;
    public float secondaryHealth;
    public float secondaryHealthSpeed = 10;
    public float secondaryTimer;
    public GUIStyle healthBar;



    #endregion
    #region Level and Exp
    [Header("Levels and Exp")]

    //players current level
    public int level;
    //max and current experience 
    public int maxExp;
    public int currentExp;
    #endregion
    [Header("Mana")]
    public float maxMana;
    public float curMana;
    public Slider manaBar;
    public Slider secondaryManaBar;
    [Header("Stamina")]
    public float maxStamina;
    public float curStamina;
    public float staminaTimer = 2f;
    public Slider staminarBar;
    [Header("Stats")]
    public float dexterity = 1f;
    public float constitution = 1f;
    public float wisdom = 1f;
    public float stamina = 100f;

    [Header("Camera Connection")]

    #region MiniMap
    //render texture for the mini map that we need to connect to a camera
    public RenderTexture miniMap;
    #endregion
    #endregion
    public void Awake()
    {
        stamina *= dexterity;
        maxHealth *= constitution;
        maxMana *= wisdom;
        curStamina = maxStamina;
        curMana = maxMana;
        secondaryHealth = curHealth;

        objects = GameObject.FindGameObjectWithTag("PainBox");
        objectstats = objects.GetComponent<ObjectStats>();
    }
    #region Start
    private void Start()
    {

        //set max health to 100
        maxHealth = 100f;
        //set current health to max
        curHealth = maxHealth;
        //make sure player is alive
        alive = true;
        //max exp starts at 60
        maxExp = 60;
        //connect the Character Controller to the controller variable
        controller = GetComponent<CharacterController>();
        maxMana = 1;

    }



    #endregion
    #region Update
    private void Update()
    {
        if (secondaryTimer > 0)
        { secondaryTimer--; }
        if (secondaryTimer < 0)
        { secondaryTimer = 0; }

        if (secondaryHealth > curHealth)
        {
            if (secondaryTimer == 0f)
                secondaryHealth -= Time.deltaTime * secondaryHealthSpeed;
        }
        if (secondaryHealth < curHealth)
        {
            secondaryHealth = curHealth;
        }
        staminarBar.value = curStamina / maxStamina;
        manaBar.value = curMana / maxMana;
        if (curStamina < maxStamina)
        {
            staminaTimer -= Time.deltaTime;
            if (staminaTimer <= 0)
            {
                curStamina++;

            }

        }
        if (curStamina >= maxStamina)
        {
            curStamina = maxStamina;
        }

        if (curHealth < maxHealth)
        {
            waitingTime -= Time.deltaTime;
            if (waitingTime <= 0)
            {
                curHealth += healOvertime;
                waitingTime = 3;
            }
        }

        //if our current experience is greater or equal to the maximum experience
        if (currentExp >= maxExp)
        {
            //then the current experience is equal to our experience minus the maximum amount of experience
            currentExp -= maxExp;
            //our level goes up by one
            level++;
            maxExp += 50;
        }
        curHealth = (int)curHealth;


        //the maximum amount of experience is increased by 50
    }

    #endregion
    #region LateUpdate
    private void LateUpdate()
    {
        newHealthBar.value = curHealth / maxHealth;
        secondaryHealthBar.value = secondaryHealth / maxHealth;
        //if our current health is greater than our maximum amount of health
        if (curHealth > maxHealth)
        {//then our current health is equal to the max health
            curHealth = maxHealth;
        }

        //if current helath is below 0
        if (curHealth < 0)
        {   //make the current health 0
            curHealth = 0;
        }

        if (alive && curHealth == 0)
        {   //alive is false
            alive = false;
            //controller is turned off
            controller.enabled = false;
            //tell console to say kek
            Debug.Log("kek");
        }
    }
    #endregion
    public void TakeDamage()
    {
        secondaryTimer = 50f;
        curHealth -= objectstats.damage;
    }
    #region OnGUI
    void OnGUI()
    {

        //set up our aspect ratio for the GUI elements
        //scrW - 16
        float scrW = Screen.width / 16;
        //scrH - 9
        float scrH = Screen.height / 9;
        #region health
        //GUI Box on screen for the healthbar background
        GUI.Box(new Rect(6f * scrW, 0.25f * scrH, 4 * scrW, 0.5f * scrH), "");
        //GUI Box for current health that moves in same place as the background bar      
        //current Health divided by the posistion on screen and timesed by the total max health
        GUI.Box(new Rect(6f * scrW, 0.25f * scrH, curHealth * (4 * scrW) / maxHealth, 0.5f * scrH), "", healthBar);
        //Taken Health divided by the posistion on screen and timesed by the total max health
        // GUI.Box(new Rect(6f * scrW, 0.25f * scrH, curHealth * (4 * scrW) / maxHealth, 0.5f * scrH), "", healthBar);
        //GUI Box on screen for the experience background
        GUI.Box(new Rect(6f * scrW, 0.75f * scrH, 4 * scrW, 0.25f * scrH), "");
        GUI.Box(new Rect(6f * scrW, 0.75f * scrH, currentExp * (4 * scrW) / maxExp, 0.25f * scrH), "");
        //GUI Box for current experience that moves in same place as the background bar
        //current experience divided by the posistion on screen and timesed by the total max experience
        //GUI Draw Texture on the screen that has the mini map render texture attached
        GUI.DrawTexture(new Rect(13.75f * scrW, 0.25f * scrH, 2 * scrW, 2 * scrH), miniMap);




    }




}
#endregion

#endregion