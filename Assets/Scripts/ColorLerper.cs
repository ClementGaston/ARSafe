using UnityEngine;

public class ColorLerper : MonoBehaviour
{
    private float speed = 1f;
    private Color startColor = Color.yellow;
    private Color endColor = Color.black;
    private bool repeatable = true;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (!repeatable)
        {
            float t = (Time.time - startTime) * speed;
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }
        else
        {
            float t = Mathf.Sin((Time.time - startTime) * speed);
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }
    }
}
