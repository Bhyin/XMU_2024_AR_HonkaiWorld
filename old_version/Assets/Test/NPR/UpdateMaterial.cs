using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMaterial : MonoBehaviour
{
    [SerializeField] Transform headBone;
    [SerializeField] Vector3 headDirection = Vector3.up;
    [SerializeField] List<Material> faceMaterials;

    // Update is called once per frame
    void Update()
    {
        if(faceMaterials == null || headBone == null) return;
        Vector3 direction = headBone.rotation * headDirection;

        foreach(var material in faceMaterials)
        {
            material.SetVector("_FaceDirection", direction);
        }
        
    }
}
