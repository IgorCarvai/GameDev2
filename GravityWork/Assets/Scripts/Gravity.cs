using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Code adapted from Sebastian Lague
/// </summary>
public class Gravity : MonoBehaviour {

	public float gravity = -10f;
	public void Attract(Transform body){
		Vector3 targetDirection = (body.position - transform.position).normalized;

		if (body.tag == "Player"|| body.tag == "Object") {
			body.rotation = Quaternion.FromToRotation (body.up, targetDirection) * body.rotation;
		}

		body.GetComponent<Rigidbody>().AddForce (targetDirection * gravity);
	}
}
