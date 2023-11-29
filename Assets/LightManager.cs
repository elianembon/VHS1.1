using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public TurnLight And;
    public Or Or;
    public Not Not;

    public GameObject puzzle;
    private bool Juego;

    public Gen2 MyGen;
    private bool PuzzleDone = false;

    private AudioSource audioSource;
    public AudioClip Repair;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Juego = false;
        MyGen.OnGeneratorRepaired += HandleGeneratorRepaired;
    }

    // Update is called once per frame
    void Update()
    {

       
        

    }

    private void HandleGeneratorRepaired()
    {
        Puzzle();
        
    }

    private void Resolve()
    {
        MyGen.RepairGenerator();
    }

    public void Puzzle()
    {
        if (!Juego)
        {
            Juego = true;
            puzzle.SetActive(true);
        }
        else
        {
            Juego = false;
            puzzle.SetActive(false);
        }

         if(And.encendido == true && Or.encendido == true && Not.encendido == true)
        {
            Resolve();
        }
    }
}
