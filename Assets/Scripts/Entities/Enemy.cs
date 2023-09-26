using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerManager sacarVida;
    public Transform jugador; //ref jugador
    public float rangoPersecusion = 10.0f;
    public float velocidad = 5.0f;
    Rigidbody2D rb;

 
private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sacarVida = GameObject.FindObjectOfType<PlayerManager>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sacarVida.LooseLife();                  
        }      
    }

    private void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.position); //calcula distancia entre enemigo y jugador

        if (distancia <= rangoPersecusion)
        {
            //calcula direccion hacia el jugador
            Vector3 direccion = (jugador.position - transform.position).normalized;
            
            //mueve enemigo hacia el jugador
           rb.velocity = direccion * (velocidad * Time.deltaTime);
        }
    }
}