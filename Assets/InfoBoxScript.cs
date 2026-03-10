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
        infoBoxes["vainity1"] = "Advertisements played a role in influencing consumerism. Strategies in advertisement research included depictions of beauty standards and social norms, which intertwined consumption choices with identity, and strong branding, which encouraged customer loyalty and an attachment to companies.\n\n“[There was] Clare Kendry's having been seen at the dinner hour in a fashionable hotel in company with another woman and two men, all of them white. And dressed! And there was another which told of her driving in Lincoln Park with a man, unmistakably white, and evidently rich. Packard limousine, chauffeur in livery, and all that.”";
        infoBoxes["bed2020"] = "Subcultures based on aesthetics of visuals such as “cottagecore” and “grunge” have gained traction. They can become labels for ourselves, provide a sense of belonging, and help understand the person we aspire to be.\n\nHow does this connect to Art Deco?";
        infoBoxes["ringlight"] = "Being a social media influencer is a desired career. A 2022 poll found that 54% of young teens wanted to be influencers, more compared to any other career such as being a doctor or a movie star. A new kind of an American Dream, it provides an alternative route to the traditional path of getting an education and getting a traditional job.\n\nWhile economic mobility has been slowing down as inequality rises, 70% of Americans believe the poor can escape poverty through hard work. In Europe, that number is 30% despite generally having higher mobility rates.";
        infoBoxes["package"] = "Interestingly, there has been a recent trend of nostalgia for pre-COVID and pre-AI aesthetics. How does our attitude towards the past reflect our current selves, and how does this connect to Egyptomania and World War I/the Spanish flu?";
        infoBoxes["phone"] = "How we express ourselves through social media is constrained by features such as likes and metrics, which in turn can influence our self-expression and perception of identity.\n\nWhen does personality become a commodity?";
        infoBoxes["wallet"] = "Influencers with higher incomes tend to experience higher levels of fear and anger compared to those with lower incomes. This is mostly attributed to the high pressure of sustaining their presence, which can lead to a disconnection from purposeful passion to commercial viability.";
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
