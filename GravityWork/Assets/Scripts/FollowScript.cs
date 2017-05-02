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
		}if (this.tag == "FollowArrow") {
			
			Vector3 relativePos = player.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			transform.rotation = rotation;
			transform.localEulerAngles = new Vector3(
				90f,
				transform.localEulerAngles.y,
				transform.localEulerAngles.z
			);

		} else {
			transform.LookAt (player.transform, player.rotation * Vector3.up);
		}
	}

}