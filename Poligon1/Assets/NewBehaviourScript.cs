using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	
	public float maxSpeed = 10f;
	Animator anim;
	
	void Start () {
		anim = GetComponent<Animator>();
	}

	void FixedUpdate () {
		float move = Input.GetAxis ("Horizontal");
		float move1 = Input.GetAxis ("Vertical");
		rigidbody2D.velocity = new Vector2 (move1 * maxSpeed, rigidbody2D.velocity.y);
		
		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.x);
		
		anim.SetFloat("SpeedH", move);
		anim.SetFloat("SpeedV", move1);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "PickUp" && anim.GetBool ("Full") == false) {
			other.gameObject.SetActive (false);
			anim.SetBool("Full", true);
		}
		if (other.gameObject.tag == "DropSpot" && anim.GetBool ("Full") == true) {
			anim.SetBool ("Full", false);	
		}
	}

	void Update () {
	
	}
}
