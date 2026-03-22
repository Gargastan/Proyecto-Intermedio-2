using UnityEngine;
using UnityEngine.UI;

public class BuildTimerUI : MonoBehaviour
{
    public BuildTimer buildTimer;
    public Image fillImage;

    [Header("Colors")]
    public Color greenColor = Color.green;
    public Color yellowColor = Color.yellow;
    public Color redColor = Color.red;

    void Update()
    {
        if (buildTimer == null || fillImage == null) return;

        float remaining = buildTimer.GetTimeRemaining();
        float fill = remaining / buildTimer.buildTime;

        fillImage.fillAmount = fill;

        if (fill > 0.4f)
            fillImage.color = greenColor;
        else if (fill > 0.2f)
            fillImage.color = yellowColor;
        else
            fillImage.color = redColor;
    }
}