using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Generator;

public class GeneratorManager : MonoBehaviour
{
    private static GeneratorManager instance;

    private Stack<GameObject> generatorStack = new Stack<GameObject>();

    public delegate void GeneratorCountReachedMax();
    public static event GeneratorCountReachedMax OnGeneratorCountReachedMax;

    private float timer = 0f;
    public float maxTimer = 60f;
    private bool isTimerActive = false;
    public static GeneratorManager Instance
    {
        get { return instance; }
    }

    public int maxGeneratorCount = 3;
    private int currentGeneratorCount = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isTimerActive)
        {
            timer += Time.deltaTime;

            if (timer >= maxTimer && generatorStack.Count() > 0)
            {
                GameObject lastGenerator = generatorStack.Pop();
                ChangeSpriteColorOfLastGenerator(lastGenerator);
                PerformAction(lastGenerator);

                ResetTimer();
            }
        }
    }

    public void PushGenerator(GameObject generator)
    {
        generatorStack.Push(generator);
        currentGeneratorCount++;

        if (currentGeneratorCount >= maxGeneratorCount)
        {
            StopTimer();
            if (OnGeneratorCountReachedMax != null)
            {
                OnGeneratorCountReachedMax(); // Dispara el evento
                Enemy.rangoPersecusion -= 1;
                Enemy.velocidad -= 1;
            }
        }
        else if (!isTimerActive)
        {
            StartTimer();
        }
    }

    public int GetCurrentGeneratorCount()
    {
        return currentGeneratorCount;
    }

    private void StartTimer()
    {
        isTimerActive = true;
        timer = 0f;
    }

    private void StopTimer()
    {
        isTimerActive = false;
    }

    private void ResetTimer()
    {
        timer = 0f;
    }

    private void ChangeSpriteColorOfLastGenerator(GameObject generator)
    {
        Generator generatorComponent = generator.GetComponent<Generator>();

        if (generatorComponent != null)
        {
            generatorComponent.ChangeSpriteColor(Color.red);
        }
    }

    private void PerformAction(GameObject generator)
    {
        // Realiza la acción cuando se alcanza el tiempo límite.
        // Puedes implementar tu lógica personalizada aquí.
    }

}

