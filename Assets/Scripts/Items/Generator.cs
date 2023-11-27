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
    private DesactiveColLihgt changeTag;

    Animator anim;

    private void Start()
    {
        generatorManager = GeneratorManager.Instance;
        generatorManager.RegisterGenerator(this);
        anim = GetComponent<Animator>();
        startTimer = RepairCounter;
        myLight = GetComponent<ChangesLightColor>();
        changeTag = GetComponent<DesactiveColLihgt>();       
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
        
        StartCoroutine(RepairCountdown());
        myLight.ChangetoWhite();
        changeTag.ChangedTagToNoDamage();


    }

    private IEnumerator RepairCountdown()
    {
        while (isRepaired && RepairCounter > 0)
        {
            yield return new WaitForSeconds(1f);
            RepairCounter--;
            anim.SetBool("GenOk", true);
        }

        if(RepairCounter <= 15)
        {
            anim.SetBool("GenOk", false);
            anim.SetBool("GenBroking", true);
        }

        if (RepairCounter == 0)
        {
            NoRepairGenerator();
        }
    }

    public void NoRepairGenerator()
    {
        isRepaired = false;
        anim.SetBool("GenBroke", true);
        generatorManager.UnregisterGenerator(this);
        RepairCounter = startTimer;
        myLight.ChangetoPurple();
        changeTag.ChangedTagToDamage();
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



