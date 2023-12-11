using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DesactiveColLihgt : MonoBehaviour, Observer
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

    public void UpdateState(Subject subject)
    {
        if(subject is Generator gen)
        {
            if (gen.isRepaired == true)
            {
                ChangedTagToNoDamage();
            }
            else
                ChangedTagToDamage();
        }
    }
}
