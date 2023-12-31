using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//this is a dummy-implementation of the game loop.
//in this implementation, pressing any button sends the player back to the main menu.
//Any actual gameplay code would go here.
public class GameplayLoop : MonoBehaviour
{
    public static GameplayLoop Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
    }

    #region Update Kommentare
    //Wenn die Taste Escape gedrückt wird, wird die Methode ReturnToMainMenu aufgerufen um zum Hauptmenü zurückzukehren
    //Wenn die Taste R gedrückt wird, wird die Methode ReloadGamePlay aufgerufen um die Szene neu zu laden
    #endregion
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ReturnToMainMenu();
        if (Input.GetKeyDown(KeyCode.R))
            ReloadGamePlay();
    }

    public void ReturnToMainMenu()
    {
        GameStateManager.GoToMainMenu();
    }

    public void ReloadGamePlay()
    {
        SceneLoader.ReloadGame();
    }
}
