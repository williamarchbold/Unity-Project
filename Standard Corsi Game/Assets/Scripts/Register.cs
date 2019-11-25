// this class is based on however modifcations were made. 
//https://www.youtube.com/watch?v=FBo9OdEF_D4&t=21s

using System.Collections;
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
            if (username_string!="" && password_string!="" && confirm_password_string!="")
            {
                RegisterButton();
            }
        }
    }

    public void RegisterButton()
    {

        InputField usernameField = user_name.GetComponent<InputField>(); //video had this info in update, but bad for memory. update occurs all thetime
        InputField passwordField = password.GetComponent<InputField>();
        InputField confirm_password_field = confirm_password.GetComponent<InputField>();
        username_string = usernameField.text;
        password_string = passwordField.text;
        confirm_password_string = confirm_password_field.text;


        //string save_path = @"C:\Users\William Archbold\Desktop\Unity Project\" + username_string + ".txt";
        string save_path = Application.persistentDataPath + "\\" + username_string + ".txt";

        if (username_string != null)
        {
            if (!System.IO.File.Exists(save_path)) //stream writer will write file automatically if it doesn't exist. https://answers.unity.com/questions/990496/ioexception-sharing-violation-on-path-please-help.html
            {
                //System.IO.File.Create(save_path);
            }
        }
        if (confirm_password_string != password_string)
        {
            Debug.LogWarning("Passwords don't match!");
        }
        else
        {
            bool password_is_clear = true;
            string encrypted_password = "";
            int i = 1;
            foreach (char c in password_string)
            {
                if (password_is_clear)
                {
                    password_string = "";
                    password_is_clear = false;
                }
                i++;
                char encrypted_char = (char)(c * i); //multiplying by i then get back to char. each char times another number
                encrypted_password += encrypted_char.ToString();
            }
            form = username_string + System.Environment.NewLine + encrypted_password; //put username and encrypted password into a single string
            System.IO.File.WriteAllText(save_path, form);

            user_name.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confirm_password.GetComponent<InputField>().text = "";

            print("Registration complete!");
        }
    }
}
