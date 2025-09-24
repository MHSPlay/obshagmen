using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    [SerializeField] private string firstLevel;

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("Game is exiting");
    }

    public void ShowSettings(bool state)
    {
        mainMenu.SetActive(!state);
        settingsMenu.SetActive(state);
    }
}
