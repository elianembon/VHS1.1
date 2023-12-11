using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallerGen : MonoBehaviour
{
    [SerializeField] private Generator generator;
    [SerializeField] private ChangesLightColor lightColor;
    [SerializeField] private DesactiveColLihgt colision;
    void Start()
    {
        if (generator != null)
        {
            generator.Suscribe(lightColor);
            generator.Suscribe(colision);
        }
    }


}
