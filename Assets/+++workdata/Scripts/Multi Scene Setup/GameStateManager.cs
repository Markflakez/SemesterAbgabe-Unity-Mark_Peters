using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStateManager : MonoBehaviour
{
    private static GameStateManager Instance;

    public GameObject gameUI;
    public GameObject itemLauncher;
    public GameObject particleSystems;

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
    //Wenn ja, wird das Game UI und der itemLauncher in der Szene aktiviert
    //Wenn nein, wird das Game UI und der itemLauncher in der Szene deaktiviert
    #endregion
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == (int)SceneLoader.DefaultScenes.MainMenu)
        {
            if (!gameUI || !itemLauncher || !particleSystems)
                return;
            DisableSceneElements();
        }
        else
        {
            if (!gameUI || !itemLauncher || ! particleSystems)
                return;
            EnableSceneElements();
        }
    }

    #region Kommentare DisableSceneGameObjects und EnableSceneGameObjects
    //Deaktiviert/aktiviert alle wichtigen GameObjects der Szene
    #endregion
    private void DisableSceneElements()
    {
        gameUI.SetActive(false);
        itemLauncher.SetActive(false);
        particleSystems.SetActive(false);
    }
    private void EnableSceneElements()
    {
        gameUI.SetActive(true);
        itemLauncher.SetActive(true);
        particleSystems.SetActive(true);
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
