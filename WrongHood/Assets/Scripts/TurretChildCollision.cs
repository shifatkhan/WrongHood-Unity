using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretChildCollision : MonoBehaviour {

	private EnemyTankController turretScript;

	void Start () {
		turretScript = GetComponentInParent<EnemyTankController> ();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		turretScript.CollisionHappened (collision);
	}
}
