using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public GameObject deathVFXPrefab;

    int tranpsLayerIndex;

    bool isDie;

    private void Start()
    {
        isDie = false;
        tranpsLayerIndex = LayerMask.NameToLayer("Traps");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == tranpsLayerIndex && !isDie) {

            Instantiate(deathVFXPrefab, this.transform.position, transform.rotation);

            gameObject.SetActive(false);

            AudioManager.PlayDeathAudio();
            GameManager.PlayerDie();
            isDie = true;
        }
    }
}
