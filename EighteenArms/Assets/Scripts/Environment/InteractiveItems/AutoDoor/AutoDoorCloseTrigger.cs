using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoorCloseTrigger : MonoBehaviour
{
    int playerLayer;
    AutoDoor door;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        door = GetComponentInParent<AutoDoor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(playerLayer) && door.isOpen)
        {
            door.Close();
            door.isOpen = false;
        }
    }
}
