using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

/// <summary>
/// The SceneLoader is used to load scenes, asynchronous.
/// It can be used by using the singleton reference:
/// SceneLoader.Instance.LoadSceneViaIndex(0);
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public enum DefaultScenes
    {
        Startup = 0,
        Manager = 1,
        MainMenu = 2,
        GamePlay = 3,
    }

    private static SceneLoader _instance;

    //this is a "lazy singleton", meaning that the singleton creates itself the moment
    //it is referenced for the first time.
    public static SceneLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("SceneLoader").AddComponent<SceneLoader>();
                //Don't destroy on load means, that this gameObject is added to a special scene that will be always loaded.
                //This way, the SceneLoader will not be lost, if the originally containing scene is unloaded.
                DontDestroyOnLoad(_instance); 
            }

            return _instance;
        }
    }

    //this is an example of how the scene loader could be written to skip the need for an .Instance-call.
    //It would be the cleaner implementation for the following reasons:
    // - Improved readability through shorter code
    // - Easier to understand how scripts are to use by other devs
    // - Singleton-System can be refactored to different system more easily

    public static Coroutine LoadScene(DefaultScenes scene, Action onLoadingFinished = null)
    {
        return Instance.StartCoroutine(Instance.LoadSceneViaIndexCoroutine((int)scene, onLoadingFinished));
    }

    public Coroutine LoadSceneViaIndex(DefaultScenes scene, Action onLoadingFinished = null)
    {
        return LoadSceneViaIndex((int)scene, onLoadingFinished);
    }

    public Coroutine LoadSceneViaIndex(int index, Action onLoadingFinished = null)
    {
        //we return the coroutine, so that we can wait until the loading is done within other coroutines
        return StartCoroutine(LoadSceneViaIndexCoroutine(index, onLoadingFinished));
    }

    //this is the actual point where we load the scene and yield for the finishing of it.
    private IEnumerator LoadSceneViaIndexCoroutine(int index, Action onLoadingFinished)
    {
        var scene = SceneManager.GetSceneByBuildIndex(index);
        if (scene.isLoaded)
        {
            onLoadingFinished?.Invoke();
            yield break;
        }
        
        yield return SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        onLoadingFinished?.Invoke();
    }
    
    public Coroutine UnloadSceneViaIndex(int index, Action onLoadingFinished = null)
    {
        return StartCoroutine(UnloadSceneViaIndexCoroutine(index, onLoadingFinished));
    }
    
    private IEnumerator UnloadSceneViaIndexCoroutine(int index, Action onLoadingFinished)
    {
        var scene = SceneManager.GetSceneByBuildIndex(index);
        if (!scene.isLoaded)
        {
            onLoadingFinished?.Invoke();
            yield break;
        }
        
        yield return SceneManager.UnloadSceneAsync(index);
        onLoadingFinished?.Invoke();
    }


    //reload the manager scene, which will reload all other scenes.
    public static void ReloadGame()
    {
        Instance.StartCoroutine(Instance.ReloadGameCoroutine());
    }

    private IEnumerator ReloadGameCoroutine()
    {
        yield return UnloadSceneViaIndexCoroutine((int)DefaultScenes.Manager, null);
        yield return LoadSceneViaIndexCoroutine((int)DefaultScenes.Manager, null);
    }


#if UNITY_EDITOR
    //this is a great way to add simple functionality to your editor, by adding a menu item.
    //in this case, clicking the menu item will load the startup scene.

    [MenuItem("+++Load Scene+++/Startup Scene", false)]
    private static void LoadStartup()
    {
        LoadScene(DefaultScenes.Startup);
    }


    [MenuItem("+++Load Scene+++/GamePlay Scene", false)]
    private static void LoadGamePlay()
    {
       LoadScene(DefaultScenes.GamePlay);
    }


    [MenuItem("+++Load Scene+++/MainMenu Scene", false)]
    private static void LoadMainMenu()
    {
        LoadScene(DefaultScenes.MainMenu);
    }

    [MenuItem("+++Load Scene+++/Manger", false)]
    private static void LoadManager()
    {
        LoadScene(DefaultScenes.Manager);
    }


    #region LoadScene Kommentare
    //L�dt die mitgegebene Szene und �ffnet sie im Editor als einzige Szene.
    #endregion
    private static void LoadScene(DefaultScenes scene)
    {
        var scenePath = EditorBuildSettings.scenes[(int)scene].path;
        EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
    }
#endif

}
