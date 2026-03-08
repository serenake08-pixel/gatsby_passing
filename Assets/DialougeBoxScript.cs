using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class DialougeBoxScript : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI textComponent;
    public float textSpeed;
    private Dictionary<string, List<string>> dialogues;
    public List<string> currentLines;
    private int currentIndex;
    public Boolean dialogueFinished = true;
    public Boolean typing = false;

    //below are booleans that might be used for a different script.
    private List<string> gold;
    private List<string> silver;
    private List<string> bronze;
    private List<string> artifacts;

    void Start()
    {
        textSpeed = 0.02f;
        textComponent = GetComponent<TextMeshProUGUI>();
        if (textComponent == null) {textComponent = GetComponentInChildren<TextMeshProUGUI>();}

        dialogues = new Dictionary<string, List<string>>();

        dialogues["hello1"] = new List<string> {
            "You have woken up on a typical day in the 1920s. It's a beautiful day outside.",
            "There isn't much time to waste, so you should start getting ready.",
            "..."
        };
        dialogues["hello2"] = new List<string> {
            "HUH? What's this? Why is your room randomly littered with stuff you don't own?",
            "Who could've done this?",
            "Well, in any case, you've got a party to go to, so there's no use dwelling over it.",
            "Let's get this all sorted out quickly."
        };
        dialogues["empty1"] = new List<string> {"Nothing especially swanky here."};
        dialogues["empty2"] = new List<string> {"Yes yes, I know this place is the bee’s knees but don’t just stand here!"};
        dialogues["empty3"] = new List<string> {"Let's keep looking, Old Sport."};

        dialogues["bed"] = new List<string> {
            "You admire the bed and the headboard, which pairs nicely under the wall design.",
            "So classy."
        };

        dialogues["shirts"] = new List<string> {
            "Clothes. Clothes and clothes and clothes.",
            "This is a collection of your favorite shirts.",
            "You have a dedicated room for all your clothing.",
            "You can never have too many!"
        };

        dialogues["phonograph"] = new List<string> {
            "Ah, the phonograph. Some innovative stuff, if you do say so yourself.", 
            "Here, let’s actually put on some nice music.",
            "There we go! How pleasant it is.",
        };

        dialogues["chair"] = new List<string> {"Chair :D"};

        dialogues["books1"] = new List<string> {
            "A neat little collection of books.",
            "There are many more in the library!",
            "You should probably read more of them, something you've been meaning to do.",
            "You mentally remind yourself to pick out a book before you go."
        };

        dialogues["books2"] = new List<string> {
            "Looking a little closer at the books, the Bible catches your eye",
            ""
        };

        dialogues["cassidy"] = new List<string> {
            "It is titled Hopalong Cassidy. The book details a self-improvement schedule.",
            "The list includes practicing poise and elocution, exercising, and saving money.",
            "All that hard work of a man who reinvented himself from the group up…let’s tuck this book away."
        };

        dialogues["lamp"] = new List<string> {
            "A lamp. But not just any old lamp!",
            "This is a scarab lamp.",
            "Did you know that scarabs are a symbol of rebirth and protection, from Ancient Egyptian culture?",
            "They represented the sun god Khepri.",
            "See, when I turn it on, the scarab starts to glow."
        };

        
        dialogues["vainity1"] = new List<string> {
            "It's you!"
        };  

        dialogues["vainity2"] = new List<string> {
            "It's you!"
        };  

        dialogues["letters"] = new List<string> {
            "It's you!"
        };

        dialogues["telephone"] = new List<string> {
            "It's you!"
        };  

        dialogues["oxford"] = new List<string> {
            "It's you!"
        };  
/*
        yield return StartCoroutine(PlayDialouge("hello1"));
        yield return GameObject.Find("Main Camera").GetComponent<CameraScript>().ShakeCamera();
        yield return StartCoroutine(PlayDialouge("hello2"));
*/
    

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (!typing)
            {
                NextLine();
            }
        }
    }

    public IEnumerator PlayDialogue(string dialougeName)
    {
        dialogueFinished = false;
        currentLines = dialogues[dialougeName];
        textComponent.text = "";
        currentIndex = 0;
        yield return StartCoroutine(TypeLine());
        yield return new WaitUntil(() => dialogueFinished);
        InfoBoxScript infoBox = GameObject.Find("InfoBox").GetComponent<InfoBoxScript>();
        if (infoBox.infoBoxes.ContainsKey(dialougeName))
        {
            yield return getInfoBox(dialougeName);
        }
        dialogueFinished = false;
    }

    IEnumerator getInfoBox(string dialougeName)
    {
        yield return GameObject.Find("InfoBox").GetComponent<InfoBoxScript>().ActivateInfoBox(dialougeName);
    }


    void NextLine(){ //type next line or end dialogue
        if (currentIndex < currentLines.Count - 1)
        {
            currentIndex++;
            textComponent.text = "";
            StartCoroutine(TypeLine());
        }
        else //end and call infobox
        {
            textComponent.text = "";
            dialogueFinished = true;
        }     
    }



    IEnumerator TypeLine()
    {
        typing = true;
        textComponent.text = currentLines[currentIndex];
        textComponent.maxVisibleCharacters = 0; 
        string line = currentLines[currentIndex];
        for (int i = 0; i <= line.Length; i++)
        {
            textComponent.maxVisibleCharacters = i;
            yield return new WaitForSeconds(textSpeed);
        }
        typing = false;
    }

    IEnumerator TypeLine(string line){
        typing = true;
        foreach(char c in line.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        typing = false;
    }
}
