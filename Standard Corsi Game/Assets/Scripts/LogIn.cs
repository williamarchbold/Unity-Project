using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class LogIn : MonoBehaviour, IEventSystemHandler 
{
    public GameObject user_name;
    public GameObject password;

    public UnityEvent logged_in;

    private string username_string;
    private string password_string;

    private string[] user_profile;

    public string Username => username_string;

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.Tab)) //if tab is pressed
        {
            if (user_name.GetComponent<InputField>().isFocused) //and you're editing the username field
            {
                password.GetComponent<InputField>().Select(); //go to to password box
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (username_string != "" && password_string != "")
            {
                LoginButton();
            }
        }
        
    }

    public void LoginButton()
    {
        InputField usernameField = user_name.GetComponent<InputField>();
        InputField passwordField = password.GetComponent<InputField>();
        username_string = usernameField.text;
        password_string = passwordField.text;

        string save_path = Application.persistentDataPath + "\\" + username_string + ".txt";
        if (username_string != "")
        {
            if (!System.IO.File.Exists(save_path))
            {
                Debug.LogWarning("Username Invalid");
            }
        }
        // read all lines from file into string array
        user_profile = System.IO.File.ReadAllLines(save_path);

        if (password_string !="")
        {
            string decrpyted_password = "";
            
                
            int i = 1;
            foreach (char c in user_profile[1])
            {
                i++;
                char decrypted_char = (char)(c / i);
                decrpyted_password += decrypted_char.ToString();
            }
            
            if (decrpyted_password == password_string)
            {
                Debug.LogWarning("password correct!");
                logged_in.Invoke(); //allows Unity UI options for hiding Start and login boxes 
                usernameField.text = "";
                passwordField.text = "";
            }
            else
            {
                Debug.LogWarning("incorrect password!");
            }
        }
    }
}


