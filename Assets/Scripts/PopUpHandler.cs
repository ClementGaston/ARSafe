using UnityEngine;

public class PopUpHandler : MonoBehaviour
{
    public void ShowPopUp()
    {
        gameObject.SetActive(true);
    }

    public void HidePopUp()
    {
        gameObject.SetActive(false);
    }
}
