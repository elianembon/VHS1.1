using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botones : MonoBehaviour
{
    public bool posLight;
    public bool negLight;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLight()
    {
        if(posLight == true)
        {
            posLight = false;
            negLight = true;
            Debug.Log("Me cambio -");
        }
        else if(negLight == true)
        {
            negLight = false;
            posLight = true;
        }
    }
}
