using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChangesLightColor : MonoBehaviour
{
    public Light2D[] Lights;
    private Color[] originalColors;

    void Start()
    {
        // Almacena los colores originales de todas las luces
        originalColors = new Color[Lights.Length];
        for (int i = 0; i < Lights.Length; i++)
        {
            originalColors[i] = Lights[i].color;
        }
    }

    public void ChangetoWhite()
    {
        // Cambia el color de todas las luces al blanco
        for (int i = 0; i < Lights.Length; i++)
        {
            Lights[i].color = Color.white;
        }
    }

    public void ChangetoPurple()
    {
        // Restaura el color original de todas las luces
        for (int i = 0; i < Lights.Length; i++)
        {
            Lights[i].color = originalColors[i];
        }
    }
}
