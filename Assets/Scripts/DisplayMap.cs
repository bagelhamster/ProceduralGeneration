using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMap : MonoBehaviour
{
    public Renderer renderTexture;
    public MeshFilter filter;
    public MeshRenderer meshRenderer;
    public void DrawTexture(Texture2D texture)
    {
     
        renderTexture.sharedMaterial.mainTexture = texture;
        renderTexture.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
    public void DrawMesh(MeshData meshData,Texture2D texture)
    {
        filter.sharedMesh=meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
