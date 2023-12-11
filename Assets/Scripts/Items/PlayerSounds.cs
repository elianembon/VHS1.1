using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour, Observer
{
    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioSource whispers;

    
  
    private void PlayPain()
    {
        sound.Play();
        Debug.Log("repreoduciendo gemidos");
    }

    private void PlayWhispers()
    {
        whispers.Play();
        Debug.Log("Reproduciendo susurros");
    }
    public void UpdateState(Subject subject)
    {
        if(subject is PlayerManager playerManager)
        {
            if(playerManager.corduramax >= 50)
            {
                if (!whispers.isPlaying)
                    PlayWhispers();
            }
            if(!sound.isPlaying)
            {
                PlayPain();
            }    

        }
    }
}
