using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private MeshFilter[] foodMeshes;
    [SerializeField] private MeshFilter mesh;

    [SerializeField] private Vector3 scale;
    [SerializeField] private int rotationSpeed;
   
    void Start()
    {
        mesh.sharedMesh = foodMeshes[Random.Range(0, foodMeshes.Length)].sharedMesh;
        transform.localScale = scale;
    }

    void Update()
    {
       transform.Rotate(0, rotationSpeed * Time.deltaTime,0); 
    }
}
