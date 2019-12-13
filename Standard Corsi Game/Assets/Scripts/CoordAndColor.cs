using UnityEngine;

public class CoordAndColor
{
    public Vector2 coordinates;
    public string color;

    public CoordAndColor(Vector2 coord, string color)
    {
        this.coordinates = coord;
        this.color = color;
    }

    public override string ToString()
    {
        return coordinates + " " + color;
    }
}