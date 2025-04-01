using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CloudChanger : MonoBehaviour
{
   [SerializeField] private ParticleSystem cloud;
   [SerializeField] private EnvironmentDamageCalculator edc;
   bool isCloudChanged = false;

   public event Action OnCloudChange; 

    void Start()
    {
        StartCoroutine("ChangeCloud");
    }

    IEnumerator ChangeCloud()
    {
       if (edc.GetTimeUsageData() >= 2)
       {
            yield return new WaitForSeconds(7);
            isCloudChanged = true;
            ChangeCloudToDust();
            OnCloudChange?.Invoke();
       }

       else { Debug.Log("there is no cloud here : ");}
    }

    private void ChangeCloudToDust()
    {
        var clorLifetime = cloud.colorOverLifetime; 
        clorLifetime.enabled = true;
    }

    public bool HasCloudChangedToDust()
    {
        return isCloudChanged;
    } 
}
