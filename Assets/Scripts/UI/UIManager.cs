using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _main;
    [SerializeField] private GameObject _settings;

    [Header("Main")]
    [SerializeField] private Button _continue;
    [SerializeField] private Button _start;
    [SerializeField] private Button _setting;
    [SerializeField] private Button _quit;

    [Header("Settings")]
    [SerializeField] private Button save;

    // zavtra


    private SceneManagers sceneManager;

    void Start()
    {
        sceneManager = GetComponent<SceneManagers>();
        if (sceneManager == null)
            sceneManager = gameObject.AddComponent<SceneManagers>();

        //_continue.onClick.AddListener
        _start.onClick.AddListener(onClickStart);

        _quit.onClick.AddListener(onClickQuit);
    }

    void onClickStart() => sceneManager.load("SampleScene");

    void onClickQuit() => Application.Quit();

}
