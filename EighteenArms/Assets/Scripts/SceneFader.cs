using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    Animator anim;
    int faderID;
    int faderInID;
    int faderOutID;

    private void Start()
    {
        anim = GetComponent<Animator>();
        faderID = Animator.StringToHash("Fade");
        faderInID = Animator.StringToHash("FadeIn");
        faderOutID = Animator.StringToHash("FadeOut");

        GameManager.RegisterSceneFader(this);
    }

    public void FadeOut() {
        //anim.SetTrigger(faderID);
        anim.SetBool(faderOutID, true);
        anim.SetBool(faderInID, false);
    }

    public void FadeIn()
    {
        //anim.SetTrigger(faderID);
        anim.SetBool(faderInID, true);
        anim.SetBool(faderOutID, false);

    }

    internal bool FadeOutEnd()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Fade Out Scene") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            return true;
        }
        else
            return false;
    }

    internal bool FadeInEnd()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Fade In Scene") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            return true;
        }
        else
            return false;
    }

    public void ResetFader() {
        anim.SetBool(faderOutID, false);
        anim.SetBool(faderInID, false);
    }
}
