using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesPositionRandomizer : MonoBehaviour 
{
    public int grid_size = 4; //initial ballpark figure for attaching script; real value is in unity 
    public float grid_width = 1.5f; //width of a unit on grid 
    public List<Transform> cubes; //Transforms describe 3-D location. can have different cubes member in this class because dragged objects in 
                                    //GUI to reference the same thing 

    private List<Vector3> positions = new List<Vector3>();

    // Awake is called before the first frame update. This method sets up all the possible grid locations that 
    // the cubes can occupy. 
    void Awake()
    {
        for (int i = 0; i < grid_size; i++)
            for (int j = 0; j < grid_size; j++)
            {
                var odd = grid_size % 2 == 1;

                var x = odd ?
                    -grid_size / 2 * grid_width + grid_width * i : //negative so start left side and works way right
                    -grid_size / 2 * grid_width + grid_width / 2 + grid_width * i;
                var z = odd ? 
                    -grid_size / 2 * grid_width + grid_width * j :
                    -grid_size / 2 * grid_width + grid_width * j; //negtive starts at bottom
                positions.Add(new Vector3(x, 0, z));
            }
    }

    // ShuffleCubes takes the list of all the ordered possible positions and rearranges them randomly when Shuffle is called. 
    // Then those random positions are assigned to the cubes' positions
    public void ShuffleCubes() 
    {
        positions.Shuffle();

        for (int i = 0; i < cubes.Count; i++)
            cubes[i].position = positions[i];
    }
}
