using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChangesLightColor : MonoBehaviour
{
    public Light2D MyLight;
    private Color originalColor;
    void Start()
    {
        originalColor = MyLight.color;
    }


    public void ChangetoWhite()
    {
        MyLight.color = Color.white;
    }

    public void ChangetoPurple()
    {
        MyLight.color = originalColor;
    }
}
