using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public bool isRepaired = false;
    private bool canSpaceInp = false;

    private GeneratorManager generatorManager;
    public float RepairCounter = 60; // Contador inicializado en 60 segundos
    private float startTimer; // variable para guardar

    private ChangesLightColor myLight;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        generatorManager = GeneratorManager.Instance;
        generatorManager.RegisterGenerator(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
        startTimer = RepairCounter;
        myLight = GetComponent<ChangesLightColor>();
    }
    private void Update()
    {
        if(canSpaceInp == true && !isRepaired)
        {
            Debug.Log("puedo reparar");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RepairGenerator();
                Debug.Log("repare");
            }
        }
    }

    public void RepairGenerator()
    {
        isRepaired = true;
        spriteRenderer.color = Color.yellow;
        StartCoroutine(RepairCountdown());
        myLight.ChangetoWhite();
    }

    private IEnumerator RepairCountdown()
    {
        while (isRepaired && RepairCounter > 0)
        {
            yield return new WaitForSeconds(1f);
            RepairCounter--;
        }

        if (RepairCounter == 0)
        {
            NoRepairGenerator();
        }
    }

    public void NoRepairGenerator()
    {
        isRepaired = false;
        spriteRenderer.color = Color.red;
        generatorManager.UnregisterGenerator(this);
        RepairCounter = startTimer;
        myLight.ChangetoPurple();
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



