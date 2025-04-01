using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnvironmentDamageCalculator : MonoBehaviour
{
    private float timeUsage;

    void Start()
    {
        Debug.Log("getfinalappusageval : " + UsageStatsExample.timeUsage);
        Debug.Log("getfinalappusageval : get hour value : " + CovertAppUsageTimeToHour());
        timeUsage = CovertAppUsageTimeToHour();
    }


    // convert time to hours
    // if the time usge is greater than 2 than the nature will start taking damange wrt the total time.
   
    // 2-3 = 45s
    // 3-4 = 40s
    // 4-5 = 35
    // 5-6 = 30
    // 6-7 = 35
    // 7-8 = 30
    // 8-9 = 25
    // 9-10 = 15
    

    public int DamageTime( )
    {
        float t = CovertAppUsageTimeToHour();
        

        if (t >= 2 && t <= 3)
        {
            Debug.Log(" here is the vale : " + t);
            return 45;
        }
         if (t >= 3 && t <= 4)
        {
            Debug.Log(" here is the vale : " + t);
            return 40;
        }
         if (t >= 4 && t <= 5)
        {
            Debug.Log(" here is the vale : " + t);
            return 35;
        }
        if (t >= 5 && t <= 6)
        {
            Debug.Log(" here is the vale : " + t);
            return 30;
        }
        
        if (t >= 6 && t <= 7)
        {
            Debug.Log(" here is the vale : " + t);
            return 25;
        }
        if (t >= 7 && t <= 8)
        {
            Debug.Log(" here is the vale : " + t);
            return 20;
        }
        if (t >= 8 && t <= 9)
        {
            Debug.Log(" here is the vale : " + t);
            return 15;
        }
        if (t >= 9 && t <= 10)
        {
            Debug.Log(" here is the vale : " + t);
            return 10;
        }
        
        else {return 0;}
     
    }
         

    private float CovertAppUsageTimeToHour()
    {
        TimeSpan timeSpan =  TimeSpan.FromMilliseconds( UsageStatsExample.timeUsage);
        return MathF.Round(((float)timeSpan.TotalHours) * 10) / 10;
    }

    public float GetTimeUsageData()
    {
        return timeUsage;
    }

   
}
