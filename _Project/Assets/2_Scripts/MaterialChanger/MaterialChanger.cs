using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] Renderer[] meshRenderers;
    [SerializeField] float temporalMaterialDuration;
    List<Material[]> initialMaterials;
    public void AssignTemporalMaterial()
    {
        StartCoroutine(ChangeColor());
    }

    #region PRIVATE FUNCS
    void Awake()
    {
        initialMaterials = new List<Material[]>(meshRenderers.Length);

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            initialMaterials.Add(meshRenderers[i].materials);
        }
    }

    
    void AssignMatToRenderers(List<Material[]> mats)
    {
        for (int i = 0; i < meshRenderers.Length; i++) meshRenderers[i].materials = mats[i];
    }

    void AssignMatToRenderers(Material[] mat)
    {
        for (int i = 0; i < meshRenderers.Length; i++) meshRenderers[i].materials = mat;
    }
    void AssignMatToRenderers(Material mat)
    {
        Material[] matToArray = new Material[1];
        matToArray[0] = mat;
        AssignMatToRenderers(matToArray);
    }

    IEnumerator ChangeColor()
    {
        //Material material = new Material(Shader.Find("Unlit/Color"));
        Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        material.color = new Color(1,0,0);
        AssignMatToRenderers(material);

        //yield return new WaitForSeconds(temporalMaterialDuration);
        //material = new Material(Shader.Find("Unlit/Color"));
        //material.color = new Color(1, 0.3f, 0.3f);
        //AssignMatToRenderers(material);

        yield return new WaitForSeconds(0.06f);
        AssignMatToRenderers(initialMaterials);
    }
    #endregion
}
