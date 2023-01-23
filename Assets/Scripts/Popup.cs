using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject PrevBtn;
    public GameObject CloseBtn;

    public GameObject defaultMenu;
    private GameObject content;

    public void Show(GameObject content)
    {
        if(gameObject.activeSelf)
        {
            return;
        }
        
        gameObject.SetActive(true);
        content.SetActive(true);
    }

    public void SubShow(GameObject content)
    {
        defaultMenu.SetActive(false);
        content.SetActive(true);
        PrevBtn.SetActive(true);
        CloseBtn.SetActive(false);
        this.content = content;
    }

    public void Back(GameObject content)
    {
        defaultMenu.SetActive(true);
        CloseBtn.SetActive(true);
        PrevBtn.SetActive(false);
        this.content.SetActive(false);
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
