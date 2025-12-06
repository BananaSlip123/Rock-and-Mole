using UnityEngine;

public interface IDamageableComponent
{
    void RecieveDamage(int damage, float duration, float magnitude);
    void ResetHasBeenDamaged();
    bool GetHasBeenDamaged();
}
