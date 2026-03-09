using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScreenScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("AboutText") != null)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                GameObject.Find("AboutText").SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("1920s scene");
    }
}