﻿// not my code. got it here: http://stackoverflow.com/questions/273313/randomize-a-listt/1262619#1262619

using System;
using System.Collections.Generic;
using System.Linq;

public static class ShufflingExtension
{

    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list) //IList represents a non-generic collection of objects that can be individually accessed by index.
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1); //Next returns the random integer with argument n+1 as max value
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
