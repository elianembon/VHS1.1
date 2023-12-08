using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnLight : MonoBehaviour
{
    public Botones boton1;
    public Botones boton2;

    public Image lightP;
    private Image originalLight;


    // Start is called before the first frame update
    void Start()
    {
      
        lightP = GetComponent<Image>();
        originalLight = lightP;
    }

    // Update is called once per frame
    void Update()
    {


        if (boton1.posLight == true && boton2.posLight == true)
        {
            lightP.gameObject.SetActive(false);
            Debug.Log("Me desactivo");
        }
        else
        {
            lightP.gameObject.SetActive(true);
        }

        Debug.Log(boton1.posLight);
        Debug.Log(boton2.posLight);
    }
}
