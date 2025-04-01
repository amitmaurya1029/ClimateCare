using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AirQualityIndex : MonoBehaviour
{
    [SerializeField] private Image tree;
    [SerializeField] private TextMeshProUGUI aqiText;
    [SerializeField] private EnvironmentDamageCalculator edc;
    [SerializeField] private CloudChanger cloudChanger;
    private Color32  currentcolor = new Color32(255,255,255,255);
    private float changeHudTime;
   

   private float timeUsage;

    void Start()
    {
        timeUsage = edc.GetTimeUsageData();
        cloudChanger.OnCloudChange += UpdateHudValues;
        Debug.Log("aqi vahe : appusage val : " + timeUsage);
        StartCoroutine (ColorChangeHud( new Color32(77,255,45,255)));
        
    }

    // set the color of the teer based on the appusage time;
    //display the aqi number on screen

    private void UpdateHudValues()
    {

        if (timeUsage > 0 && timeUsage <= 2)
        {
          // green color  20 aqi
            Color32 color = new Color32(77,255,45,255);
            StartCoroutine (ColorChangeHud(color));
            aqiText.text = "20";

        }
        
         if (timeUsage > 2 && timeUsage <= 3)
        {
           // yellow color  55 aqi
            Color32 color = new Color32(241,255,4,255);
            StartCoroutine (ColorChangeHud(color));
            aqiText.text = "55";
        }

        if (timeUsage > 3 && timeUsage <= 4)
        {
          
           // yellow color  90 aqi
            Color32 color = new Color32(241,255,4,255);
            StartCoroutine (ColorChangeHud(color));
            aqiText.text = "90";
        }
         
        if (timeUsage > 4 && timeUsage < 5)
        {
           // orange color  150 aqi
            Color32 color = new Color32(255,112,4,255);
            StartCoroutine (ColorChangeHud(color));
            aqiText.text = "150";
        }
        if (timeUsage > 5)
        { 
           // red color  300 aqi
            Color32 color = new Color32(255,42,4,255);
            StartCoroutine(ColorChangeHud(color));
            aqiText.text = "300";
        }
    }
    
    IEnumerator ColorChangeHud(Color32 newColor)
    {
        float startTime = Time.time;
        while (Time.time < startTime + 3)
        {
            changeHudTime += Time.deltaTime / 2;
            tree.color = Color.Lerp(currentcolor, newColor, changeHudTime);
            Debug.Log("colorChnage vALUE : 1" + changeHudTime);
            yield return null;
        }
        currentcolor = newColor;
        changeHudTime = 0;
    }
    
}
