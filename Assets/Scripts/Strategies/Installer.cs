using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Installer : MonoBehaviour
{
    [SerializeField] private Cordura _bar1;
    [SerializeField] private Cordura _bar2;
    [SerializeField] private Cordura _bar3;
    [SerializeField] private Cordura _bar4;
    [SerializeField] private PlayerManager playerManager; 
    [SerializeField] private PlayerSounds sounds; 

    private void Start()
    {
        if (playerManager != null)
        {
            playerManager.Suscribe(_bar1);
            playerManager.Suscribe(_bar2);
            playerManager.Suscribe(_bar3);
            playerManager.Suscribe(_bar4);
            playerManager.Suscribe(sounds);
        }
        else
        {
            Debug.LogError("PlayerManager no asignado en el Inspector de Unity en el script Installer.");
        }
        
    }

}
