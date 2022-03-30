using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    int openDoorID;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        openDoorID = Animator.StringToHash("Open");

    }

    private void Start()
    {
        GameManager.RegisterDoor(this);
    }
    public void Open() {
        AudioManager.PlayDoorOpenAudio();
        anim.SetTrigger(openDoorID);
    }
}
