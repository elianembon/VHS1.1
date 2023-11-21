using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DesactiveColLihgt : MonoBehaviour
{
    public GameObject[] objects;

    public void ChangedTagToNoDamage( )
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].tag = "NoDamage";
        }
    }

    public void ChangedTagToDamage()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].tag = "Damage";
        }
    }


}
