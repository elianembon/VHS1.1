using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM2 : MonoBehaviour
{
    private static GM2 instance;
    private List<Gen2> generators = new List<Gen2>();

    public static GM2 Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterGenerator(Gen2 generator)
    {
        generators.Add(generator);
    }

    public void UnregisterGenerator(Gen2 generator)
    {
        generators.Remove(generator);
    }
}
