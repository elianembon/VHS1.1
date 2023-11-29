using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnLight : MonoBehaviour
{
    public Botones boton1;
    public Botones boton2;
    public bool encendido = false;

    public Image lightP;
    private Image originalLight;


    // Start is called before the first frame update
    void Start()
    {
      
        

    }

    // Update is called once per frame
    void Update()
    {
        originalLight = lightP;

        if (boton1.posLight == true && boton2.posLight == true)
        {
            lightP.gameObject.SetActive(false);
            encendido = true;


            Debug.Log("Me desactivo");
        }
        else
        {
            lightP.gameObject.SetActive(true);
        }
    }
}
