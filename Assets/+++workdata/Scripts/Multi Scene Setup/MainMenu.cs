using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a quick and dirty implementation of the MainMenu.
public class MainMenu : MonoBehaviour
{
    //Wird aufgerufen, wenn der Start Button gedr�ckt wird.
    //Startet das Spiel und l�dt die Gameplay Szene.
    public void StartGame()
    {
        GameStateManager.StartGame();
    }

    public void QuitGame()
    {
        //There might be reason to move this to the GameStateManager, so saving & cleanup logic
        //can be added and collected at a single point.
        Application.Quit();
    }
}
