using UnityEngine;

public class InitiliazeProcedure : MonoBehaviour
{
    public GameObject laserCutterPlaceholder;

    public void HideLaserCutter()
    {
        laserCutterPlaceholder.SetActive(false);
    }
}
