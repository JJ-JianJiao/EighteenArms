using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int CheckPointID;

    int playerLayerIndex;
    Transform checkPointOrb;

    private void Start()
    {
        playerLayerIndex = LayerMask.NameToLayer("Player");
        checkPointOrb = transform.GetChild(0);
        GameManager.RegisterCheckPoint(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayerIndex && !checkPointOrb.gameObject.activeSelf) {

            if (GameManager.SetCheckPointPosition(transform.position, 1, CheckPointID)) {
                checkPointOrb.gameObject.SetActive(true);
            }

        }
    }

    public void CloseLight() {
        checkPointOrb.gameObject.SetActive(false);
    }
}
