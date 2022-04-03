using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orib : MonoBehaviour
{
    public GameObject explosionVFXPrefab;

    int playerLayerIndex;

    public AutoDoor autoDoor;

    bool touched;

    private void Start()
    {
        playerLayerIndex = LayerMask.NameToLayer("Player");
        GameManager.RegisterOrb(this);
        if (autoDoor != null) {
            autoDoor.RegisterOrb();
        }
        touched = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayerIndex && !touched)
        {
            touched = true;
            Instantiate(explosionVFXPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);

            AudioManager.PlayOrbAudio();

            //GameManager.PlayerGetOrb(this,collision.transform.position);
            if (autoDoor != null)
            {
                autoDoor.CollectOrb();
            }
        }
    }
}
