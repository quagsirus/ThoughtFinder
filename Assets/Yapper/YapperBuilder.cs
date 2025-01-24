using System.Collections.Generic;
using UnityEngine;

namespace Yapper
{
    public class YapperBuilder : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private SkinnedMeshRenderer faceRenderer;
        [SerializeField] private SkinnedMeshRenderer tShirtRenderer;
        [SerializeField] private SkinnedMeshRenderer bodyRenderer;
        [SerializeField] private Transform headBone;
        [SerializeField] private Material highlights;

        public void BuildYapper(Material face, Material hairColour, Material shirt, Material shoes, Material skin, GameObject hair, GameObject hat)
        {
            faceRenderer.SetMaterials(new List<Material> { face });
            tShirtRenderer.SetMaterials(new List<Material> { shirt });
            bodyRenderer.SetMaterials(new List<Material> {shoes, highlights, skin});
            var hairInstance = Instantiate(hair, transform.position, transform.rotation);
            hairInstance.transform.parent = headBone;
            hairInstance.GetComponentInChildren<MeshRenderer>().SetMaterials(new List<Material> { hairColour });
            var hatInstance = Instantiate(hat, transform.position, transform.rotation);
            hatInstance.transform.parent = headBone;
        }
    }
}
