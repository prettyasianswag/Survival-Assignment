using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GUISkin mainMenuSkin;
    public GUIStyle muteButton, playButton, optionsButton, exitButton, returnButton;
    public bool showOP;
    public float audioSlider;
    public float dirSlider;
    public float unmuteVolume;
    public AudioSource audi;
    public Light dirLight;

    void Start()
    {
        // Changing so that the cursor is visible
        Cursor.lockState = CursorLockMode.None;

        // Added the feature to mute the volume inside the game (used all NoCopyrightSounds music on youtube)
        audi = GameObject.Find("Audio Source").GetComponent <AudioSource>();
        dirLight = GameObject.Find("Directional Light").GetComponent<Light>();

        // Lighting and the Volume for the game
        if (PlayerPrefs.HasKey("Volume"))
        {
            dirLight.intensity = PlayerPrefs.GetFloat("Directional Light");
            audi.volume = PlayerPrefs.GetFloat("Volume");
            if (PlayerPrefs.GetInt("mute") == 0)
            {
                // Muting the volume of the player checks the mute button
                unmuteVolume = PlayerPrefs.GetFloat("Volume");
                audi.volume = 0;
            }
            else
            {
                //Unmuting the volume if the player checks the unmute button
                audioSlider = PlayerPrefs.GetFloat("Volume");
            }
        }
        // Sliders for the Options menu to change the value of the certain settings
        audioSlider = audi.volume;
        dirSlider = dirLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        // Changes the volume of game to match the volume that the user set
        if (audi.volume != audioSlider)
        {
            audi.volume = audioSlider;
        }

        // Changes the directional light of game to match the directional light that the user set
        if (dirLight.intensity != dirSlider)
        {
            dirLight.intensity = dirSlider;
        }
    }

    void OnGUI()
    {
        // Setting the screen resolution for the game
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        GUI.skin = mainMenuSkin;

        // Setting the values for the buttons inside the main menu
        if (!showOP)
        {
            GUI.Box(new Rect(12f * scrW, 5f * scrH, 4f * scrW, 1f * scrH), "WASD TO MOVE");
            GUI.Box(new Rect(12f * scrW, 6f * scrH, 4f * scrW, 1f * scrH), "SPACE TO JUMP");
            GUI.Box(new Rect(12f * scrW, 7f * scrH, 4f * scrW, 1f * scrH), "LEFT MOUSE CLICK TO FIRE");
            GUI.Box(new Rect(12f * scrW, 8f * scrH, 4f * scrW, 1f * scrH), "SURVIVE AS LONG AS YOU CAN!");

            if (GUI.Button(new Rect (0f * scrW, 6f * scrH, 2f * scrW, 1f * scrH), "", playButton))
            {
                SceneManager.LoadScene(1);
            }
            if (GUI.Button(new Rect (0f * scrW, 7f * scrH, 2f * scrW, 1f * scrH), "", optionsButton))
            {
                showOP = true;
            }
            if (GUI.Button(new Rect (0f * scrW, 8f * scrH, 2f * scrW, 1f * scrH), "", exitButton))
            {
                Application.Quit();
            }
        }

        else // else we are in the options menu
        {
       
            GUI.Label (new Rect (7.5f * scrW, 5f * scrH, 3 * scrW, 0.5f * scrH), "Volume!");
            audioSlider = GUI.HorizontalSlider(new Rect(5 * scrW, 5.5f * scrH, 6 * scrW, 1 * scrH), audioSlider, 0f, 1f);

            GUI.Label(new Rect(7.2f * scrW, 6.5f * scrH, 2 * scrW, 0.5f * scrH), "Brightness!");
            dirSlider = GUI.HorizontalSlider(new Rect(5 * scrW, 7f * scrH, 6 * scrW, 1f * scrH), dirSlider, 0f, 1f);

            if (GUI.Button (new Rect (6.5f * scrW, 8 * scrH, 3 * scrW, 1 * scrH), "", returnButton))
            {
                SaveOptions();
                showOP = false;
            }
            GUI.skin = mainMenuSkin;
        }
    }

    // Saves the player's settings
    void SaveOptions()
    {
        PlayerPrefs.SetFloat("Volume", audioSlider);
        PlayerPrefs.SetFloat("Directional Light", dirSlider);
   }
}
