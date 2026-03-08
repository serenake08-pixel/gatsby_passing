using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class InfoBoxScript : MonoBehaviour
{
    private UnityEngine.UI.Image imageComponent;
    public TextMeshProUGUI textComponent;
    bool keyDown = false;
    [SerializeField] Sprite clearBox;
    [SerializeField] Sprite infoBox;
    public Dictionary<string, string> infoBoxes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = "";

        imageComponent = GetComponent<UnityEngine.UI.Image>();
        imageComponent.sprite = clearBox;
        infoBoxes = new Dictionary<string, string>();

        infoBoxes["books1"] = "Since many people own books as an extension of the self and their values, they can also be seen as symbols of civilization and socio-economic status. This was particularly so in the 1920s, as a new demographic of business-class people dubbed “book Babbitts” began to buy more books. These books mainly were part of a get-cultured-quick scheme to match the owner’s wealth ().\n\n“Absolutely real—have pages and everything. I thought they’d be a nice durable cardboard. Matter of fact, they’re absolutely real. Pages and—Here! Lemme show you.” - Owl Eyes.";
        infoBoxes["bed"] = "Art Deco is an architectural style that was just called modern at the time. After the Spanish Flu and World War I, Art Deco was a rejection of the past and looked forwards to the future. \n\n Art Deco typically features stylized geometric forms, bold and luxurious materials, and symmetry.";
        infoBoxes["lamp"] = "While Art Deco in many ways was a rejection of the past, at the same time, it drew inspiration from cultures such as Greece/Rome and Egypt. \n\n The discovery of King Tut’s tomb in 1922 sparked a period of Egyptomania, a Western obsession of Egyptian culture, which actually became the basis of Art Deco.\n\nHow do we define ourselves based on our past?";
        infoBoxes["vainity"] = "Advertisements played a role in influencing consumerism. Strategies in advertisement research included depictions of beauty standards and social norms, which intertwined consumption choices with identity, and strong branding, which encouraged customer loyalty and an attachment to companies.\n\n“[There was] Clare Kendry's having been seen at the dinner hour in a fashionable hotel in company with another woman and two men, all of them white. And dressed! And there was another which told of her driving in Lincoln Park with a man, unmistakably white, and evidently rich. Packard limousine, chauffeur in livery, and all that.”";
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            keyDown = true;
        }
        else {
            keyDown = false;
        }
    }

    public IEnumerator ActivateInfoBox(string dialougeName)
    {
        imageComponent.sprite = infoBox;
        textComponent.text = infoBoxes[dialougeName];
        yield return new WaitForSeconds(2.0f); 
        Debug.Log(keyDown);
        yield return new WaitUntil(() => keyDown);
        imageComponent.sprite = clearBox;
        textComponent.text = "";
    }
}
