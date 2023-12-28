using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStateManager : MonoBehaviour
{
    private static GameStateManager Instance;

    public GameObject inventoryManager;
    public GameObject healthSystem;
    public GameObject instructions;
    public GameObject buttons;
    public GameObject itemLauncher;

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
    //Wenn eine Szene geladen wird, wird überprüft, ob es sich um die Szene handelt
    //Wenn ja, werden alle genannten Elemente der Szene aktiviert
    //Wenn nein, werden alle genannten Elemente der Szene deaktiviert
    #endregion
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (inventoryManager == null)
            return;

        if (scene.buildIndex == (int)SceneLoader.DefaultScenes.GamePlay)
        {
            EnableSceneElements();
        }
        else
        {
            DisableSceneElements();
        }
    }

    #region Kommentare DisableSceneElements und EnableSceneElements
    //Deaktiviert/aktiviert alle Elemente der Szene
    #endregion
    private void DisableSceneElements()
    {
        inventoryManager.SetActive(false);
        healthSystem.SetActive(false);
        instructions.SetActive(false);
        buttons.SetActive(false);
        itemLauncher.SetActive(false);
    }
    private void EnableSceneElements()
    {
        inventoryManager.SetActive(true);
        healthSystem.SetActive(true);
        instructions.SetActive(true);
        buttons.SetActive(true);
        itemLauncher.SetActive(true);
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
