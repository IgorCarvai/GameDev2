using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {
	public Transform player;
	public int x, y, z;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(transform.position + player.rotation * Vector3.forward,
			player.rotation * Vector3.up);
	}
}
