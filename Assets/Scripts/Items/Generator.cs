using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Generator : MonoBehaviour
{
    private GeneratorManager generatorManager;

    private Stack<GameObject> generatorStack = new();

    private bool canSpaceInp = false;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        generatorManager = GeneratorManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {

        if (canSpaceInp == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Repare");
                SaveGenerator();
                Enemy.rangoPersecusion += 1;
                Enemy.velocidad += 1;
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
        spriteRenderer.color = Color.yellow;
    }

    public void EnableSpaceInput()
    {
        canSpaceInp = true;
    }

    public void DisableSpaceInput()
    {
        canSpaceInp = false;
    }

    public void ChangeSpriteColor(Color newColor)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }
    }
}
