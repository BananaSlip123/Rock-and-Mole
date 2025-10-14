using UnityEngine;
using UnityEngine.UI;
public class LifeBarUI : MonoBehaviour
{
    [SerializeField] DamageableComponent damageableComponent;
    [SerializeField] RectTransform rectT_lifeBarSlider;
    [SerializeField] Image img_lifeBarSlider;
    int MaxLife = 0;
    private void OnEnable()
    {
        MaxLife = damageableComponent.Health;
        damageableComponent.OnHealthChange += OnHealthChange;
    }
    private void OnDisable()=> damageableComponent.OnHealthChange -= OnHealthChange;
    void OnHealthChange(int value)
    {
        //Right es 100 cuando value es 0, y 0 cuando value es 100
        float percent = 100.0f * value / MaxLife;//division decimal, no entera
        float normalicedVal = percent / 100;
        img_lifeBarSlider.fillAmount = normalicedVal;
    }
}
