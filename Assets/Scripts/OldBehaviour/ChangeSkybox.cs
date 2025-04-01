using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
     float val = 0;
     bool canChangeSky = false;
    void Start()
    {
       StartCoroutine("ChangeSky");
        RenderSettings.skybox.SetFloat("_BlendFactor",0);
    }


    void Update()
    {
        if (RenderSettings.skybox.GetFloat("_BlendFactor") < 1    &&  canChangeSky)
        {
            val = Mathf.Lerp(0,1, Time.deltaTime * .2f);
            RenderSettings.skybox.SetFloat("_BlendFactor",RenderSettings.skybox.GetFloat("_BlendFactor") + val);
            Debug.Log("is still skybox change here : yes");
        }
         
    }


    IEnumerator ChangeSky()
    {
       
        yield return new WaitForSeconds(5);
        canChangeSky = true;

        
    }
   

     void OnDisable()
    {
         RenderSettings.skybox.SetFloat("_BlendFactor",0);          
    }
}
