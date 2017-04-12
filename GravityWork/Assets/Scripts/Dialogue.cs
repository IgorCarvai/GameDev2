using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dialogue : MonoBehaviour {

    private Text _textComponent;
    private Text speaker;

    public string whoTalking;
    string[] DialogueStrings = {
        "L: I hate gravity fluxes.",
        "L: (The heck is up with the gravity systems today?)",
        "L: Status report before we break atmosphere.",
        "T: Really? We're really doing this NOW?",
        "R: Aw, lay off Tangie. Cappy just got done settling some widdle tummy troubles.",
        "A: Ah! Um! En route to Auroran's troposphere with no anticipated delays, Captain.",
        "L: Excel-",
        "R: Spoke too soon. Went and incurred the wrath of Murphy you did.",
        "R: (This, ladies and gentlemen, is what you might call Murphy's law.)",
        "   ...",
        "L: Well, don't just stand there. Get to your escape pods, tykes.",
        "   ",
        "   ",
	"   ...",
	"   ...",
	"S: --is dangerous. We ask that you take what you've already gathered and return to the empire immediately",
	"L: ...",
        "L: This is your captain speaking. All units report.",
        "L: Role call everyone.",
        "L: Captain Lua here. Anyone who can hear this sound off.", 
        "L: (and give me status updates...)",
        "   ...",
        "L: All units report!",
        "   ...",
        "L: Rats!",
        "R: ---anging from a tree~ Never beeeeen so happy as haaaan--",
        "L: Ridi?! Ridi, you there?",
        "R: Eh? Oh hey Cappy! Thanks for tuning in to Crashlander's Community Radio.",
        "L: Not  the time Ridi. Where are you?",
        "R: Eh, I dunno maybe... Hanging from a treeee~--",
        "L: Beyond that.",
        "R: Beats me,  You the navingator.",
        "L: At least describe your surroundings.",
        "R: Ummmmmmm... more trees?",
        "L: (Sigh)",
        "L: Just stay put until I can get to you.",
        "R: Sure, sure. Guess I'm just gonna have to sing even louder now to trumpet my location and all that.",
        "L: ...Sure,  Have fun.",
        "R: I will~",
        "   ",
        "T: --Pod thr--",
        "L: Captain Lua here. Can your hear me?",
        "T: --Cap--ain? Tangi---f Pod --",
        "L: Repeat that. Your channel's a little distorted, Tangie.",
        "T: ---ng it. h--- on.",
        "T: Better?",
        "L: Much.",
        "T: Finally.",
        "L: What happened?",
        "T: Stupid pod decided to crashlandland in a gorge. Shot my reception to a zillion peices. Took me longer than I 'd like to rid up a Comm extender.",
        "L: Can you get out?",
        "T: I'm trying but...",
        "L: But?",
        "T: But... Ithinkmylegsbroken.",
        "T: ...",
        "L: Ah. Well. That's a thing.",
        "T: Yeah. But I think I can get out if I just t--",
        "L: Um, no. You're injured, Tangie. Don't aggrivate it more by wiggling aroud.",
        "T: But--",
        "L: No. As my subordinate, I need you to stay still and safe until I can reach you.",
        "T: Fiiine.",
        "    ",
        "L: Annd, Aven? Aven can you hear me?",
        "   ...",
        "L: Darnit, his radio must have been destroyed in the landing.",
        "   ...",
        "L: I'll try to gather Ridi and Tangie before I set out to look for Aven",
        "L: (Stay put, Aven.)"
    };

    public float SecondsBetweenCharacters = 0.15f;
    public float CharacterRateMultiplier = 0.5f;

    public KeyCode DialogueInput = KeyCode.Return;

    private bool _isStringBeingRevealed = false;
    private bool _isDialoguePlaying = false;
    private bool _isEndOfDialogue = false;

    public GameObject ContinueIcon;
    public GameObject StopIcon;

    // Use this for initialization
    void Start()
    {
        _textComponent = GetComponent<Text>();
        speaker = GameObject.Find("Speaker").GetComponent<Text>();
        _textComponent.text = "";
        speaker.text = "";

        HideIcons();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyUp (KeyCode.P)) {
			_isEndOfDialogue = true;

			_isDialoguePlaying = false;
		}
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!_isDialoguePlaying)
            {
                _isDialoguePlaying = true;
                StartCoroutine(StartDialogue());
            }

        }

		if (_isEndOfDialogue && Application.loadedLevel==2) {
			Application.LoadLevel (3);
		}
    }

    private IEnumerator StartDialogue()
    {
        int dialogueLength = DialogueStrings.Length;
        int currentDialogueIndex = 1;

        while (currentDialogueIndex < dialogueLength || !_isStringBeingRevealed)
        {
            if (!_isStringBeingRevealed)
            {
                _isStringBeingRevealed = true;
                StartCoroutine(DisplayString(DialogueStrings[currentDialogueIndex++]));

                if (currentDialogueIndex >= dialogueLength)
                {
                    _isEndOfDialogue = true;
                }
            }

            yield return 0;
        }

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }

        HideIcons();
        _isEndOfDialogue = false;
        _isDialoguePlaying = false;
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;

        HideIcons();

        _textComponent.text = "";

        //parse strings
        if (stringToDisplay.Contains("L:"))
            whoTalking = "Lua";

        else if (stringToDisplay.Contains("T:"))
            whoTalking = "Tangie";

        else if (stringToDisplay.Contains("R:"))
            whoTalking = "Ridi";

        else if (stringToDisplay.Contains("A:"))
            whoTalking = "Aven";

	else if (stringToDisplay.Contains("S:"))
            whoTalking = "Lt. Sangr";
	    
        else
            whoTalking = "";

        speaker.text = whoTalking;

        if (stringToDisplay.Contains("(") && stringToDisplay.Contains(")"))
        {
            Color c = new Color();

            c = _textComponent.color;
            c.a = .5f;

            _textComponent.color = c ;
            speaker.text = whoTalking;
        }
        else
        {
            _textComponent.color = Color.black;
        }

        while (currentCharacterIndex < stringLength-3)
        {
            _textComponent.text += stringToDisplay[currentCharacterIndex+3];
            currentCharacterIndex++;

            if (currentCharacterIndex < stringLength)
            {
                if (Input.GetKey(DialogueInput))
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters * CharacterRateMultiplier);
                }
                else
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters);
                }
            }
            else
            {
                break;
            }
        }

        ShowIcon();

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }

        HideIcons();

        _isStringBeingRevealed = false;
        _textComponent.text = "";
        speaker.text = "";
    }

    private void HideIcons()
    {
        ContinueIcon.SetActive(false);
        StopIcon.SetActive(false);
    }

    private void ShowIcon()
    {
        if (_isEndOfDialogue)
        {
            StopIcon.SetActive(true);
            return;
        }

        ContinueIcon.SetActive(true);
    }
}
