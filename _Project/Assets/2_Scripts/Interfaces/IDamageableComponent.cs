using UnityEngine;

public interface IDamageableComponent
{
    void RecieveDamage(int damage);
    void ResetHasBeenDamaged();
    bool GetHasBeenDamaged();
}
