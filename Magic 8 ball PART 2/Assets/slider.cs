using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderPercentLabel : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI label;

    void Update()
    {
        if (slider != null && label != null)
        {
            int pct = Mathf.RoundToInt(slider.value * 100f);
            label.text = pct + "%";
        }
    }
}

