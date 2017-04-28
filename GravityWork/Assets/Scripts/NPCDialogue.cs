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
    }

    public void hideText()
    {
        displayLine.enabled = false;
        displayPerson.enabled = false;
    }
    public void showText()
    {

        displayLine.enabled = true;
        displayPerson.enabled = true;
    }
    public void checkConditions(int player, int textNumber)
    {
        if (player == 1)
        {
            speaker = "Avin";
        }
        else if (player == 2)
        {
            speaker = "Ridi";
        }
        else if (player == 3)
        {
            speaker = "Tangie";
		}  
		else if (player == 4)
		{
			speaker = "Mechanic";
		}
        dialogueLine = " ";
        if (speaker == "Avin")
        {
            if (sceneNumber == 3)//world after prologue
            {
                if (textNumber == 1)
                    dialogueLine = "I was looking all over for you.";
                if (textNumber == 2)
                    dialogueLine = "Go and find the others. I'll meet you at the base.";
                if (textNumber == 3)
                    dialogueLine = "Still haven't found the others?";
                if (textNumber == 4)
                    dialogueLine = "We are still mising Ridi.";
				if (textNumber == 5)
					dialogueLine = "Find Tangie, then come here.";

            }
            else if (sceneNumber >= 4)//cutscene 2
            {
				if(textNumber == 0)
	                dialogueLine = "Find objects around the planet and use them to craft using inventory.\n Mind finding some things for me so we can fix the ship?\n If you come across any debris, like Bones, Steel scraps, Wires, and Mirror Shards, I can probably jurryrig some parts.";
				if (textNumber == 1)
					dialogueLine = "Here's the compontenets we need:\n 5 Steel scraps and 1 Wire make a Piston.\n 1 Cog and 1 Steel scrps make a Screw.\n 1 Steel and 1 Bone make a Cog.";
				if (textNumber == 2)
					dialogueLine = "10 Mirror Shards, 5 Steel scraps, and 1 Wire make the Solar Panels.\n 5 Cogs, 5 Wires, and 10 Steel scraps make the Sensors.";
				if (textNumber == 3)
					dialogueLine = "5 Screws, 5 Wires, and 3 Pistons make the Engine.\n 10 Screws, 10 Steel scraps, and 2 Pistons make the Solid Rocket Booster.\n 10 Wires, 15 Screws, and 10 Steel scraps make the Fuel Tank.\n 5 Steel scraps, and 10 Screws make the Radiator.";  
			}
        }

        else if (speaker == "Ridi")
        {
			if (sceneNumber == 3) {//world after prologue
				if (textNumber == 1)
					dialogueLine = "Sup Cappy!";
				if (textNumber == 2)
					dialogueLine = "Imma hang out in this this neato tree for a bit, but I'll be at the base soon enough. Good luck finding others.";
				if (textNumber == 3)
					dialogueLine = "Sure are taking thier sweet time reconvening. Wonder if those nerds got lost trying to find the ship.";
				if (textNumber == 4)
					dialogueLine = "Wheres Tangie?";
				if (textNumber == 5)
					dialogueLine = "Seems like Avin always gets left behind.";
			}  else if (sceneNumber == 4) {//inside spaceship
				if (textNumber == 1)
					dialogueLine = "Parallel Lines have so much in common.";
				if (textNumber == 2)
					dialogueLine = "It's a shame they'll never meet.";
				if (textNumber == 3)
					dialogueLine = "Man, I can't believe how emotional Mom and Dad's wedding was.";
				if (textNumber == 4)
					dialogueLine = "Even the cake was in tiers.";
				if (textNumber == 5)
					dialogueLine = "Why do cows wear bells?";
				if (textNumber == 6)
					dialogueLine = "Because their horns don't work.";
				if (textNumber == 7)
					dialogueLine = "I took the shell out of a snail to see if it would go fasters.";
				if (textNumber == 8)
					dialogueLine = "All I did was make it more sluggish.";
				if (textNumber == 9)
					dialogueLine = "I used to try and catch fog.";
				if (textNumber == 10)
					dialogueLine = "But all I did was mist.";
				if (textNumber == 11)
					dialogueLine = "Whats the fastest way of getting out of a planet?";
				if (textNumber == 12)
					dialogueLine = "By finding all the pieces, building the ship and leaving.";
				if (textNumber == 13)
					dialogueLine = "Come on, find everything and lets go.";
				if (textNumber == 14)
					dialogueLine = "Come on, find everything and lets skiddaddle.";
				if (textNumber == 15)
					dialogueLine = "Please go and find everything so we can leave. \n Pleeeeeeeaaaaaaasssseee.";
				if (textNumber == 16)
					dialogueLine = "Ugh fine.\n I'll tell you the jokes again.";
				
			}
        }

        else if (speaker == "Tangie")
        {
            if (sceneNumber == 3)//world after prologue
            {
                if (textNumber == 1)
                    dialogueLine = "I could've got myself out if you let me.";
                if (textNumber == 2)
                    dialogueLine = "Did you find the others yet? No? Then what are you still doing here? I'll meet you at base when you do.";
                if (textNumber == 3)
                    dialogueLine = "I'm busy here since no one is around. Hurry up and find the others.";
                if (textNumber == 4)
                    dialogueLine = "We are still mising Ridi";
				if (textNumber == 5)
					dialogueLine = "We need the whole team. Go find Avin.";
            }
            else if (sceneNumber == 4 )//inside spaceship
            {
				if (textNumber >= 1)
					dialogueLine = "Lets get building.";
			}
        }
		else if (speaker == "Mechanic")
		{
			if (sceneNumber == 1)
			{
				if (textNumber == 1)
					dialogueLine = "Hey Friend, come over and talk to me. You can interactive with me by pressing the E key.";

				if (textNumber == 2)
					dialogueLine = "Okay we are almost ready to go. We just need to power up the ship with a crystal. Find one and come talk to me after.";

				if (textNumber == 3)
					dialogueLine = "Hey, looks like you found it. You can press I to check it out in your inventory.\n Also since you are near me I can help you build objects. At the moment there is nothing that needs to be built. But go ahead and try to check your inventory.";

				if (textNumber == 4)
					dialogueLine = "Pretty cool huh. Okay now give me the crystal and I'll put it on the ship.\n (Hint: Press E)";

				if (textNumber == 5)
					dialogueLine = "We will be all set pretty soon. Go ahead and tell the Pilot that we are all good to go.";
			}

			/*
            check the world time in comparison to days left, let's say on day 5 thing will strike
            countdown in days to that day
            */
		}
        else if (NPC.tag == "NPC")
        {
            if (sceneNumber >= 3)
            {
                dialogueLine = "Leave our planet.";
            }
        }

        

        else if (NPC.tag == "Radio")
        {
            if (sceneNumber == 3)
            {
                dialogueLine = "This is Lt. Sangr speaking. Captain are you there? Capt?";

                /*"... Shortly, after your departure, Leader Cerul has terminated your mission." 
                "We have proof to believe that planet Auroran is dangerous." 
                "We ask that you take what you've already gathered and return to the empire immediately."*/
            }
            else if (sceneNumber == 5)
            {
                dialogueLine = "";
            }
        }

        displayLine.text = dialogueLine;

        displayPerson.text = speaker;

    }
}

