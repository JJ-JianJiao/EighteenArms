using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoDoor : MonoBehaviour
{
    public enum DoorType { Auto,collect }

    Animator anim;
    int openDoorID;
    int closeDoorID;

    public DoorType doorType = DoorType.Auto;

    public bool isOpen;

    public int needOrlNum;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        openDoorID = Animator.StringToHash("Open");
        closeDoorID = Animator.StringToHash("Close");
        isOpen = false;
        needOrlNum = 0;
    }

    public void Open()
    {
        AudioManager.PlayDoorOpenAudio();
        anim.SetTrigger(openDoorID);
    }

    public void Close()
    {
        AudioManager.PlayDoorOpenAudio();
        anim.SetTrigger(closeDoorID);
    }

    public void RegisterOrb() {
        needOrlNum++;
    }

    public void CollectOrb() {
        needOrlNum--;
        if (needOrlNum == 0) {
            Open();
        }
    }
}
