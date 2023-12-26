using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//A very quick and dirty implementation of a GameStateManager.
//The job the GameStateManager is to mediate between different GameStates.
//At the moment it can only switch between MainMenu and the GameLoop.
//Additional responsibilities would be:
// - keep actual track of the GameState
// - pause the GameLoop without stopping it
// - Quitting the application
//and possibly more.
public class GameStateManager : MonoBehaviour
{
    private static GameStateManager Instance;

    public GameObject inventoryManager;

    private void Awake()
    {
        Instance = this;
    }


    #region Kommentare
    //Wenn eine Szene geladen wird, wird die Methode OnSceneLoaded aufgerufen
    #endregion
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    #region Kommentare
    //Wenn eine Szene geladen wird, wird überprüft, ob es sich um die Spielszene handelt
    //Wenn ja, wird das Inventar aktiviert
    //Wenn nein, wird das Inventar deaktiviert
    #endregion
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (inventoryManager != null)
        {
            if (scene.buildIndex == (int)SceneLoader.DefaultScenes.GamePlay)
            {
                inventoryManager.SetActive(true);
            }
            else
            {
                inventoryManager.SetActive(false);
            }
        }
    }

    public static void StartGame()
    {
        //the hardcoded "3" should be replaced by an actual scene.
        Instance.StartCoroutine(Instance.LoadScenesCoroutine((int)SceneLoader.DefaultScenes.MainMenu, 3));
    }

    public static void GoToMainMenu()
    {
        //the hardcoded "3" should be replaced by an actual scene.
        Instance.StartCoroutine(Instance.LoadScenesCoroutine(3, (int)SceneLoader.DefaultScenes.MainMenu));
    }
    
    private IEnumerator LoadScenesCoroutine(int oldScene, int newScene)
    {
        LoadingScreen.Show(this);
        yield return SceneLoader.Instance.UnloadSceneViaIndex(oldScene);
        yield return SceneLoader.Instance.LoadSceneViaIndex(newScene);
        LoadingScreen.Hide(this);
    }
}
