using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerOnHit : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D obj) {

		Destroy (obj.gameObject);

	}
}
