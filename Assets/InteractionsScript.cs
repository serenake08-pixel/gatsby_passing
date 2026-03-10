using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class InteractionsScript : MonoBehaviour
{
    [SerializeField] public Sprite interactionSprite;
    [SerializeField] public Sprite newSprite;
    public static bool isInteracting = false;
    private bool keyDown = false;
    public static bool anyTouching = false;
    public bool isTouching = false;
    private static List<string> interactedWith = new List<string>();
    [SerializeField] AudioSource musicPlayer;


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
            if (interactionName == "books" && dialogueProgressions[interactionName] >= 3) //books special case
            {
                yield return StartCoroutine(dialogueBox.PlayDialogue("empty1"));
            }
            else if (dialogueProgressions[interactionName] >= 3) //exhausted interactions.
            {
                yield return StartCoroutine(dialogueBox.PlayDialogue(interactionName + 2));
            }
            else {
                interactedWith.Add(interactionName+ interactionName + dialogueProgressions[interactionName].ToString());
                yield return StartCoroutine(dialogueBox.PlayDialogue(interactionName + dialogueProgressions[interactionName].ToString()));
            }
            dialogueProgressions[interactionName]++;
        }

        else { //doesn't have multiple interactions
            if (interactionName == "closet")//closet special case
            {
                if (interactedWith.Contains(interactionName))
                {
                    yield return GameObject.Find("Main Camera").GetComponent<CameraScript>().ShakeCamera();
                    yield return GameObject.Find("Main Camera").GetComponent<CameraScript>().ShakeCamera();
                    yield return StartCoroutine(dialogueBox.PlayDialogue(interactionName + "UGH"));
                    isInteracting = false;
                    yield break;
                }
                else
                {
                    yield return GameObject.Find("Main Camera").GetComponent<CameraScript>().ShakeCamera();
                } 
            }

            if (interactionName == "vainity" && dialogueProgressions[interactionName] >= 2) //vainity special case
            {
                yield return StartCoroutine(dialogueBox.PlayDialogue("empty1"));
                isInteracting = false;
                yield break;
            }
            

            if (!interactedWith.Contains(interactionName))//this takes care of adding more interactions
            {
                interactedWith.Add(interactionName);
            }
            yield return StartCoroutine(dialogueBox.PlayDialogue(interactionName));
        }

        //special additions
        if (interactionName == "phonograph" || interactionName == "speaker")
        {
            musicPlayer.Play();
        }

        if (interactionName == "package"){
            GameObject.Find("package").SetActive(false);
        }

        if (newSprite != null)
        {
            SpriteRenderer myRenderer = GetComponent<SpriteRenderer>();
            myRenderer.sprite = newSprite;
        }

        if (!OverallScript.is2020){
            if (interactedWith.Count >=13)
            {
            yield return new WaitForSeconds(1f);
            Debug.Log("DONE!!!!!");

            interactedWith = new List<string>();
            yield return StartCoroutine(dialogueBox.PlayDialogue("ending"));
            OverallScript bigScript = GameObject.Find("OverallScript").GetComponent<OverallScript>();
            StartCoroutine(bigScript.SwitchTo2020());
            } 
        }
        else {
            if (interactedWith.Count >=8)
            {
            yield return new WaitForSeconds(1f);
            Debug.Log("DONE!!!!!");

            yield return StartCoroutine(dialogueBox.PlayDialogue("ending2020"));
            OverallScript bigScript = GameObject.Find("OverallScript").GetComponent<OverallScript>();
            StartCoroutine(bigScript.EndGame());
            } 
        }
        
        isInteracting = false;
    }
}
