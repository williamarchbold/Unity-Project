﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions; //video included
using UnityEngine.UI; //video included

public class Register : MonoBehaviour
{
    public GameObject user_name;
    public GameObject password;
    public GameObject confirm_password;

    private string username_string;
    private string password_string;
    private string confirm_password_string;

    private string form; //will hold all sring variables above
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RegisterButton()
    {
        print("Registration successful!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //if tab is pressed
        {
            if (user_name.GetComponent<InputField>().isFocused) //and you're editing the username field
            {
                password.GetComponent<InputField>().Select(); //go to to password box
            }
            if (password.GetComponent<InputField>().isFocused) 
            {
                confirm_password.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (password_string!="" && password_string!="" && confirm_password_string!="")
            {
                RegisterButton();
            }
        }
        username_string = user_name.GetComponent<InputField>().text;
        password_string = password.GetComponent<InputField>().text;
        confirm_password_string = confirm_password.GetComponent<InputField>().text;
    }
}
