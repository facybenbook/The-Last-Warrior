﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonControl : MonoBehaviour
{
    private float horizontal;
    private float vertical;

    // default buttons
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode slowKey = KeyCode.LeftShift;
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode pauseKey = KeyCode.Escape;

    // gets the components of the gameObject to call functions
    private Movement movementScript;
    private SlowdownAbility slowdownScript;
    private MeleeAttack attackScript;
    private GameObject pauseScreen;

    void Start() 
    {
        // sets script values
        movementScript = gameObject.GetComponent<Movement>();
        slowdownScript = gameObject.GetComponent<SlowdownAbility>();
        attackScript = gameObject.transform.Find("WeaponHitBox").GetComponent<MeleeAttack>();
        
        pauseScreen = GameObject.FindWithTag("Pause");
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movementScript.move(horizontal, vertical);

        // if jump button is pressed, call jump from the movement script
        if (Input.GetKeyDown(jumpKey))
        {
            movementScript.jump();
        }
        
        if (slowdownScript.slowCooldownCurrent > 1)
        {
            if (Input.GetKeyDown(slowKey))
            {
              slowdownScript.slowdown();
            }
        }

        if (Input.GetKeyUp(slowKey) || slowdownScript.slowCooldownCurrent < 1)
            slowdownScript.normalSpeed();

        if (Input.GetKeyDown(attackKey))
            attackScript.Attack();

        if (Input.GetKeyDown(pauseKey))
        {
            pauseScreen.SetActive(!pauseScreen.activeSelf);
            if (pauseScreen.activeSelf)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
        }
    }
}
