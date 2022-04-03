using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerMovement movement;
    PlayerHealth health;
    Rigidbody2D rb;

    int groundID;
    int hangingID;
    int crouchID;
    int speedID;
    int fallID;
    int slideWallID;
    int getHurtID;
    int getHurtTriggerID;
    int getDieID;

    private void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        health = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        groundID = Animator.StringToHash("isOnGround");
        hangingID = Animator.StringToHash("isHanging");
        crouchID = Animator.StringToHash("isCrouching");
        speedID = Animator.StringToHash("speed");
        fallID = Animator.StringToHash("verticalVelocity");
        slideWallID = Animator.StringToHash("SlideWall");
        getHurtID = Animator.StringToHash("GetHurt");
        getHurtTriggerID = Animator.StringToHash("GetHurtTrigger");
        getDieID = Animator.StringToHash("Die");
    }

    private void Update()
    {
        if (health.isDie) {
            movement.isWallSlide = false;
            movement.isHanging = false;
            movement.isCrouch = false;
        }

        anim.SetFloat(speedID, Mathf.Abs( movement.xVelocity));
        //anim.SetBool("isOnGround", movement.isOnGround);
        anim.SetBool(groundID,movement.isOnGround);
        anim.SetBool(hangingID, movement.isHanging);
        anim.SetBool(crouchID, movement.isCrouch);
        anim.SetFloat(fallID, rb.velocity.y);
        anim.SetBool(slideWallID, movement.isWallSlide);
        //anim.SetBool(slideWallID, movement.isWallSlide);


    }

    public void StepAudio() {
        AudioManager.PlayFootstepAudio();
    }

    public void DieAudio()
    {
        AudioManager.PlayDeathAudio();
        GameManager.PlayerDie();

        //StartCoroutine("LoadCheckPoint");

    }

    IEnumerator LoadCheckPoint() {
        GameManager.FadeOut();
        while (!GameManager.FadeOutEnd()) {
            yield return null;
        }
        transform.position = GameManager.GetCheckPointPosition();
        yield return new WaitForSeconds(1f);
        health.ResetHealthState();
        movement.RestMovement();

        anim.Rebind();
        anim.Update(0f);
        yield return new WaitForSeconds(1f);
        GameManager.FadeIn();
    }

    public void CrouchStepAudio() {
        AudioManager.PlayCrouchAudio();
    }
    public void PlayerDie() {
        anim.SetTrigger(getDieID);
        //GetComponent<PlayerMovement>().xVelocity = 0;
        rb.velocity = new Vector2(0f , rb.velocity.y);
        //rb.bodyType = RigidbodyType2D.Static;
        //AudioManager.PlayDeathAudio();
    }

    public void GetHurtTrigger() {
        anim.SetBool(getHurtID, true);
        anim.SetTrigger(getHurtTriggerID);
    }

    public void EndHurtAnim() {
        anim.SetBool(getHurtID, false);
    }

}
