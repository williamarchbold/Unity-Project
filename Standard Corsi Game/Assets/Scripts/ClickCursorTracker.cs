using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ClickCursorTracker : MonoBehaviour
{
    public LogIn login;

    //make a safe path to save to my computer
    private string save_path => Application.persistentDataPath + "\\" + Register.EncryptString(login.Username) + "_" +
        "cube_cursor_data.txt";  //where the project is stored. can be .txt not necessarily json
    private List<CoordAndColor> screenCoords = new List<CoordAndColor>();

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    public void CubeClicked(Vector2 mousePos)
    {
        var color = "Cursor not on cube";

        //https://docs.unity3d.com/Manual/CameraRays.html
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            // Do something with the object that was hit by the raycast.

            //cube objects have a collider componenent so they'll be hit by the ray cast and things like the plane don't have collider so won't be deteced
            var mesh = objectHit.GetComponent<MeshRenderer>();
            if (mesh != null)
                color = mesh.material.name;
        }

        screenCoords.Add(new CoordAndColor(Input.mousePosition, color));
    }

    public void WriteSequence(string sequence) 
    {
        using (StreamWriter stream = new StreamWriter(save_path, append: true))
            stream.WriteLine(sequence);

        screenCoords.Clear();
    }

    public void StopTrackingAndSave()
    {
        using (StreamWriter stream = new StreamWriter(save_path, append: true))
        {
            foreach (var coord in screenCoords)
                stream.WriteLine(coord + " " + DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }    
}
