using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public GameObject deathVFXPrefab;

    Rigidbody2D rb;
    public float kicbackForce= 30;

    int tranpsLayerIndex;

    public bool isDie;

    private SpriteRenderer sr;


    public bool invincible = false;
    public float invincibleTime = 1f;

    public float currentHealth;
    public float totalHealth = 100;

    public int flashFrequency =3;
    public int flashIndex;

    public Sprite getHurtSprite;

    //public bool IsPlayerDie { get { return currentHealth == 0 ? true : false; } }

    private void Start()
    {
        isDie = false;
        tranpsLayerIndex = LayerMask.NameToLayer("Traps");

        currentHealth = totalHealth;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F1))
        //    KickBack();
        //if (Input.GetKeyDown(KeyCode.F2))
        //    KickUp();
        if (isDie) {
            GetComponent<PlayerAnimation>().PlayerDie();
        }

    }

    private void FixedUpdate()
    {
        if (invincible) {
            //GetComponent<SpriteRenderer>().enabled = false;
            //InvincibleFlash();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == tranpsLayerIndex && !isDie && !invincible) {

            //Instantiate(deathVFXPrefab, this.transform.position, transform.rotation);

            //gameObject.SetActive(false);

            //AudioManager.PlayDeathAudio();
            //GameManager.PlayerDie();
            //isDie = true;

            Debug.Log(collision.tag);
            //currentHealth -= 10;
            currentHealth -= DamageOfTraps(collision.tag);


            if (currentHealth <= 0) {
                currentHealth = 0;
                isDie = true;
            }

            UIManager.UpdateHealthBar(currentHealth / totalHealth);


            KickBack(collision.ClosestPoint(transform.position));
            StartCoroutine("InvincibleStage");

        }
    }

    internal void ResetHealthState()
    {
        currentHealth = totalHealth;
        isDie = false;
        UIManager.UpdateHealthBar(currentHealth/totalHealth);
    }

    private float DamageOfTraps(string tag)
    {
        float damage;
        switch (tag)
        {
            case "Axe":
                damage = 25;
                break;
            case "Spike":
                damage = 10;
                break;
            case "FallingBlock":
            case "SuperSpike":
                damage = currentHealth;
                break;
            default:
                damage = 10;
                break;
        }
        return damage;
    }

    void KickBack(Vector2 closePoint) {
        int direction = 0;
        if (closePoint.x > transform.position.x) {
            if (transform.localScale.x > 0)
            {
                direction = -1;
            }
            else {
                direction = -1;
            }
        }
        else
        {
            if (transform.localScale.x > 0)
            {
                direction = 1;
            }
            else
            {
                direction = 1;
            }
        }

        //Debug.Log("add force, " + (direction * kicbackForce).ToString() + ":" + rb.velocity.y.ToString());
        //rb.velocity = new Vector2(-transform.localScale.x * kicbackForce, rb.velocity.y* kicbackForce);
        rb.velocity = new Vector2(direction * kicbackForce, rb.velocity.y);
        //rb.AddForce(new Vector2(-transform.localScale.x * kicbackForce, 1 * kicbackForce),ForceMode2D.Impulse);

    }

    void KickUp() {
        //Debug.Log("add force, " + rb.velocity.x.ToString() + ":" + kicbackForce.ToString());

        rb.velocity = new Vector2(rb.velocity.x, kicbackForce);
        //rb.AddForce(new Vector2(-transform.localScale.x * kicbackForce, 1 * kicbackForce),ForceMode2D.Impulse);

    }

    private IEnumerator InvincibleStage() {
        invincible = true;
        GetComponent<PlayerAnimation>().GetHurtTrigger();
        GetComponent<PlayerMovement>().xVelocity = 0;
        flashIndex = 0;
        //Debug.Log("in IEnumerator");
        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
        GetComponent<PlayerAnimation>().EndHurtAnim();
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void InvincibleFlash() {
        flashIndex++;
        if (flashIndex % flashFrequency == flashFrequency - 1)
        {
            GetComponent<SpriteRenderer>().enabled = false;

        }
        else if (flashIndex % flashFrequency == flashFrequency - 2)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}
