using UnityEngine;
using UnityEngine.Events;

public class OnClickHandler : MonoBehaviour
{
    public UnityEvent OnClick;

    private void OnMouseUpAsButton() {
        OnClick.Invoke();
    }
}
