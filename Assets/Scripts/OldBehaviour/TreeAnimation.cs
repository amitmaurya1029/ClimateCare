using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimation : MonoBehaviour
{
    [SerializeField] private CloudChanger cloudChanger;
    float timer = 0;
    bool isTreeStartShaking = false;
    public event Action<bool> OnTreeMovement;

    void Start()
    {
        cloudChanger.OnCloudChange += PlayTreeShakeAnim;
        //PlayTreeShakeAnim();
    }

    void Update()
    {
        if (isTreeStartShaking)
        {
            if (timer <= 3.5f)
                timer += Time.deltaTime;

            else
            {
                Debug.Log("timer value : " + timer);
                StopTreeShakeAnim();
            }
       
        }
       
    }

    private void PlayTreeShakeAnim()
    {
        TreeShakeAnim(true);
        isTreeStartShaking = true;
        OnTreeMovement?.Invoke(true);
    }

    private void StopTreeShakeAnim()
    {
        TreeShakeAnim(false);
        isTreeStartShaking = false;
        OnTreeMovement?.Invoke(false);
    }


    // only run for 3 sec.
    private void TreeShakeAnim(bool isShake)
    {
        foreach (Transform tree in this.transform)
        {
            if (!tree.TryGetComponent<Animator>(out Animator anim))
            {
                Debug.Log("tree does not contain an animation component : ");
            }
            else
            {
                tree.GetComponent<Animator>().SetBool("IsTreeShaking", isShake);
                Debug.Log("here is the tree name : " + tree.name);
            }
           
        }
    } 
   
}
