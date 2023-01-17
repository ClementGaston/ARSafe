using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image progressBar;

    public void LoadLevel(int sceneId)
    {
        StartCoroutine(AsyncLoad(sceneId));
    }

    IEnumerator AsyncLoad(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressBar.fillAmount = progress;

            yield return null;
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
