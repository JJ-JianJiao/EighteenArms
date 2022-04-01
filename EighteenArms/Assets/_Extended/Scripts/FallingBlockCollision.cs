using UnityEngine;

public class FallingBlockCollision : MonoBehaviour
{
	Rigidbody2D rigidBody;
	BoxCollider2D box;
	AudioSource audioSource;
	LayerMask groundMask;
	int groundLayer;


	int trapsLayer;

	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		box = GetComponent<BoxCollider2D>();
		audioSource = GetComponent<AudioSource>();

		groundMask = LayerMask.GetMask("Ground");
		groundLayer = LayerMask.NameToLayer("Ground");

		trapsLayer = LayerMask.NameToLayer("Traps");

		box.isTrigger = false;
	}

	public void Fall()
	{
		gameObject.layer = trapsLayer;
		box.isTrigger = true;

		rigidBody.bodyType = RigidbodyType2D.Dynamic;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != groundLayer)
			return;

		Vector3 pos = rigidBody.position;
		RaycastHit2D hit;

		hit = Physics2D.Raycast(pos, Vector2.down, 1f, groundMask);
		pos.y = hit.point.y + .5f;
		transform.position = pos;

		box.usedByComposite = true;
		Destroy(rigidBody);

		audioSource.Play();
	}
}
