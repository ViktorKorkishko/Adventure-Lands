using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        
    }

    public void Smash()
    {
        animator.SetBool("smash", true);
    }
}
