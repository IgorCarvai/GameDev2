using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndGame: MonoBehaviour {


	
	public Inventory inv;
	public GameObject obj1;//ship-
	public GameObject obj2;//solar panelx
	public GameObject obj3;//solar panel
	public GameObject obj4;//radiator-
	public GameObject obj5;//sendor
	public GameObject obj6;//engine-
	bool found1=false;
	bool found2=false;
	bool found3=false;
	bool found4=false;
	bool found5=false;
	bool found6=false;

	// Use this for initialization
	void Start () {
		inv = GameObject.Find("Inventory").GetComponent<Inventory>();
	}
	
	// Update is called once per frame 
	void Update () {
		
		foreach (Item item in inv.items) {
			if (item.Title == "Fuel Tank"&&!found1) {
				found1=true;
				obj1.SetActive(true);
			}
			if (item.Title == "Solar Panels"&&!found2) {
				found2=true;
				obj2.SetActive(true);
				
			}
			if (item.Title == "Solid Rocket Booster"&&!found3) {
				found3=true;
				obj3.SetActive(true);
				
			}
			if (item.Title == "Radiator"&&!found4) {
				found4=true;
				obj4.SetActive(true);
				
			}
			if (item.Title == "Sensors"&&!found5) {
				found5=true;
				obj5.SetActive(true);
				
			}
			if (item.Title == "Engine"&&!found6) {
				found6=true;
				
				obj6.SetActive(true);
				
			}
					
		}
		if(found1&&found2&&found3&&found4&&found5&&found6){
			Application.LoadLevel(0);
		}	
	}
}
