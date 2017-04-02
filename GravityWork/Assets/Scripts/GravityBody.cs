	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GravityBody uses rigid body and thus if the component does not have one then apply it
[RequireComponent (typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {

	public Gravity planet;

	void Awake()
	{
		planet = GameObject.FindGameObjectWithTag ("Enviroment").GetComponent<Gravity>();
		GetComponent<Rigidbody>().useGravity = false;
		if (this.tag == "Player" ||this.name == "Planet") {
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeRotation;
		}
	}

	void FixedUpdate(){
		planet.Attract(transform);
	}

	void OnTriggerEnter(Collider col){
		planet=col.GetComponent <Gravity>();
	}
}
