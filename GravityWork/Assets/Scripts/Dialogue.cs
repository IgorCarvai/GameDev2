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
        "L: Well, don't just stand there. Get to your escape pods, tykes." };

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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!_isDialoguePlaying)
            {
                _isDialoguePlaying = true;
                StartCoroutine(StartDialogue());
            }

        }
    }

    private IEnumerator StartDialogue()
    {
        int dialogueLength = DialogueStrings.Length;
        int currentDialogueIndex = 0;

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