﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextTips : MonoBehaviour
{
    public string currentLevel;
    public int xPos;
    List<string> textList1 = new List<string>();
    
    Text text;
    float t;
    int i;


    // Start is called before the first frame update
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().name;
        
        text = GameObject.FindWithTag("Text").GetComponent<Text>();
        t = 1;
        textList1.Add("Click to attack.");
        textList1.Add("Now, press Left Shift to slow things down.");
        textList1.Add("You are ready. Move rightwards and fight!");
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (string.Equals(currentLevel, "FirstLevel"))
        {
            if (text.color.a <= 0)
            {
                text.text = textList1[i];
                StartCoroutine(TextFadeIn());
            }
            
            else if (text.color.a >= 0.9)
            {
                if (i == 0)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        StartCoroutine(TextFadeOut());
                        i = i + 1;
                    }
                }
                else if (i == 1)
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        StartCoroutine(TextFadeOut());
                        i = i + 1;
                    }
                }
                else if (i == 2)
                {
                    if (Input.anyKeyDown)
                    {
                        StartCoroutine(TextFadeOut());
                        i = i + 1;
                    }
                }
            }
            
        }
        else if (string.Equals(currentLevel, "SecondLevel"))
        {

        }
    }

    public IEnumerator TextFadeIn()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime/t));
            yield return null;
        }
    }

    public IEnumerator TextFadeOut()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime/t));
            yield return null;
        }
    }
}