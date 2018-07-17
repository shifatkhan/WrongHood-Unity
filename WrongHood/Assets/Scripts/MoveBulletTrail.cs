using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulletTrail : MonoBehaviour {

	public int moveSpeed = 100;
	public bool enemy = false;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if (enemy) {
			transform.Translate (Vector3.left * Time.deltaTime * moveSpeed);
			Destroy (gameObject, 1f);
		} else {
			transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
			Destroy (gameObject, 1f);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Destroy (gameObject);
	}
}
