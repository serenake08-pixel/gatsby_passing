using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class DialougeBoxScript : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Sprite clearBox;
    [SerializeField] Sprite notClearBox;

    private UnityEngine.UI.Image imageComponent;

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

    void Awake()
    {
        imageComponent = GetComponent<UnityEngine.UI.Image>();
        Debug.Log("imageComponent found: " + imageComponent);
        Debug.Log("Script is on: " + gameObject.name);
        imageComponent.sprite = clearBox;
        textSpeed = 0.02f;
        textComponent = GetComponent<TextMeshProUGUI>();
        if (textComponent == null) {textComponent = GetComponentInChildren<TextMeshProUGUI>();}
        textComponent.text = "";

        dialogues = new Dictionary<string, List<string>>();

        dialogues["hello1"] = new List<string> {
            "You have woken up on a typical day in the 1920s. It's a beautiful day outside.",
            "There isn't much time to waste, so you should start getting ready.",
            "..."
        };
        dialogues["hello2"] = new List<string> {
            "HUH? What's this? Why is your room randomly littered with stuff you don't own?",
            "This is strangely unsettling. You have no idea how this could have happened.",
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
            "You have another dedicated room in the mansion for all your clothing.",
            "You can never have too many!"
        };

        dialogues["phonograph"] = new List<string> {
            "Ah, the phonograph. A couple hundred dollars of innovative stuff, if you do say so yourself.", 
            "Here, let’s actually put on some nice music.",
            "There we go! How pleasant it is.",
        };

        dialogues["chair"] = new List<string> {"Chair :)"};

        dialogues["books1"] = new List<string> {
            "A neat little collection of books.",
            "There are many more in the library!",
            "You should probably read more of them, something you've been meaning to do.",
            "You mentally remind yourself to come back later to pick out a book before you head out. Just in case."
        };

        dialogues["books2"] = new List<string> {
            "Looking a little closer at the books, your Bible catches your eye",
            "As you touch it, you see the struggle of a young girl who was made to work by a religious household, in exchange for a roof over her head.",
            "She wanted to be her own person, and so she left.",
            "You put it back on the shelf. You won't be going near that for a while."
        };

        dialogues["cassidy"] = new List<string> {
            "'Hopalong Cassidy'. The book details a self-improvement schedule.",
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
            "It's you!",
            "There are also various beauty products here.",
            "A particular red lipstick catches your eye.",
            "This poor thing has been through so many lies and smiles. The lies and smiles of a black woman passing as white, who believes it is the worth the money it brings her.",
            "You take the lipstick and discard it.",
        };  

        dialogues["vainity2"] = new List<string> {
            "Your reflection in the mirror looks cracked and distorted.",
            "Gosh, what happened to this mirror?",
            "It needs to be replaced right away!",
            "You'll have to deal with this after the party, though. But it must be fixed before you have any guests over."
        };  

        dialogues["letters"] = new List<string> {
            "Letters detailing the longing of the woman who realized she lost something in herself too late.",
            "Another letter from the man who was so confident that wealth and the American Dream would bring him back to his lover.",
            "These letters don't make you feel too good.",
            "You toss them in the trash."
        };

        dialogues["telephone"] = new List<string> {
            "You don't keep the telephone in the bedroom.",
            "This isn't even the same telephone you have!",
            "You pick it up. It occurs to you that this was the telephone of the man who worked as a bootlegger to make his fortune.",
            "...You put it down."
        };  

        dialogues["oxford"] = new List<string> {
            "You open the elegant pocketwatch.",
            "There's a picture of a man holding a cricket bat along with a group of people.",
            "The background is from the University of Oxford.",
            "How nice. You let it rest there."
        };  

    }

    IEnumerator Start()
    {
        InteractionsScript.isInteracting = true;
        yield return StartCoroutine(PlayDialogue("hello1"));
        yield return GameObject.Find("Main Camera").GetComponent<CameraScript>().ShakeCamera();
        yield return StartCoroutine(PlayDialogue("hello2"));
        InteractionsScript.isInteracting = false;
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
        imageComponent.sprite = notClearBox;
        dialogueFinished = false;
        currentLines = dialogues[dialougeName];
        textComponent.text = "";
        currentIndex = 0;
        yield return StartCoroutine(TypeLine());
        yield return new WaitUntil(() => dialogueFinished);
        InfoBoxScript infoBox = GameObject.Find("InfoBox").GetComponent<InfoBoxScript>();
        imageComponent.sprite = clearBox;
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
