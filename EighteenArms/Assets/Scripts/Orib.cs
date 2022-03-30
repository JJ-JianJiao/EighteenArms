using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orib : MonoBehaviour
{
    public GameObject explosionVFXPrefab;

    int playerLayerIndex;

    private void Start()
    {
        playerLayerIndex = LayerMask.NameToLayer("Player");
        GameManager.RegisterOrb(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayerIndex)
        {
            Instantiate(explosionVFXPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);

            AudioManager.PlayOrbAudio();

            GameManager.PlayerGetOrb(this);
        }
    }
}
