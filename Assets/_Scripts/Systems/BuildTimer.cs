using UnityEngine;
using System;

public class BuildTimer : MonoBehaviour
{
    public float buildTime = 30f;
    private float currentTime;
    public bool canBuild = true;

    public event Action OnBuildTimeEnded;

    void Start()
    {
        currentTime = buildTime;
    }

    void Update()
    {
        if (!canBuild) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            canBuild = false;
            currentTime = 0;
            Debug.Log("Tiempo terminado");
            OnBuildTimeEnded?.Invoke();
        }
    }

    public float GetTimeRemaining()
    {
        return currentTime;
    }
}