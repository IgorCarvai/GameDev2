using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {
	public Transform player;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (this.tag == "Character Dialogue") {
			transform.LookAt (transform.position + player.rotation * Vector3.forward, player.rotation * Vector3.up);
		} else {
			transform.LookAt (player.transform, player.rotation * Vector3.up);
		}
	}
}