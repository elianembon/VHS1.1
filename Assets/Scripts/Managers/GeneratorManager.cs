using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    private static GeneratorManager instance;
    private List<Generator> generators = new List<Generator>();

    public static GeneratorManager Instance
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

    public void RegisterGenerator(Generator generator)
    {
        generators.Add(generator);
    }

    public void UnregisterGenerator(Generator generator)
    {
        generators.Remove(generator);
    }
}



