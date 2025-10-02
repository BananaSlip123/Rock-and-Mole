using UnityEngine;

public class DashComponent : MonoBehaviour, ISkillComponent, IMoveComponent
{
    const float COOLDOWN = 0.2f;
    const float DASH_TIME = 0.1f;

    float timeCooldown = 0f;
    float timeDashing = 0f;

    bool IsInCooldown = false;

    public bool isDashing = false;

    // Update is called once per frame
    void FixedUpdate()
    {       
        if (timeCooldown < COOLDOWN)
        {
            timeCooldown += Time.fixedDeltaTime;
        }

        if(timeDashing < DASH_TIME)
        {
            timeDashing += Time.fixedDeltaTime;
        }
    }

    public void DoSpecialSkill()
    {
        isDashing = true;
    }

    public void Move()
    {
        
    }

    private Vector3 VectorConeverter(Vector3 vectorToChange)
    {
        //Quaternion rotation = Quaternion.Euler(0, Vector3.Angle(Vector3.forward, calculoMovimiento.transform.forward), 0);
        Quaternion rotation = Quaternion.Euler(0, 45f, 0);
        Matrix4x4 matrix = Matrix4x4.Rotate(rotation);
        Vector3 vectorConverted = matrix.MultiplyPoint3x4(vectorToChange);
        return vectorConverted;
    }
}
