using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TreeChanger : MonoBehaviour
{
    private GameObject tree;
    [SerializeField] private Mesh tree2;
    private Material treeMat;

    bool canChangeTree = false;
    float val = 0;
    float treeChangeTime;
    [SerializeField] private EnvironmentDamageCalculator edc;
    [SerializeField] private CloudChanger cloudChanger;

    bool treeSwitch = false;

    Material mat;

    void Start()
    {
        tree = this.gameObject;
        StartCoroutine("ChangeTree");
        treeMat = tree.transform.GetComponent<MeshRenderer>().material;
        mat = GetComponent<MeshRenderer>().material;
    }
    
    void Update()
    {
        if (cloudChanger.HasCloudChangedToDust())
        {
            if (treeMat.GetFloat("_Dissolve") < 1 && canChangeTree)
            {
                val = Mathf.Lerp(0,1, Time.deltaTime * 0.5f);
                treeMat.SetFloat("_Dissolve", treeMat.GetFloat("_Dissolve") + val);
            }
            
            else if (treeMat.GetFloat("_Dissolve") != 0)
            {
                canChangeTree = false;
                ChangeDryTree();
                val = Mathf.Lerp(0,1, Time.deltaTime * 0.5f);
                treeMat.SetFloat("_Dissolve", treeMat.GetFloat("_Dissolve") - val);
            } 

            ChangeTreeColor();
        }
    }

      IEnumerator ChangeTree()
    {
        yield return new WaitForSeconds(edc.DamageTime() - 2);
        canChangeTree = true;
    }


    private void ChangeDryTree()
    {
        if ( tree.transform.GetComponent<MeshFilter>().mesh != tree2)
        {
            tree.transform.GetComponent<MeshFilter>().mesh = tree2; 
        }
    }


    private void ChangeTreeColor()
    {
        if (treeChangeTime !=1 )
        {
            treeChangeTime += Time.deltaTime /(edc.DamageTime() / 2);
            Debug.Log("color change value : " + treeChangeTime);
            mat.SetColor("_BaseColor", Color.Lerp(Color.white, Color.red, treeChangeTime)); 
        }
        
    }

  
}
