using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentChanger : MonoBehaviour
{
    [SerializeField] private Material greenEnv_mat;
    [SerializeField] private Material droughtEnv_mat;
    [SerializeField] private GameObject plane;

    [SerializeField] private EnvironmentDamageCalculator edc;

    bool canChangePalne;
    bool canChangeDroughtPlane;

    float val = 0;
    float droughtVal = 0;

    void Start()
    {
         StartCoroutine("ChangeTree");
    }

    void Update()
    {
        if ( greenEnv_mat.GetFloat("_DissolveAmount") < 1    &&  canChangePalne )
        {
           val = Mathf.Lerp(0,1, Time.deltaTime * .4f);
           greenEnv_mat.SetFloat("_DissolveAmount", greenEnv_mat.GetFloat("_DissolveAmount") + val);
        }

        else if (greenEnv_mat.GetFloat("_DissolveAmount") >= 1)
        {                                                                       // green environment must removed once the drought env appear.
            plane.SetActive(false);
        }

        if (droughtEnv_mat.GetFloat("_DissolveAmount") != 0 &&  canChangeDroughtPlane)
        {
            droughtVal = Mathf.Lerp(0,1, Time.deltaTime * .5f);
            Debug.Log("here is the drought plan value here : " +droughtVal);
            droughtEnv_mat.SetFloat("_DissolveAmount", droughtEnv_mat.GetFloat("_DissolveAmount") - droughtVal);
        }

    }

    IEnumerator ChangeTree()
    {
        yield return new WaitForSeconds(edc.DamageTime());
        canChangePalne = true;
        // yield return new WaitForSeconds(.05f);
         canChangeDroughtPlane = true;
    }


    void OnDisable()
    {
        greenEnv_mat.SetFloat("_DissolveAmount",0);
        droughtEnv_mat.SetFloat("_DissolveAmount",1);
    }
}
