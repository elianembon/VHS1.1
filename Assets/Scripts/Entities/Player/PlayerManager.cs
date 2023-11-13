using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Player
{
    public static PlayerManager Instance { get; private set; }

    public GameObject sword;
    private Animator animator;
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
    
}
