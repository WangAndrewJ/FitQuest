using UnityEngine;
using TMPro;

public class Fade : MonoBehaviour
{
    public TextMeshProUGUI text;
    private float a = 1f;
    private float start;

    private void Start()
    {
        InvokeRepeating("FadeAlpha", 0f, 0.01f);
    }

    private void FadeAlpha()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, a);
        a -= 0.01f;

        if (a <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
