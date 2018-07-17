using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankController : MonoBehaviour {
	
	private Animator turretAnim;

	// Health
	public GameObject healthBar;
	public float maxHealth = 100f;
	private float currentHealth;

	Transform turret;

	void Awake() {
		turret = transform.Find ("Turret1");
		if(turret == null) {
			Debug.LogError ("Turret1 not found!");
		}
	}

	void Start() {
		turretAnim = GetComponent<Animator> ();

		currentHealth = maxHealth;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		CollisionHappened (collision);
	}

	public void CollisionHappened(Collision2D collision) {
		if(collision.collider.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			DealDamageToTank (50f);
		}
	}

	public void DealDamageToTank(float damage) {
		currentHealth -= damage;

		float healthRatio = currentHealth / maxHealth;
		Debug.Log ("ratio: " + healthRatio);

		healthBar.transform.localScale = new Vector3(healthRatio, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

		if(currentHealth <= 0f) {
			turretAnim.SetBool ("turretDead", true);

			transform.Find ("EnemyCanvas").gameObject.SetActive (false);
			transform.Find ("Turret1").gameObject.SetActive (false);

			gameObject.GetComponent<BoxCollider2D> ().enabled = false;

			Debug.Log ("Enemy Tank1 DIED");
		}
	}
}
