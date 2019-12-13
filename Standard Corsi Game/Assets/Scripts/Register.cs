// this class is based on https://www.youtube.com/watch?v=vFs0_skd0E4
// however these changed: 
// 1. no inclusion of emails as in video 
// 2. added user name and password checking to encrypted password username
// 3. video also used a bunch of booleans in RegisterButton() but those weren't necessary so took out all references 
// 4. Video saved files by username which violates projects instruction for privacy so changed algorith in EncryptString()
//       to a simple Ceaser cypher to fit a valid path name and still be encrypted. 


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

               
        if (confirm_password_string != password_string)
        {
            Debug.LogWarning("Passwords don't match!");
        }
        else
        {
            string encrypted_password = EncryptString(password_string); //this is not part of the video 
            string encrypted_username = EncryptString(username_string); //

            //string save_path = @"C:\Users\William Archbold\Desktop\Unity Project\" + username_string + ".txt";
            string save_path = Application.persistentDataPath + "\\" + encrypted_username + ".txt";

            form = encrypted_username + System.Environment.NewLine + encrypted_password; //put username and encrypted password into a single string
            System.IO.File.WriteAllText(save_path, form);

            user_name.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confirm_password.GetComponent<InputField>().text = "";

            print("Registration complete!");
        }
    }

 
    public static string EncryptString(string input) 
    {
        string encrypted = "";
        int i = 1;
        foreach (char c in input)
        {
            i++;
            char encrypted_char = ' ';
            if (c == 'z')
                encrypted_char = 'a';
            else if (c == 'Z')
                encrypted_char = 'A';
            else
             encrypted_char = (char)(c + 1); //multiplying by i then get back to char. each char times another number
            encrypted += encrypted_char.ToString();
        }

        return encrypted;
    }
}
