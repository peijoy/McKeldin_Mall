using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	Vector3 start, originalScale, before, after;
	float speed, rSpeed, degree;
	private bool enable;
	GameObject obj;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (true);
		start = new Vector3 (170f, 1.8f, 89f);
		speed = 1f;
		rSpeed = 3f;
		degree = 0f;
		changeY ();
		obj = GameObject.Find("Plane");
		enable = true;
		obj.SetActive(enable);
	}

	// Update is called once per frame
	void Update () {
		before = transform.position;
		if (Input.GetKeyDown (KeyCode.M)) {
			enable = !enable;
			obj.SetActive(enable);
		}
		if (Input.GetKey(KeyCode.W)) {
			for (int i = 0; i < (int)speed / 0.1f; i++) {
				transform.Translate (Vector3.forward * 0.1f);
				changeY ();
				if (checkMove (transform.position, before)) {
					transform.position = before;
				}
			}
		}
		if (Input.GetKey(KeyCode.S)) {
			for (int i = 0; i < (int)speed / 0.1f; i++) {
				transform.Translate (Vector3.back * 0.1f);
				changeY ();
				if (checkMove (transform.position, before)) {
					transform.position = before;
				}
			}
		}
		if (Input.GetKey(KeyCode.A)) {
			for (int i = 0; i < (int)speed / 0.1f; i++) {
				transform.Translate (Vector3.left * 0.1f);
				changeY ();
				if (checkMove (transform.position, before)) {
					transform.position = before;
				}
			}
		}
		if (Input.GetKey(KeyCode.D)) {
			for (int i = 0; i < (int)speed / 0.1f; i++) {
				transform.Translate (Vector3.right * 0.1f);
				changeY ();
				if (checkMove (transform.position, before)) {
					transform.position = before;
				}
			}
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate (Vector3.right * degree);
			transform.Rotate (Vector3.up * rSpeed);
			transform.Rotate (Vector3.left * degree);
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate (Vector3.right * degree);
			transform.Rotate (Vector3.down * rSpeed);
			transform.Rotate (Vector3.left * degree);
		}
		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.Rotate (Vector3.left);
			degree = degree + 1f;
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			transform.Rotate (Vector3.right);
			degree = degree - 1f;
		}
		if (Input.GetKeyDown (KeyCode.Slash)) {
			transform.Rotate (Vector3.right * degree);
			degree = 0f;
		}
		if (Input.GetKeyDown(KeyCode.I)) {
			if (speed >= 1f) {
				speed = speed + 1f;
			} else {
				speed = speed + 0.1f;
			}
		}
		if (Input.GetKeyDown(KeyCode.K)) {
			if (speed <= 1f && speed >= 0.1f) {
				speed = speed - 0.1f;
			} else if (speed > 1f) {
				speed = speed - 1f;
			}
		}
		if (Input.GetKeyDown(KeyCode.O)) {
			if (rSpeed >= 1f) {
				rSpeed = rSpeed + 1f;
			} else {
				rSpeed = rSpeed + 0.1f;
			}
		}
		if (Input.GetKeyDown(KeyCode.L)) {
			if (rSpeed <= 1f && rSpeed >= 0.1f) {
				rSpeed = rSpeed - 0.1f;
			} else if (speed > 1f) {
				rSpeed = rSpeed - 1f;
			}
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			speed = 1f;
			rSpeed = 3f;
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			transform.position = start;
			changeY ();
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			after = GameObject.Find ("Reticle").transform.position;
			transform.position = after;
			changeY ();
			if (after.y > before.y) {
				if (after.y + 1.8f - transform.position.y > 0.000001f) {
					transform.position = before;
				}
			}
		}
		if (!place ()) {
			transform.position = before;
		}
	}

	private bool checkMove(Vector3 a, Vector3 b) {
		if ((a.y > b.y) && (Mathf.Pow (a.x - b.x, 2f) + Mathf.Pow (a.z - b.z, 2f)) * 4f < Mathf.Pow (a.y - b.y, 2f)) {
			return true;
		} else {
			return false;
		}
	}

	private void changeY() {
		Vector3 pos = transform.position;
		pos.y = Terrain.activeTerrain.SampleHeight (transform.position) + 1.8f;
		transform.position = pos;
	}

	private bool place() {
		Vector3 pos = transform.position;
        // Testudo
        if (pos.x < 183f && pos.x > 182f && pos.z < 90f && pos.z > 88.5f)
        {
            return false;
        }
        // Left hall
        if (pos.x < 155f && pos.x > 108f && pos.z > 139f)
        {
            return false;
        }
        // Right hall(Hopkins)
        if (pos.x < 170f && pos.x > 107f && pos.z < 54f && pos.z > 15f)
        {
            return false;
        }
        // McKeldin Library
        if (pos.x < 270f && pos.x > 190f && pos.z < 127f && pos.z > 50f) {
			return false;
		}
        // World Border
		if (pos.x > 280f || pos.x < 0f || pos.z > 165f || pos.z < 0f) {
			return false;
		}
		return true;
	}
}