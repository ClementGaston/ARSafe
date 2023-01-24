using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    private bool isOn = false;
    public RectTransform toggleIndicator;
    private float offsetX = 125;
    
    public GameObject loadingScreen;
    public Image progressBar;

    public void Toggle() 
    {
        Debug.Log(!isOn);
        isOn = !isOn;

        if(isOn) {
            toggleIndicator.anchoredPosition = new Vector2(toggleIndicator.anchoredPosition.x + offsetX, toggleIndicator.anchoredPosition.y);
        }
        else {
            toggleIndicator.anchoredPosition = new Vector2(toggleIndicator.anchoredPosition.x - offsetX, toggleIndicator.anchoredPosition.y);
        }
    }

    public void LoadLevel(int sceneId)
    {
        if(isOn && sceneId > 1) {
            sceneId += 2;
        }
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
