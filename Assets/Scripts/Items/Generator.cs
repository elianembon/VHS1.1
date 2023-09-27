using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Generator : MonoBehaviour
{
    private GeneratorManager generatorManager;

    private Stack<GameObject> generatorStack = new();

    Enemy enemy;

    private bool canSpaceInp = false;
    private bool isRepaired = false;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        generatorManager = GeneratorManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemy = new();
        GeneratorManager.OnEnemyReduce += ReduceDifficultEnemy;
    }
    private void Update()
    {

        if (canSpaceInp == true && !isRepaired)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Repare");
                SaveGenerator();
                enemy.IncreaseVelocity();
                enemy.IncreaseRange();
            }
        }
        else if (canSpaceInp == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("no estoy cerca o ya estoy reparado");
            }
        }
    }

    private void SaveGenerator()
    {
        generatorManager.PushGenerator(gameObject);
        RepairGenerator();
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

    public void ReduceDifficultEnemy()
    {
        enemy.DecreaseRange();
        enemy.DecreaseVelocity();
    }

    public void RepairGenerator()
    {
        // Lógica para reparar el generador...
        isRepaired = true;
        spriteRenderer.color = Color.yellow;
    }

    public void NoRepairGenerator()
    {
        isRepaired = false;
    }

    public bool IsRepaired()
    {
        return isRepaired;
    }

}
