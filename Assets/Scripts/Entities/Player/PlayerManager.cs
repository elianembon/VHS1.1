using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Player
{
    public static PlayerManager Instance { get; private set; }

    public GameObject blackScreen;
    private float teleportTime = 2f;
    public GameObject sword;
    private Animator animator;
    private GameObject currentDoor;
    [SerializeField] private Cordura cordura;
    

    private void Start()
    {
        _currentLife = _maxLife;
        
        animator = GetComponent<Animator>();

        cordura.InitBarraCordura(_currentLife);
    }

    private void Update()
    {
        Moving();
        

        if (Input.GetKey(_moveForward))
        {
            
            animator.SetBool("PressW", false);
            animator.SetBool("PressS", true);
            animator.SetBool("PressA", false);
            animator.SetBool("PressD", false);
        }

        if (Input.GetKey(_moveBack))
        {
           
            animator.SetBool("PressS", false);
            animator.SetBool("PressW", true);
            animator.SetBool("PressA", false);
            animator.SetBool("PressD", false);
        }

        if (Input.GetKey(_moveLeft))
        {
            
            animator.SetBool("PressA", true);
            animator.SetBool("PressW", false);
            animator.SetBool("PressS", false);
            animator.SetBool("PressD", false);
        }

        if (Input.GetKey(_moveRight))
        {
            
            animator.SetBool("PressD", true);
            animator.SetBool("PressW", false);
            animator.SetBool("PressS", false);
            animator.SetBool("PressA", false);
        }
        Attack();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentDoor != null)
            {
                StartCoroutine(TeleportCoroutine());
            }
        }
    }

    private IEnumerator TeleportCoroutine()
    {
        // Activa la pantalla negra
        blackScreen.SetActive(true);

        // Espera el tiempo de teletransporte
        yield return new WaitForSeconds(teleportTime);

        // Cambia la posición del jugador
        transform.position = currentDoor.GetComponent<Doors>().GetDestination().position;

        // Desactiva la pantalla negra después del teletransporte
        yield return new WaitForSeconds(2f); // Espera adicional de 2 segundos
        blackScreen.SetActive(false);
    }


    private void Attack()
    {
        if (Input.GetKey(_attack))
        {
            Instantiate(sword, transform.position, transform.rotation);
        }
    }

    public void LooseLife()
    {    
        TakeDamage(1);
        cordura.changeCurrentCordura(_currentLife);
    }

    public void GetLife()
    {
        if (_currentLife == _maxLife)
            return;
        else
        {
            TakeLife(1);
            cordura.changeCurrentCordura(_currentLife);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            currentDoor = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            if(collision.gameObject == currentDoor)
            {
                currentDoor = null;
            }
        }
    }

}
