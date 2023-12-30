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
