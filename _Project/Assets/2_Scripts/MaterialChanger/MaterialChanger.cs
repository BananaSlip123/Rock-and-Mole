using UnityEngine;
using System.Collections;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] Renderer meshRenderer;
    [SerializeField] float temporalMaterialDuration;
    Material[] initialMaterials;

    void Awake()
    {
        initialMaterials = meshRenderer.materials;
    }

    public void AssignTemporalMaterial(Material temporalMat)
    {
        StartCoroutine(ChangeColor(temporalMat));
    }
    void AssignMatToRenderer(Material[] mats) => meshRenderer.materials = mats; 
    void AssignMatToRenderer(Material mat)
    {
        Material[] matToArray = new Material[1];
        matToArray[0] = mat;
        AssignMatToRenderer(matToArray);
    }
        
    IEnumerator ChangeColor(Material temporalMat)
    {
        AssignMatToRenderer(temporalMat);
        yield return new WaitForSeconds(temporalMaterialDuration);
        AssignMatToRenderer(initialMaterials);
    }

}
