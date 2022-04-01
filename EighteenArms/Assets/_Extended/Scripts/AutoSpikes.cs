using UnityEngine;

public class AutoSpikes : MonoBehaviour
{
	public float activeDuration = 2f;

	Animator anim;
	AudioSource audioSource;
	int activeParamID = Animator.StringToHash("Active");
	float deactivationTime;
	bool playerInRange;
	bool trapActive;
	int playerLayer;
	public float activeTime = 0;

	void Start ()
	{
		playerLayer = LayerMask.NameToLayer("Player");

		anim = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();

	}
	
	void Update ()
	{
		if (trapActive && !playerInRange && Time.time >= deactivationTime)
		{
			trapActive = false;
			anim.SetBool(activeParamID, false);
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == playerLayer)
		{
			playerInRange = true;
			trapActive = true;

			Invoke("ActiveSpike", activeTime);

		}
	}

	void ActiveSpike() {
		anim.SetBool(activeParamID, true);
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == playerLayer)
		{
			playerInRange = false;
			deactivationTime = Time.time + activeDuration;
		}
	}

	public void AudioPlay() {
		audioSource.Play();
	}
}
