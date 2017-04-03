	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GravityBody uses rigid body and thus if the component does not have one then apply it
[RequireComponent (typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {

	Gravity planet;

	bool foundT=false;
	bool foundA=false;
	bool foundR=false;

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
		if (col.tag == "Enviroment") {
			planet = col.gameObject.GetComponent<Gravity> ();
		}
	}
	public void setFoundTangie(bool x){
		foundT = x;
	}
	public void setFoundAlvin(bool x){
		foundA = x;
	}
	public void setFoundRidi(bool x){
		foundR = x;
	}
	public bool FoundRidi(){
		return foundR;
	}
	public bool FoundTangie(){
		return foundT;
	}
	public bool FoundAlvin(){
		return foundA;
	}
}
