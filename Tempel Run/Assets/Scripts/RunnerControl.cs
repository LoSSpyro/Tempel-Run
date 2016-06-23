using UnityEngine;
using System.Collections;


public class RunnerControl : MonoBehaviour {

	public bool running = false;
	public Vector3 direction = new Vector3(0, 0, 1);
	float speed = 5;
	float maxSpeed = 5;

	float maxJumpingHeight = 8;

	public float maxJumpTime = 20;
	float airTime;

	float rotation;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		if (running) {

			transform.rotation = Quaternion.AngleAxis(rotation, Vector3.up);

			checkInput();

			processOrientation();

			freezePosition();

			if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed) {
				GetComponent<Rigidbody>().AddForce(direction * speed);
			}

			if (transform.position.y == maxJumpingHeight && airTime > 0) {
				airTime -= Time.deltaTime;
			}

			if (transform.position.y == maxJumpingHeight && airTime < 0) {
				transform.position = new Vector3(transform.position.x, 1, transform.position.z);
			}


			checkDeath();
		}
	}

	/**
	 * verarbeitet die Touch-Eingaben
	 */
	void processTouches(){

	}

	/**
	 * checks the orientation of the smartphone and moves the runner according to the actual orientation 
	 */
	void processOrientation() {

	}

	//sagt, ob ein entsprechender swipe vom Spieler ausgeführt wurde.
	bool swipeLeft(){
		return false;
	}

	bool swipeRight(){
		return false;
	}

	bool swipeUp(){
		return false;
	}

	//Input interpretieren
	void checkInput(){

		processTouches();

		if (swipeLeft()) {
			direction = Quaternion.AngleAxis(-90, Vector3.up) * direction;
			rotation -= 90;
			float upwardsMovement = GetComponent<Rigidbody>().velocity.y;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().velocity += new Vector3(0, upwardsMovement, 0);

		}

		if (swipeRight()){
			direction = Quaternion.AngleAxis(+90, Vector3.up) * direction;
			rotation += 90;
			float upwardsMovement = GetComponent<Rigidbody>().velocity.y;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().velocity += new Vector3(0, upwardsMovement, 0);
		}

		if (swipeUp()) {

			if (transform.position.y < 2) {
				transform.position = new Vector3(transform.position.x, maxJumpingHeight, transform.position.z);
			}
		}

	}
	

	void die(){
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		direction = new Vector3(0, 0, 1);
		rotation = 0;
		transform.position = new Vector3(0, 1, 0);
		running = false;
	}


	void checkDeath() {
		if (transform.position.y < 0.0f) {
			die ();
		}
	}

	/**
	 * make sure the player runs only in one direction.
	 */
	void freezePosition(){
		if (direction == new Vector3(0, 0, 1) || direction == new Vector3(0, 0, -1)) {
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
		} else {
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
		}
		GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezeRotation;
	}
}
