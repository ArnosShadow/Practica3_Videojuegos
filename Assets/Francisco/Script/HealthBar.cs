using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Color endValue = Color.green;
    [SerializeField] private Color startValue = Color.red;


    [SerializeField] private Image fill;


    public void SetSalud(float salud) {
        slider.value = salud;
        float normalizedValue = (slider.value - slider.minValue) / (slider.maxValue - slider.minValue);
        fill.color = Color.Lerp(startValue, endValue, normalizedValue);

    }
}
