using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public int Level { get; set; }
    public int Difficulty { get; set; }

    public Levels(int levels, int difficulty)
    {
        this.Difficulty = difficulty;
        this.Level = levels;
    }
}
