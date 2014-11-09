using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	public float maxSpeed = 10f;
	//bool facingRight = false;
	//bool facingUp = false;
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
		/*
		if (move1 > 0 && !facingRight)
			Flip();
		else if (move1 < 0 && facingRight) 
			Flip();
		
		if (move > 0 && !facingUp)
			FlipV();
		else if (move < 0 && facingUp) 
			FlipV();*/
	}
	/*
	void Flip() {
		facingRight = !facingRight;
		
	}
	void FlipV() {
		facingUp = !facingUp;
		Vector3 theScale = transform.localScale;
		theScale.x*=-1;
		transform.localScale = theScale;
	}*/
	// Update is called once per frames
	void Update () {
	
	}
}
