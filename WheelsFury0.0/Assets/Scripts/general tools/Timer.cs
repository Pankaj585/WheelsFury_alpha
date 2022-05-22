using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float timerDuration = 3.0f;
    public float timeLeft = 3.0f;
    public bool isTimerRunning = false;
    public Timer(float seconds)
    {
        timerDuration = seconds;
    }

    public void StartTimer()
    {
        timeLeft = timerDuration;
        isTimerRunning = true;
    }
    public bool Tick(float deltaTime)
    {
        if (!isTimerRunning)
            return false;

        timeLeft -= deltaTime;
        if (timeLeft <= 0)
            return true;

        return false;
    }

    public void ResetTimer()
    {
        isTimerRunning = false;
        timeLeft = timerDuration;
    }

    public float GetProgressValue()
    {
        return (timerDuration - timeLeft) / timerDuration;
    }
}
