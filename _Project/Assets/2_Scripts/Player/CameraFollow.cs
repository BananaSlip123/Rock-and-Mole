
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo a seguir")]
    public Transform target;

    [Header("Offset en ESPACIO DE MUNDO")]
    public Vector3 worldOffset = new Vector3(0f, 5f, -10f);
    public float distance = 6f;

    [Header("Suavizado")]
    [Range(0f, 1f)]
    public float smoothSpeed = 0.125f;

    [Header("Opcional")]
    public bool lookAtTarget = false;
    public bool detachFromParentOnStart = true; // desparentar para ignorar rotación del padre

    private void Start()
    {
        if (detachFromParentOnStart && transform.parent != null)
        {
            // Desparenta la cámara para que no herede rotaciones/escala del padre
            transform.SetParent(null, true);
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + worldOffset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }
}
