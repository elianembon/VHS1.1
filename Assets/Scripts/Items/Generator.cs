using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour, Subject
{
    [SerializeField] string nameGen;
    public bool isRepaired = false;
    private bool canSpaceInp = false;
    private List<Observer> _observers;

    private GeneratorManager generatorManager;
    public float RepairCounter = 60; // Contador inicializado en 60 segundos
    private float startTimer; // variable para guardar
    private eventManager evMan;

    //private ChangesLightColor myLight;
    //private DesactiveColLihgt changeTag;


    private AudioSource audioSource;
    public AudioClip Repair;
    public AudioClip Broking;
    public AudioClip Broke;
    Animator anim;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        generatorManager = GeneratorManager.Instance;
        generatorManager.RegisterGenerator(this);
        anim = GetComponent<Animator>();
        startTimer = RepairCounter;
        //myLight = GetComponent<ChangesLightColor>();
        //changeTag = GetComponent<DesactiveColLihgt>();
        _observers = new List<Observer>();
        evMan = FindObjectOfType<eventManager>();
    }
    private void Update()
    {
        if(canSpaceInp == true && !isRepaired)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RepairGenerator();
                audioSource.PlayOneShot(Repair);
            }
        }
    }

    public void RepairGenerator()
    {
        isRepaired = true;
        
        StartCoroutine(RepairCountdown());
        //myLight.ChangetoWhite();
        //changeTag.ChangedTagToNoDamage();
        audioSource.PlayOneShot(Repair);
        Notify();
        evMan.SendinteractionsGen(nameGen);


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
        //myLight.ChangetoPurple();
        //changeTag.ChangedTagToDamage();
        Notify();
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

    public void Suscribe(Observer observer)
    {
        _observers.Add(observer);
    }

    public void Unsuscribe(Observer observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach(var observer in _observers)
        {
            observer.UpdateState(subject: this);
        }
    }
}



