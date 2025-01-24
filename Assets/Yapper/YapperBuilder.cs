using System.Collections.Generic;
using UnityEngine;

public class YapperBuilder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private SkinnedMeshRenderer faceRenderer;
    [SerializeField] private SkinnedMeshRenderer tShirtRenderer;
    [SerializeField] private SkinnedMeshRenderer bodyRenderer;
    [SerializeField] private Transform headBone;
    [SerializeField] private Material highlights;

    public void BuildYapper(Material face, Material shirt, Material shoes, Material skin, GameObject hair, GameObject hat)
    {
        faceRenderer.SetMaterials(new List<Material>(){ face });
        tShirtRenderer.SetMaterials(new List<Material>(){ shirt });
        bodyRenderer.SetMaterials(new List<Material>() {shoes, highlights, skin});
        Instantiate(hair, transform.position, transform.rotation);
        Instantiate(hat, transform.position, transform.rotation);
    }
}
