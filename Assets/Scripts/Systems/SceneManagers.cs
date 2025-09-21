using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

public class SceneManagers : MonoBehaviour
{
    public void load(string sceneName)
    {
        if ( !string.IsNullOrEmpty(sceneName) )
            StartCoroutine(loadAsync(sceneName));
        
    }

    private IEnumerator loadAsync(string sceneName)
    {
        // LoadingScreen.Show();

        if (Inventory.Instance != null)
            Inventory.Instance.save();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            // LoadingScreen.UpdateProgress(asyncLoad.progress);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        asyncLoad.allowSceneActivation = true;

        // LoadingScreen.Hide();
    }

}
