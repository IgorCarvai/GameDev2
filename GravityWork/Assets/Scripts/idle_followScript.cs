using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idle_followScript : MonoBehaviour {

	Animator m_Animator;

	public Transform player;
	// Use this for initialization
	void Start () {
		m_Animator = GetComponent<Animator>();
		m_Animator.SetBool("OnGround", true);
	}
	
	// Update is called once per frame
	void Update () { 
		Vector3 targetPostition = new Vector3( player.position.x, 
		this.transform.position.y, 
			player.position.z ) ;
		this.transform.LookAt( targetPostition ) ;
	}
}
