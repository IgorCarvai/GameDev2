using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour {

	Transform player;
	bool rotated=true;
	void Update()
	{
		player = GameObject.FindWithTag("Player").transform;
		if (Input.GetButtonUp ("Horizontal")) {
			transform.parent = null;
		}
		if (Input.GetButtonUp ("Vertical")) {
			transform.parent = player;
		}
	}
}
