using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LaunchZing() {
        SceneManager.LoadScene("LaunchZing");
    }

    public void QuitApp() {
        Application.Quit();
    }
}
