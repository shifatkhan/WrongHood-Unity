using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TankController : MonoBehaviour {

	private Rigidbody2D tankRigidBody;
	private Animator tankAnimation;
	private Collider2D tankCollider;

	public Rigidbody2D barrelRigidBody;

	public float jumpForce = 300f;
	private float tankDeadTime = -1f;

	public Text scoreText;
	//private Time startTime;

	public GameObject healthBar;

	public float maxHealth = 100f;
	private float currentHealth;

	// Use this for initialization
	void Start () {

		tankRigidBody = GetComponent<Rigidbody2D> ();
		tankAnimation = GetComponent<Animator> ();
		tankCollider = GetComponent<Collider2D> ();

		currentHealth = maxHealth;
		//startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (tankDeadTime == -1) {
			if (Input.GetButtonDown ("Jump")) {
				tankRigidBody.AddForce (transform.up * jumpForce);
			}

			// Update tank's velocity.
			tankAnimation.SetFloat ("vVelocity", tankRigidBody.velocity.y);

			if (Input.GetButton ("Fire1")) {
				// Mouse tracking
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				barrelRigidBody.transform.rotation = Quaternion.LookRotation (
					Vector3.forward, mousePos - barrelRigidBody.transform.position) * Quaternion.Euler (0, 0, 90);
			}

			//scoreText.text = (Time.time - startTime).ToString("0.0");
		} else {
			if(Time.time > tankDeadTime + 2){
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.collider.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			DealDamageToTank (10f);
		}
	}

	public void DealDamageToTank(float damage) {
		currentHealth -= damage;

		float healthRatio = currentHealth / maxHealth;
		Debug.Log ("ratio: " + healthRatio);

		healthBar.transform.localScale = new Vector3(healthRatio, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

		if(currentHealth <= 0f) {
			Debug.Log ("TANK DIED");
			foreach(EnemySpawner spawner in FindObjectsOfType<EnemySpawner>()) {
				spawner.enabled = false;
			}

			foreach(MoveLeft moveLefter in FindObjectsOfType<MoveLeft>()) {
				moveLefter.enabled = false;
			}

			// Update dead state
			tankDeadTime = Time.time;
			tankAnimation.SetBool ("tankDead", true);

			// Death animation
			tankRigidBody.velocity = Vector3.zero;
			tankRigidBody.AddForce (transform.up * jumpForce);
			tankCollider.enabled = false;
		}
	}
}
