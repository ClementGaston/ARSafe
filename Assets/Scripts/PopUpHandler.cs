using UnityEngine;
using UnityEngine.UI;

public class PopUpHandler : MonoBehaviour
{
    public void HidePopUp()
    {
        gameObject.SetActive(false);
        gameObject.transform.GetChild(1).GetComponent<Image>().sprite = null;
        gameObject.transform.GetChild(1).GetComponent<Animator>().runtimeAnimatorController = null;
    }
}
