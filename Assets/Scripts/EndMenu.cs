using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public GUISkin menuSkin;
    public GUIStyle mainMenuButton, exitButton;

    void Start()
    {
        // Making the cursor visible
        Cursor.lockState = CursorLockMode.None;
    }

    void OnGUI()
    {
        // Setting the resolution for the game
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        GUI.skin = menuSkin;

        // Setting the values for the buttons inside the end menu
        {
            // If player presses button, it will load main menu
            if (GUI.Button(new Rect(6.1f * scrW, 6f * scrH, 4f * scrW, 1f * scrH), "", mainMenuButton))
            {
                SceneManager.LoadScene(0);
            }

            // If player presses button, it will quit the game
            if (GUI.Button(new Rect(7.1f * scrW, 7f * scrH, 2f * scrW, 1f * scrH), "", exitButton))
            {
                Application.Quit();
            }
        }
    }
}
