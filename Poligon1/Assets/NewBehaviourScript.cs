using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	
	//public float maxSpeed = 10f;
	Animator anim;


	Vector3 center = new Vector3(0, 0);					//pozycja środka mapy
	Vector3 regalLeft = new Vector3(-4.5f, 1.5f);		//pozycje regałów i platform
	Vector3 regalCenter = new Vector3(0f, 1.5f);
	Vector3 regalRight = new Vector3(4.5f, 1.5f);

	Vector3 platformLeft = new Vector3(-4.5f, -1f);
	Vector3 platformCenter = new Vector3(0f, -1f);
	Vector3 platformRight = new Vector3(4.5f, -1f);
	void Start () {
		anim = GetComponent<Animator>();				//potrzebne, żeby przekazywać do unity kierunek obrócenia wózka
		//Debug.developerConsoleVisible = true;

	}

	void FixedUpdate () {								//płynne sterowanie (stare)
		//float move = Input.GetAxis ("Horizontal");
		//float move1 = Input.GetAxis ("Vertical");

		//rigidbody2D.velocity = new Vector2 (move1 * maxSpeed, rigidbody2D.velocity.y);
		//rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.x);	
		//rigidbody2D.velocity = transform.right * move2;

		//anim.SetFloat("SpeedH", move);
		//anim.SetFloat("SpeedV", move1);
	}

	void OnTriggerEnter2D (Collider2D other) {			//podnoszenie i porzucanie skrzyn
		if (other.gameObject.tag == "PickUp" && anim.GetBool ("Full") == false) {
			other.gameObject.SetActive (false);
			anim.SetBool("Full", true);
		}
		if (other.gameObject.tag == "DropSpot" && anim.GetBool ("Full") == true) {
			anim.SetBool ("Full", false);	
		}
	}
	void GoLeft () {												//funkcje ruchu skokowego
		rigidbody2D.transform.position += new Vector3 (-0.5f, 0);
		anim.SetFloat ("SpeedV", 0);
		anim.SetFloat ("SpeedH", -1);
	}
	void GoRight () {
		rigidbody2D.transform.position += new Vector3 (0.5f, 0);
		anim.SetFloat ("SpeedV", 0);
		anim.SetFloat ("SpeedH", 1);
	}
	void GoDown () {
		rigidbody2D.transform.position += new Vector3(0, -0.5f);	
		anim.SetFloat ("SpeedV", -1);
		anim.SetFloat ("SpeedH", 0);
	}
	void GoUp () {
		rigidbody2D.transform.position += new Vector3(0, 0.5f);
		anim.SetFloat ("SpeedV", 1);
		anim.SetFloat ("SpeedH", 0);
	}

	void RoundPosition () {					//zaokraglanie pozycji (nieuzywane)
		Debug.Log ("position before:");
		ShowPosition ();

		Vector3 Difference = new Vector3((Mathf.Round (rigidbody2D.transform.position.x) - rigidbody2D.transform.position.x), (Mathf.Round (rigidbody2D.transform.position.y) - rigidbody2D.transform.position.y));

		

		rigidbody2D.transform.position += Difference;
		Debug.Log ("position rounded:");
		ShowPosition ();
	}

	void ShowPosition(){			//pokazywanie pozycji gracza

		Vector3 player = rigidbody2D.transform.position;
		/*while (player.x < -0.5f) {
			GoRight ();
		}
		*/
		Debug.Log ("begin");
		Debug.Log (player.x);
		Debug.Log (player.y);
		Debug.Log ("end");
	}

	IEnumerator GoTo(Vector3 target) {					//znajdz droge i idź
		
		Debug.Log ("GoCenterBegin");
		float tolerance = 0.01f;						//jak blisko celu ma być gracz aby przerwać pętlę 
		int iterationLimit = 20;						//zabezpieczenie przed infinite loop
		//float tolerance2;
		//for (int i = 0; i < 25; i++)
		while (iterationLimit-->0 && (Vector3.Distance (rigidbody2D.transform.position , target)> tolerance)) {
			Debug.Log ("GoCenterIteration");
			bool shouldWait = true;
			if ( rigidbody2D.transform.position.x - target.x <= -0.5) {
				Debug.Log ("idz w prawo");
				ShowPosition();
				GoRight();
			} else 
				if ( rigidbody2D.transform.position.x - target.x >= 0.5) {
					GoLeft ();
					Debug.Log ("idz w lewo");
					ShowPosition();
				} else
					if ( rigidbody2D.transform.position.y - target.y <= -0.5) {
						GoUp();
						Debug.Log ("idz w gore");
						ShowPosition();
					} else
						if ( rigidbody2D.transform.position.y - target.y >= 0.5) {
							GoDown ();
							Debug.Log ("idz w dol");
							ShowPosition();
						} else shouldWait = false;
			if (shouldWait) {							//jeśli wykonałeś ruch w tej iteracji, poczekaj
				yield return new WaitForSeconds(0.2f);
				Debug.Log ("czekam");
			}
		}
		float distance = Vector3.Distance (rigidbody2D.transform.position, target);	//pokaz ostateczny dystans do celu
		Debug.Log (distance);

	}
	
	
	void Update () {						//sterowanie
		if (Input.GetKeyDown("left")) {
			GoLeft ();
			Debug.Log ("GetKeyDown left");
		}
		if (Input.GetKeyDown("right")) {
			GoRight ();
			Debug.Log ("GetKeyDown right");
		}
		if (Input.GetKeyDown("up")) {
			GoUp ();
			Debug.Log ("GetKeyDown up");
		}
		if (Input.GetKeyDown("down")) {
			GoDown ();
			Debug.Log ("GetKeyDown down");
		}
		if (Input.GetKeyDown ("p")) {
			//ShowPosition();
			Debug.Log ("GetKeyDown p");
			StartCoroutine(GoTo (center));
			//RoundPosition();
		}
		if (Input.GetKeyDown ("q")) {
			Debug.Log ("q");
			StartCoroutine(GoTo (regalLeft));
		}
		if (Input.GetKeyDown ("w")) {
			Debug.Log ("w");
			StartCoroutine(GoTo (regalCenter));
		}
		if (Input.GetKeyDown ("e")) {
			Debug.Log ("e");
			StartCoroutine(GoTo (regalRight));
		}
		if (Input.GetKeyDown ("a")) {
			Debug.Log ("a");
			StartCoroutine(GoTo (platformLeft));
		}
		if (Input.GetKeyDown ("s")) {
			Debug.Log ("s");
			StartCoroutine(GoTo (platformCenter));
		}
		if (Input.GetKeyDown ("d")) {
			Debug.Log ("d");
			StartCoroutine(GoTo (platformRight));
		}
		



	}
}
