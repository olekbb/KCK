IEnumerator GoCenter() {
	
	Debug.Log ("GoCenterBegin");
	for (int i = 0; i < 25; i++) {
		Debug.Log ("GoCenterIteration");
		bool shouldWait = true;
		if ( rigidbody2D.transform.position.x <= -0.5) {
			Debug.Log ("idz w prawo");
			ShowPosition();
			GoRight();
		} else 
		if ( rigidbody2D.transform.position.x > 0.5) {
			GoLeft ();
			Debug.Log ("idz w lewo");
			ShowPosition();
		} else
		if ( rigidbody2D.transform.position.y < -0.5) {
			GoUp();
			Debug.Log ("idz w gore");
			ShowPosition();
		} else
		if ( rigidbody2D.transform.position.y > 0.5) {
			GoDown ();
			Debug.Log ("idz w dol");
			ShowPosition();
		} else shouldWait = false;
		if (shouldWait) {
			yield return new WaitForSeconds(0.2f);
			Debug.Log ("czekam");
		}
	}
	Debug.Log ("GoCenterEND");
	
}