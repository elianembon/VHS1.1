using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjetivosCumplidos : MonoBehaviour
{
    
    public TextMeshProUGUI fusiblesInsertados;
    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {

        if (inventory != null)
        {
            fusiblesInsertados.text = inventory.fusibleColocados.ToString();
        }

    }
    
}
