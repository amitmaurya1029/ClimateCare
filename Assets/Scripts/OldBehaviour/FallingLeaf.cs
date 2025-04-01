using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class FallingLeaf : MonoBehaviour
{
    private ParticleSystem leaf;

    void Awake()
    {
        leaf = GetComponent<ParticleSystem>();
        leaf.Stop();
    }

    void Start()
    {
        Debug.Log("get the root object name : " + transform.root.transform.Find("Trees"));
        transform.root.transform.Find("Trees").GetComponent<TreeAnimation>().OnTreeMovement += SetLeaftoFall;

    }

    private void SetLeaftoFall(bool e)
    {
        if (e)
        {
            leaf.Play();
        }
        else {leaf.Stop();}
        

    }
   
}
