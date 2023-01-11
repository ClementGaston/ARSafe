using UnityEngine;

public class BasicMenuHandler : MonoBehaviour
{
    private void Awake() {
       this.gameObject.SetActive(false); 
    }

    public void OpenMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        this.gameObject.SetActive(false);
    }
}
