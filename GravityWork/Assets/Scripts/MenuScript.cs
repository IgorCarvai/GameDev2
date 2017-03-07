using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

	public Canvas MainC;
	public Canvas OptionsC;

	void Awake(){
		OptionsC.enabled = false;
	}
	public void OptionsOn(){
		OptionsC.enabled = true;
		MainC.enabled = false;
	}
	public void MenuOn(){
		OptionsC.enabled = false;
		MainC.enabled = true;
	}
	public void StartGame(){
		Application.LoadLevel(1);
	}

}
