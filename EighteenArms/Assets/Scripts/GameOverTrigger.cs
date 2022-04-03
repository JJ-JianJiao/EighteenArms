using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    int playerLayer;

    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(playerLayer)) {
            GameManager.instance.isFreezon = true;
            UIManager.UpdateGameOverUI();
        }
    }
}
