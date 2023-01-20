using UnityEngine;

public class Popup : MonoBehaviour
{
    public void Show(GameObject content)
    {
        gameObject.SetActive(true);
        content.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        foreach (Transform child in gameObject.transform.GetChild(1).transform) 
        {
            child.gameObject.SetActive(false);
        }
    }
}
