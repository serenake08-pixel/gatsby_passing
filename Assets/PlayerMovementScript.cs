using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    private Animator animator;
    private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 m;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = m*speed;

        //the empty interaction start only works here blublublublublub
        bool keyDown = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;




        if (keyDown && !InteractionsScript.isInteracting && !InteractionsScript.anyTouching)
        {
            StartCoroutine(EmptyInteraction());
        }
    }

    public IEnumerator EmptyInteraction()
    {
        InteractionsScript.isInteracting = true;
        DialougeBoxScript dialogueBox = GameObject.Find("DialougeBox").GetComponent<DialougeBoxScript>();
        yield return StartCoroutine(dialogueBox.PlayDialogue("empty" + Random.Range(1,4).ToString()));
        InteractionsScript.isInteracting = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (InteractionsScript.isInteracting){return;}
        animator.SetBool("IsWalking", true);
        if (context.canceled)
        {
            animator.SetBool("IsWalking", false);
            animator.SetFloat("LastInputX", m.x);
            animator.SetFloat("LastInputY", m.y);
        }
        m = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", m.x);
        animator.SetFloat("InputY", m.y);
    }
}

