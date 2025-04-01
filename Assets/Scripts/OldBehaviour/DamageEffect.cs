using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DamageEffect : MonoBehaviour
{
    private Volume postVolume;
    private float effectTime;
    float t;
    private Vignette vignette;
    [SerializeField] private CloudChanger cloudChanger; 


    void Start()
    {
        postVolume = GetComponent<Volume>();
        cloudChanger.OnCloudChange += CallingApplyDamageEffect; 
        
    }

    private void CallingApplyDamageEffect()
    {
        StartCoroutine(ApplyDamageEffect());
    }

     IEnumerator ApplyDamageEffect()
    {
        float startTime = Time.time;
        postVolume.profile.TryGet(out vignette);

        while (Time.time < startTime + 4)
        {
            effectTime += Time.deltaTime / 1f;
            // t = Mathf.Lerp(0, 0.35f, effectTime);

            t =  Mathf.PingPong(Time.time, 0.35f);
            vignette.intensity.value = t;

            Debug.Log("damageEffect vALUE : 1" + t);
            yield return null;
        }
         vignette.intensity.value = 0;
    }
    
}
