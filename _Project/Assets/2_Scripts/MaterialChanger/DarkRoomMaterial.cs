using UnityEngine;

public class DarkRoomMaterial : MonoBehaviour
{

    [Range(0f, 1f)]
    public float darkenFactor = 0.6f;
    public string ambientPropertyName = "_Ambient";

    bool init = false;

    void Start()
    {
        ApplyToAll();
    }

    private void LateUpdate()
    {
        
    }

    [ContextMenu("Darken Ambient (Per Renderer)")]
    public void ApplyToAll()
    {
        int ambientID = Shader.PropertyToID(ambientPropertyName);
        Renderer[] renderers = FindObjectsByType<Renderer>(FindObjectsSortMode.None);
        int changed = 0;
        int i = 0;
        foreach (Renderer r in renderers)
        {
            i = 0;
            // Saltar renderers sin materiales válidos
            Material[] mats = r.sharedMaterials;

            // Leer color desde el primer material que tenga la propiedad
            Color src = Color.white;
            foreach (Material mat in mats)
            {
                if (mat != null && mat.HasProperty(ambientID))
                {
                    src = mat.GetColor(ambientID);

                    Debug.Log("AMBIENT: " + src);

                    Color darker = new Color(src.r * darkenFactor, src.g * darkenFactor, src.b * darkenFactor, src.a);

                    MaterialPropertyBlock mpb = new MaterialPropertyBlock();
                    r.GetPropertyBlock(mpb, i);
                    mpb.SetColor(ambientID, darker);
                    r.SetPropertyBlock(mpb);

                    changed++;
                    i++;
                }
            }          
        }

        Debug.Log($"Ambient oscurecido en {changed} renderers (MPB).");
    }

}
