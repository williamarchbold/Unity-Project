using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public Material light_material;
    public Material normal_material;

    private Renderer my_renderer;
    private Vector3 my_transform_position;

    public int my_number = 99; //the button's number. default 99 

    public RobotLogic my_logic; 

    public delegate void ClickEV(int number);

    public event ClickEV onClick; 

    void Awake()
    {
        my_renderer = GetComponent<Renderer>();
        my_renderer.enabled = true;
        my_transform_position = transform.position; //to know where we are at start
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (my_logic.player)
        {
            ClickedColor();
            transform.position = new Vector3(my_transform_position.x, -.2f, my_transform_position.z); //changes shape of object clicked by -.3
            onClick.Invoke(my_number); //tell which button is pressed 
        }
        
    }

    private void OnMouseUp()
    {
        UnClickedColor();
        transform.position = new Vector3(my_transform_position.x, my_transform_position.y, my_transform_position.z); //changes shape of object clicked by -.3

    }

    public void ClickedColor()
    {
        my_renderer.sharedMaterial = normal_material;
    }

    public void UnClickedColor()
    {
        my_renderer.sharedMaterial = light_material;
    }
}
