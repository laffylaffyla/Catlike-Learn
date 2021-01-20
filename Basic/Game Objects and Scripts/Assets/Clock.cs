using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform hoursTransform;
    public Transform minutesTransform;
    public Transform secondsTransform;

    public bool continuous;

    const float DegreesPerHour = 30f;
    const float DegreesPerMinute = 6f;
    const float DegreesPerSecond = 6f;

    void Update()
    {
        if (continuous)
            UpdateContinuous();
        else
            UpdateDiscrete();
    }
    
    void UpdateContinuous ()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hoursTransform.localRotation =
            Quaternion.Euler(0f, (float)time.TotalHours * DegreesPerHour, 0f);
        minutesTransform.localRotation =
             Quaternion.Euler(0f, (float)time.TotalMinutes * DegreesPerMinute, 0f);
        secondsTransform.localRotation =
            Quaternion.Euler(0f, (float)time.TotalSeconds * DegreesPerSecond, 0f);
    }

    void UpdateDiscrete()
    {
        DateTime time = DateTime.Now;
        hoursTransform.localRotation =
            Quaternion.Euler(0f, time.Hour * DegreesPerHour, 0f);
        minutesTransform.localRotation =
             Quaternion.Euler(0f, time.Minute * DegreesPerMinute, 0f);
        secondsTransform.localRotation =
            Quaternion.Euler(0f, time.Second * DegreesPerSecond, 0f);
    }
}
