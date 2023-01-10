using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void LaunchZing() {
        SceneManager.LoadScene("LaunchZing");
    }

    public void InfoZing() {
        SceneManager.LoadScene("InfoZing");
    }

    public void QuitApp() {
        Application.Quit();
    }
}
