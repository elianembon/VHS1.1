using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GeneratorManager;

public class Player : MonoBehaviour, IMovable, ILife
{
    #region STRATEGY
    public float MovementSpeed => _movementSpeed;
    public float MovementSpeedWalk => _movementSpeedWalk;
    public float MovementSpeedRun => _movementSpeedRun;

    //public delegate void PlayerDead();
    //public static event PlayerDead OnPlayerDead;

    public EventHandler PlayerDead;

    public float MaxLife => _maxLife;

    public float CurrentLife => _currentLife;

    #endregion

    #region PRIVATE_PROPIETIES
    [SerializeField] private float _movementSpeed;
    
    [SerializeField] protected float _movementSpeedWalk = 3f;
    [SerializeField] protected float _movementSpeedRun = 8f;
    [SerializeField] protected float _movementSpeedSilent = 1f;

    [SerializeField] protected float _maxLife = 120;
    [SerializeField] protected float _currentLife;

    #endregion

    #region KEY_BINDINGS
    /*--------- [KEY BINDINGS] ---------*/
     protected KeyCode _moveForward = KeyCode.W;
     protected KeyCode _moveBack = KeyCode.S;
     protected KeyCode _moveLeft = KeyCode.A;
     protected KeyCode _moveRight = KeyCode.D;

     protected KeyCode _attack = KeyCode.Z;

    [SerializeField] protected KeyCode _moveRun = KeyCode.LeftShift;
    [SerializeField] protected KeyCode _moveSilent = KeyCode.LeftControl;
    #endregion

    #region METHODS

    /*--------- [MOVEMENT] ---------*/
    public virtual void Move(Vector3 direction) => transform.position += direction.normalized* Time.deltaTime* _movementSpeed;
    public virtual void Moving()
    {
        Vector2 movement = new Vector2();

        if (Input.GetKey(_moveForward))
            movement += Vector2.up;
        
        if (Input.GetKey(_moveBack))
            movement += Vector2.down;
        
        if (Input.GetKey(_moveLeft))
            movement += Vector2.left;
        
        if (Input.GetKey(_moveRight))
            movement += Vector2.right;
        

            ChangeSpeed();
        Move(movement);
    }
    public virtual void ChangeSpeed()
    {
        float targetSpeed = _movementSpeedWalk;

        if (Input.GetKey(_moveRun))
            targetSpeed = _movementSpeedRun;

        if (Input.GetKey(_moveSilent))
            targetSpeed = _movementSpeedSilent;

        _movementSpeed = Mathf.Lerp(_movementSpeed, targetSpeed, Time.deltaTime * 5f);
    }

    /*--------- [LIFE] ---------*/
    public virtual void TakeDamage(float damage)
    {
        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Die();
        }
    }

    public virtual void TakeLife(float RegenLife)
    {
        _currentLife += RegenLife;

       
    }

    public void Die()
    {
            

        
        PlayerDead?.Invoke(this, EventArgs.Empty);
        //Destroy(gameObject);
    }

    #endregion
}
