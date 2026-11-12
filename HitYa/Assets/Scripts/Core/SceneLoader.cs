using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles asynchronous scene loading.
/// Singleton that persists across scenes. Maintains navigation history stack.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [Header("Optional Boot Behavior")]
    [SerializeField] bool loadOnStart = false;
    [SerializeField] int startSceneIndex = 1;

    int targetSceneIndex;
    private bool isLoading = false;
    private readonly Stack<int> history = new();
    public int PreviousSceneIndex => history.Count > 0 ? history.Peek() : -1;

    // Keep scene activation paused until the caller finishes pre-fetching data.
    public static bool HoldActivation = false;

    // True on extra copies (e.g. a SceneLoader on a button in another scene).
    // They don't load anything themselves; they forward calls to the Boot instance.
    bool isProxy = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else isProxy = true;
    }

    void Start()
    {
        if (isProxy) return;
        if (loadOnStart) LoadScene(startSceneIndex);
    }

    public void LoadScene(int sceneIndex)
    {
        if (isProxy) { Instance.LoadScene(sceneIndex); return; }
        if (isLoading) return;

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        history.Push(currentScene);

        targetSceneIndex = sceneIndex;
        isLoading = true;
        StartCoroutine(AsyncLoadRoutine());
    }

    public void GoBack()
    {
        if (isProxy) { Instance.GoBack(); return; }
        if (isLoading || history.Count == 0) return;
        targetSceneIndex = history.Pop();
        isLoading = true;
        StartCoroutine(AsyncLoadRoutine());
    }

    IEnumerator AsyncLoadRoutine()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(targetSceneIndex);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f) yield return null;

        // Wait while caller is pre-fetching data (e.g. after restart)
        while (HoldActivation) yield return null;

        async.allowSceneActivation = true;
        while (!async.isDone) yield return null;
        isLoading = false;
    }
}
