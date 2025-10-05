using UnityEngine;

public class VectorConverter : MonoBehaviour
{
    /// <summary>
    /// Transforma una dirección en coordenadas isométricas
    /// </summary>
    /// <param name="vectorToChange">Vector a cambiar de coordenadas</param>
    /// <returns>El vector convertido en coordenadas isométricas</returns>
    public static Vector3 VectorConeverter(Vector3 vectorToChange)
    {
        //Quaternion rotation = Quaternion.Euler(0, Vector3.Angle(Vector3.forward, calculoMovimiento.transform.forward), 0);
        Quaternion rotation = Quaternion.Euler(0, 45f, 0);
        Matrix4x4 matrix = Matrix4x4.Rotate(rotation);
        Vector3 vectorConverted = matrix.MultiplyPoint3x4(vectorToChange);
        return vectorConverted;
    }

    public static Vector3 SetVectorToIsoCoords(Vector3 vector, float speed)
    {
        vector = VectorConeverter(vector);

        vector = speed * Time.fixedDeltaTime * vector;

        return vector;
    }
}

