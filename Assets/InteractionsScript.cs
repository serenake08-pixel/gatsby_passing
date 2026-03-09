using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionsScript : MonoBehaviour
{
    [SerializeField] public Sprite interactionSprite;
    public static bool isInteracting = false;
    private bool keyDown = false;
    public static bool anyTouching = false;
    public bool isTouching = false;
    private static int count = 0;


    private Dictionary<string, int> dialogueProgressions = new Dictionary<string, int>();
    [TextArea] public string interactionName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //initialize dialouge Progressions!
        dialogueProgressions["books"] = 1;
        dialogueProgressions["vainity"] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            keyDown = true;
        }
        else{
            keyDown = false;
        }
        if (interactionName == "bed")
        {
            //Debug.Log("Is Touching: " + isTouching + " Any Touching: " + anyTouching + " Is Interacting: " + isInteracting);
        }

        if (keyDown && isTouching && !isInteracting)
        {
            isInteracting = true;
            StartCoroutine(StartInteraction(interactionName));    
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Touching Player");
        if (collision.CompareTag("Player")) 
        {
        isTouching = true;
        anyTouching = true;
        count++;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
        isTouching = true;
        anyTouching = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
        isTouching = false;
        anyTouching = false;
        //Debug.Log("Not Touching Player");
        }
    }

    public IEnumerator StartInteraction(string interactionName)
    {

        DialougeBoxScript dialogueBox = GameObject.Find("DialougeBox").GetComponent<DialougeBoxScript>();

        if (dialogueProgressions.ContainsKey(interactionName)) //has multiple interaction times
        {
            if (dialogueProgressions[interactionName] >= 3) //exhausted interactions.
            {
                yield return StartCoroutine(dialogueBox.PlayDialogue(interactionName + 2));
            }
            else {yield return StartCoroutine(dialogueBox.PlayDialogue(interactionName + dialogueProgressions[interactionName].ToString()));}
            dialogueProgressions[interactionName]++;
        }

        else { yield return StartCoroutine(dialogueBox.PlayDialogue(interactionName)); }
        //dialouge has finished, now time for info boxes
        
        //info boxes done, now time for changes

        isInteracting = false;
    }
}
