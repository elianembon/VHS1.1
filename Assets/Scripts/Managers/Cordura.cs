using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Cordura : MonoBehaviour
{
    private UnityEngine.UI.Slider _slider;
    void Start()
    {
        _slider = GetComponent<UnityEngine.UI.Slider>();
    }


    

    public void changeCurrentCordura (float CurrentCordura)
    {
        if (_slider != null)
        {
            _slider.value = CurrentCordura;
        }
    }

    public void InitBarraCordura (float QuantityCordura)
    {
        
        changeCurrentCordura(QuantityCordura);
    }
}
