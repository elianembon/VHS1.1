using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour, Subject
{
    [SerializeField] string nameGen;
    public bool isRepaired = false;
    private bool canSpaceInp = false;
    private List<Observer> _observers;

    private GeneratorManager generatorManager;
    public float RepairCounter; 
    private float startTimer; // variable para guardar
    private eventManager evMan;

    //private ChangesLightColor myLight;
    //private DesactiveColLihgt changeTag;


    private AudioSource audioSource;
    public AudioClip Repair;
    public AudioClip Broking;
    public AudioClip Broke;
    Animator anim;

    private Coroutine repairRoutine;

    [SerializeField] private Image circularSlider;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        generatorManager = GeneratorManager.Instance;
        generatorManager.RegisterGenerator(this);

        startTimer = RepairCounter;

        _observers = new List<Observer>();
        evMan = FindObjectOfType<eventManager>();

        UpdateUI(); // Actualizamos la UI al inicio por si acaso
    }

    private void Update()
    {
        if (canSpaceInp && !isRepaired)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RepairGenerator();
            }
        }
    }

    public void RepairGenerator()
    {
        if (isRepaired) return;

        isRepaired = true;

        if (repairRoutine != null)
            StopCoroutine(repairRoutine);

        repairRoutine = StartCoroutine(RepairCountdown());

        audioSource.clip = Repair;
        audioSource.Play();

        Notify();
        evMan.SendinteractionsGen(nameGen);
    }

    private IEnumerator RepairCountdown()
    {
        while (RepairCounter > 0)
        {
            yield return new WaitForSeconds(1f);
            RepairCounter--;

            // 3. ACTUALIZAMOS LA UI CADA VEZ QUE BAJA EL CONTADOR
            UpdateUI();

            // --- ESTADOS ---
            if (RepairCounter >= 3)
            {
                anim.SetBool("GenOk", true);
                anim.SetBool("GenBroking", false);
                anim.SetBool("GenBroke", false);
            }
            else if (RepairCounter > 0 && RepairCounter < 3)
            {
                anim.SetBool("GenOk", false);
                anim.SetBool("GenBroking", true);
                anim.SetBool("GenBroke", false);

                if (audioSource.clip != Broking)
                {
                    audioSource.clip = Broking;
                    audioSource.Play();
                }
            }
        }

        NoRepairGenerator();
    }

    public void NoRepairGenerator()
    {
        isRepaired = false;

        anim.SetBool("GenOk", false);
        anim.SetBool("GenBroking", false);
        anim.SetBool("GenBroke", true);

        RepairCounter = startTimer;

        // 4. ACTUALIZAMOS LA UI AL REINICIARSE
        UpdateUI();

        audioSource.clip = Broke;
        audioSource.Play();

        Notify();
    }

    // --- NUEVO MÉTODO PARA ACTUALIZAR LA BARRA ---
    private void UpdateUI()
    {
        if (circularSlider != null)
        {
            // Fill Amount necesita un valor entre 0 y 1. 
            // Dividir el actual entre el máximo nos da el porcentaje exacto.
            circularSlider.fillAmount = RepairCounter / startTimer;
        }
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
        foreach (var observer in _observers)
        {
            observer.UpdateState(subject: this);
        }
    }
}