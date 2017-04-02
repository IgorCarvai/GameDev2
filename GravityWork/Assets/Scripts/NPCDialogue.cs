using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


/* when player interacts with NPC characters, check the tag and display correct script for corresponding situation*/

public class NPCDialogue : MonoBehaviour
{

	public static int sceneNumber;
	public int timeToStrike;
	public string dialogueLine;
	public string item, fact;
	public bool isBuilding = false;
	public bool built = false;
	private string speaker;
	private int nextText;

	public Text displayLine;
	public Text displayPerson;

	private GameObject NPC;

    void Start()
    {

        NPC = this.gameObject;
		displayLine = GameObject.FindWithTag("Character Dialogue").GetComponent<Text>();
		displayPerson = GameObject.Find("Speaker").GetComponent<Text>();

		sceneNumber = Application.loadedLevel;
		nextText = 1;

		dialogueLine = " ";
		speaker = " ";
		displayLine.text = dialogueLine;
		displayPerson.text = speaker;
		checkConditions ();
    }

	public void hideText(){
		displayLine.enabled=false;
		displayPerson.enabled = false;
	}
	public void showText(){

		displayLine.enabled = true;
		displayPerson.enabled = true;
	}
	
    public void checkConditions()
    {
        dialogueLine = " ";
		speaker = NPC.tag;
        if (NPC.tag == "Aven")
        {
            if (sceneNumber == 3)//world after prologue
            {
                dialogueLine = "I was looking all over for you.";
            }
            else if (sceneNumber >= 4)//inside spaceship
            {
                dialogueLine = "Would you like to see the wonders of crafting?";
            }
            else if (sceneNumber == 7)//right after empire strike
            {
                dialogueLine = "We've prepared for months to come on this mission and they decide to terminate it now? Who gives them the right?";
            }
        }

        else if (NPC.tag == "Ridi")
        {
            if (sceneNumber == 3)//world after prologue
            {
                dialogueLine = "-happy as can be~ Oh hey Cappy!";
            }
            else if (sceneNumber >= 4)//inside spaceship
            {
                dialogueLine = "You know the " + item + "in your backpack? Well, did you know " + fact + "?";
            }
            else if (sceneNumber == 7)//right after empire strike
            {
                dialogueLine = "The life here is actually extremely dangerous for our species. You should rethink breathing in this air. .... I'm just pulling your leg. Haha.";
            }
        }

        else if (NPC.tag == "Tangie")
        {
            if (sceneNumber == 3)//world after prologue
            {
                dialogueLine = "I could've got myself out if you let me.";
            }
            else if (sceneNumber >= 4 && !isBuilding)//inside spaceship
            {
                dialogueLine = "Did you need something built?";
            }
            else if (sceneNumber >= 4 && isBuilding)//inside spaceship
            {
                dialogueLine = "Just give me a little more time. ";
            }
            else if (sceneNumber >= 4 && built)//right after empire strike
            {
                dialogueLine = "The thing you told me to do? Well it's all done.";
            }
        }

        else if (NPC.tag == "NPC")
        {
            if (sceneNumber >= 3)
            {
                dialogueLine = "Leave our planet.";
            }
        }

        else if (NPC.tag == "Scientist")
        {

			if (sceneNumber == 1){

				if(nextText==1)
					dialogueLine = "Hey Firend, come over and talk to me. you can interactive with me by pressing E";

				if(nextText==2)
					dialogueLine = "GoodJob, go over and get that crystal adn talk to me after";
				
				if(nextText==3)
					dialogueLine = "Great! now lets get going. Press E to leave";				
				nextText++;
			}
            /*
            check the world time in comparison to days left, let's say on day 5 thing will strike
            countdown in days to that day
            */
        }
	
	else if (NPC.tag == "Radio")
	{
		if (sceneNumber == 3){
			dialogueLine = "This is Lt. Sangr speaking. Captain are you there? Capt?" 
			
			/*"... Shortly, after your departure, Leader Cerul has terminated your mission." 
			"We have proof to believe that planet Auroran is dangerous." 
			"We ask that you take what you've already gathered and return to the empire immediately."*/
		}
		else if (sceneNumber == 5){
			dialogueLine = "";
		}
	}

        displayLine.text = dialogueLine;

		displayPerson.text = speaker;

    }
}
