using UnityEngine;
using UnityEngine.UI;

public class FullscreenImage : MonoBehaviour
{
    public Image image;
    public GameObject fullscreenImage;

    public void ShowFullscreenImage()
    {
        Debug.Log("Clicked");
        fullscreenImage.SetActive(true);
        fullscreenImage.GetComponent<Image>().sprite = image.sprite;
    }

    public void HideFullscreenImage()
    {
        fullscreenImage.SetActive(false);
    }
}
