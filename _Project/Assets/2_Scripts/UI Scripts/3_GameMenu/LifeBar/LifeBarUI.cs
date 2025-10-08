using UnityEngine;

public class LifeBarUI : MonoBehaviour
{
    [SerializeField] DamageableComponent damageableComponent;
    [SerializeField] RectTransform rectT_lifeBarSlider;
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
        int right = 100 - (int)percent;
        rectT_lifeBarSlider.offsetMax = new Vector2(-right,0); // Right y Top
    }
}
