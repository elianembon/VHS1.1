using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen2 : MonoBehaviour
{
    public bool isRepaired = false;
    private bool canSpaceInp = false;

    private GM2 generatorManager;
    public float RepairCounter = 60; // Contador inicializado en 60 segundos
    private float startTimer; // variable para guardar

    private ChangesLightColor myLight;
    private DesactiveColLihgt changeTag;


    private AudioSource audioSource;
    public AudioClip Repair;
    public AudioClip Broking;
    public AudioClip Broke;
    Animator anim;

    public delegate void GeneratorRepaired();
    public event GeneratorRepaired OnGeneratorRepaired;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        generatorManager = GM2.Instance;
        generatorManager.RegisterGenerator(this);
        anim = GetComponent<Animator>();
        startTimer = RepairCounter;
        myLight = GetComponent<ChangesLightColor>();
        changeTag = GetComponent<DesactiveColLihgt>();
    }
    private void Update()
    {
        if (canSpaceInp == true && !isRepaired)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //RepairGenerator();
                audioSource.PlayOneShot(Repair);
                OnGeneratorRepaired?.Invoke();
            }
        }
    }

    public void RepairGenerator()
    {
        isRepaired = true;

        StartCoroutine(RepairCountdown());
        myLight.ChangetoWhite();
        changeTag.ChangedTagToNoDamage();
        audioSource.PlayOneShot(Repair);


    }


    private IEnumerator RepairCountdown()
    {
        while (isRepaired && RepairCounter > 0)
        {
            yield return new WaitForSeconds(1f);
            RepairCounter--;
            anim.SetBool("GenOk", true);

        }

        if (RepairCounter <= 15)
        {
            anim.SetBool("GenOk", false);
            anim.SetBool("GenBroking", true);
            audioSource.PlayOneShot(Broking);

        }

        if (RepairCounter > 15)
        {
            audioSource.PlayOneShot(Repair);
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

        audioSource.PlayOneShot(Broke);
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
