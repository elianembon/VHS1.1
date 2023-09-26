using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int maxGen = 4;

    private GeneratorManager generatorManager;

    private Stack<GameObject> generatorStack = new();

    private bool canSpaceInp = false;


    private void Start()
    {
        generatorManager = GeneratorManager.Instance;
    }
    private void Update()
    {
        if (generatorManager.GetGeneratorCount() == maxGen)
        {
            Debug.Log("¡Ganaste!");
        }

        if (canSpaceInp == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Repare");
                SaveGenerator();
            }
        }
        else if (canSpaceInp == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("no estoy cerca");
            }
        }
    }

    private void SaveGenerator()
    {
        generatorManager.PushGenerator(gameObject);
    }

    public void EnableSpaceInput()
    {
        canSpaceInp = true;
    }

    public void DisableSpaceInput()
    {
        canSpaceInp = false;
    }
}
