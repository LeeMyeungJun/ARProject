using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class UpdateNavMesh : MonoBehaviour
{
    [SerializeField] NavMeshSurface meshSurface;
    [SerializeField] NavMeshData meshData;
    public void UpdateNav()
    {
        meshSurface.UpdateNavMesh(meshData);
    }
}
