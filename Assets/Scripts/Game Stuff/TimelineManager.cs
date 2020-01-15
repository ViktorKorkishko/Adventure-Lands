﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public bool fix = false;
    public Animator playerAnimator;
    public RuntimeAnimatorController playerAnim;
    public PlayableDirector director;

    void OnEnable()
    {
        playerAnim = playerAnimator.runtimeAnimatorController;
        playerAnimator.runtimeAnimatorController = null;

    }

    void Update()
    {
        if (director.state != PlayState.Playing && !fix)
        {
            fix = true;
            playerAnimator.runtimeAnimatorController = playerAnim;
        }
    }
}
