using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TEST : MonoBehaviour
{
    
    EnvironmentDamageCalculator edc;

    void Start()
    {
        GameObject obj = new GameObject();
        edc = obj.AddComponent<EnvironmentDamageCalculator>(); // this is the way of creating an instance of a monobehaviour class in unity.  
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public class player : MonoBehaviour
    {
        public int data;
    }
}


