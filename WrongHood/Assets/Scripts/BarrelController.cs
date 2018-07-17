using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour {

	public float effectRate = 10f;
	public float fireRate = 0.2f;
	public float damage = 10;
	public LayerMask whatToHit;

	public Transform bulletTrailPrefab;
	public Transform muzzleFlashPrefab;

	private float timeToSpawnEffect = 0;
	private float timeToFire = 0f;
	Transform firePoint;

	public bool automatic = false;
	public bool enemy = false;

	// Use this for initialization on current object
	void Awake () {
		firePoint = transform.Find ("FirePoint");
		if(firePoint == null) {
			Debug.LogError ("Fire Point not found!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (automatic) {
			if (fireRate == 0) {
				Shoot ();
			} else {
				if (Time.time > timeToFire) {
					timeToFire = Time.time + 1 / fireRate;
					Shoot ();
				}
			}
		}
		else {
			if (fireRate == 0) {
				if (Input.GetButtonDown ("Fire1")) {
					Shoot ();
				}
			} else {
				if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
					timeToFire = Time.time + 1 / fireRate;
					Shoot ();
				}
			}
		}

	}

	private void Shoot() {
		if (enemy) {
			Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
				Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			Vector2 firePointPos = new Vector2 (firePoint.position.x, firePoint.position.y);

			RaycastHit2D hit = Physics2D.Raycast (firePointPos, mousePosition - firePointPos, 100,
				whatToHit);

			if(Time.time >= timeToSpawnEffect) {
				Transform bullet = Effect ();
				timeToSpawnEffect = Time.time + 1 / effectRate;

				if(hit.collider != null) {
					Destroy (bullet.gameObject, 0.1f);
					Destroy (hit.collider.gameObject, 0.1f);
				}
			}

			if(hit.collider != null) {
				Debug.Log (damage + " damage to " + hit.collider.name);
			}
		} else {
			Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
				Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
			Vector2 firePointPos = new Vector2 (firePoint.position.x, firePoint.position.y);

			RaycastHit2D hit = Physics2D.Raycast (firePointPos, mousePosition - firePointPos, 100,
				whatToHit);

			if(Time.time >= timeToSpawnEffect) {
				Transform bullet = Effect ();
				timeToSpawnEffect = Time.time + 1 / effectRate;

				if(hit.collider != null) {
					Destroy (bullet.gameObject, 0.1f);
					Destroy (hit.collider.gameObject, 0.1f);
				}
			}

			if(hit.collider != null) {
				//Debug.Log (damage + " damage to " + hit.collider.name);
			}
		}
	}

	private Transform Effect() {
		Transform bullet = (Transform) Instantiate (bulletTrailPrefab, firePoint.position, firePoint.rotation);

		Transform clone = (Transform) Instantiate (muzzleFlashPrefab, firePoint.position, firePoint.rotation);

		clone.parent = firePoint;
		float size = Random.Range(0.6f, 0.9f);
		clone.localScale = new Vector3 (size, size, 0);

		// Pause and destroy
		Destroy (clone.gameObject, 0.1f);

		return bullet;
	}
}
