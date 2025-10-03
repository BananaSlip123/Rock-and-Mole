using UnityEngine;
public class SlotUI : MonoBehaviour
{
    //A inventory has many slots, there is a slot prefab
    #region SERIALIZABLE
    [SerializeField] Sprite sprite_material;
   // [SerializeField] TextMeshProUGUI sprxite_material;

    #endregion
    #region PRIVATE VARS
    #endregion
    #region PUBLIC VARS
    #endregion
    #region PRIVATE FUNCS
    #endregion
    #region PUBLIC FUNCS
    public void Move(float X, float Y)
    {
        transform.position = new Vector3(X,Y,transform.position.z);
    }
    #endregion
}
