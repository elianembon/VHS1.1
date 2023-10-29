using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        Player.OnPlayerDead += ActivedScene;
    }

    public void ActivedScene()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
