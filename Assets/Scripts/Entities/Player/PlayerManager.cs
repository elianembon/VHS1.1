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
                transform.position = currentDoor.GetComponent<Doors>().GetDestination().position;
                Camera.instance.UpdateCameraPosition();
            }
        }
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
        TakeDamage(0.2f);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage"))
        {
            LooseLife();
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
