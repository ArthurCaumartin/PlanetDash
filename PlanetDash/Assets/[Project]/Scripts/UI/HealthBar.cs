using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    public void UpdateFill(float ratio)
    {
        _fillImage.fillAmount = ratio;
    }
}