using UnityEngine;
using UnityEngine.Events;

public class OnClickHandler : MonoBehaviour
{
    public UnityEvent OnClick = new UnityEvent();

    private void OnMouseUpAsButton()
    {
        OnClick.Invoke();
    }

    public void SetOnClick(UnityAction action)
    {
        OnClick.RemoveAllListeners();
        OnClick.AddListener(action);
    }
}
