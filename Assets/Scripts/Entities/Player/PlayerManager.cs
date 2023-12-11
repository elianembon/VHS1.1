using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Player, Subject
{
    public static PlayerManager Instance { get; private set; }

    public GameObject blackScreen;
    private float teleportTime = 2f;
    private Animator animator;
    private GameObject currentDoor;
    public float corduramax;


    public AudioSource audioSource;
    public AudioClip Walk;
    public AudioClip Run;
    private bool movement = false;
    private float initialVolume = 1f;

    private List<Observer> _observers;

    private void Start()
    {
        corduramax = 5;
        _stats.CurrentLife = _stats.MaxLife;
        
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        _observers = new List<Observer>();

        audioSource.volume = initialVolume;
        Notify();
    }

    private void Update()
    {
        Moving();
        

        if (Input.GetKeyDown(_moveForward))
        {
            
            animator.SetBool("PressW", false);
            animator.SetBool("PressS", true);
            animator.SetBool("PressA", false);
            animator.SetBool("PressD", false);
            audioSource.Play();

        }

        if (Input.GetKeyDown(_moveBack))
        {
           
            animator.SetBool("PressS", false);
            animator.SetBool("PressW", true);
            animator.SetBool("PressA", false);
            animator.SetBool("PressD", false);
            audioSource.Play();
        }

        if (Input.GetKeyDown(_moveLeft))
        {
            
            animator.SetBool("PressA", true);
            animator.SetBool("PressW", false);
            animator.SetBool("PressS", false);
            animator.SetBool("PressD", false);
            audioSource.Play();
        }

        if (Input.GetKeyDown(_moveRight))
        {

            animator.SetBool("PressD", true);
            animator.SetBool("PressW", false);
            animator.SetBool("PressS", false);
            animator.SetBool("PressA", false);
            audioSource.Play();
        }

        if (Input.GetKeyUp(_moveRight))
        {
            audioSource.Stop();
        }

        if (Input.GetKeyUp(_moveLeft))
        {
            audioSource.Stop();
        }

        if (Input.GetKeyUp(_moveBack))
        {
            audioSource.Stop();
        }

        if (Input.GetKeyUp(_moveForward))
        {
            audioSource.Stop();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentDoor != null)
            {
                Vector3 currentPosition = transform.position;
                Vector3 targetPosition = currentDoor.GetComponent<Doors>().GetDestination().position;
                Vector3 newPosition = new Vector3(targetPosition.x, targetPosition.y, currentPosition.z);

                transform.position = newPosition;
                Camera.instance.UpdateCameraPosition();
            }
        }

        
    }


    public void LooseLife(float damage)
    {    
        TakeDamage(damage);
        corduramax += damage;
        float volumen = Mathf.Clamp01(initialVolume - (corduramax / _stats.MaxLife));
        audioSource.volume = volumen;
        Notify();
    }

    public void GetLife()
    {
        if (_stats.CurrentLife == _stats.MaxLife)
            return;
        else
        {
            TakeLife(25);
            corduramax -= 25;
            Notify();
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
            LooseLife(0.2f);
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
