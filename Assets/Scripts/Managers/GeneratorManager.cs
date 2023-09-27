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

    public delegate void EnemyReduce();
    public static event EnemyReduce OnEnemyReduce;

    private float timer = 0f;
    public float maxTimer = 60f;
    private bool isTimerActive = false;
    public static GeneratorManager Instance
    {
        get { return instance; }
    }

    public int maxGeneratorCount = 3;
    private int currentGeneratorCount = 0;
    private int repairedGeneratorCount = 0; // Contador para los generadores reparados

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
                Generator generatorComponent = lastGenerator.GetComponent<Generator>();
                generatorComponent.NoRepairGenerator();
                ChangeSpriteColorOfLastGenerator(lastGenerator);
                GeneratorRemoved(lastGenerator);
                PerformAction();

                ResetTimer();
            }
        }
    }

    public void PushGenerator(GameObject generator)
    {
        generatorStack.Push(generator);
        currentGeneratorCount++;

        // Incrementa el contador si el generador se repara
        Generator generatorComponent = generator.GetComponent<Generator>();
        if (generatorComponent != null && generatorComponent.IsRepaired())
        {
            repairedGeneratorCount++;
        }

        // Verifica si se alcanza la cantidad requerida
        if (repairedGeneratorCount >= maxGeneratorCount)
        {
            StopTimer();
            if (OnGeneratorCountReachedMax != null)
            {
                OnGeneratorCountReachedMax(); // Dispara el evento
            }
        }
        else if (!isTimerActive)
        {
            StartTimer();
        }
    }

    public void GeneratorRemoved(GameObject generator)
    {
        // Resta del contador si el generador se retira o desactiva
        Generator generatorComponent = generator.GetComponent<Generator>();
        if (generatorComponent != null && generatorComponent.IsRepaired())
        {
            repairedGeneratorCount--;
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

    private void PerformAction()
    {
        // Realiza la acción cuando se alcanza el tiempo límite.
        OnEnemyReduce?.Invoke();

    }

}

