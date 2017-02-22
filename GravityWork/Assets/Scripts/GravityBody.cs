using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GravityBody uses rigid body and thus if the component does not have one then apply it
[RequireComponent (typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {

	Gravity planet;

	void Awake()
	{
		planet = GameObject.FindGameObjectWithTag ("Enviroment").GetComponent<Gravity>();
		GetComponent<Rigidbody>().useGravity = false;
		if (this.name == "TestPlayer" ||this.name == "PLanet") {
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
		}
	}

	void FixedUpdate(){
		planet.Attract(transform);
	}
}
