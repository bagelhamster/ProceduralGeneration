using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mesh;

public class MapGeneration : MonoBehaviour
{
    public enum DrawMode {NoiseMap,ColourMap,Mesh,FalloffMap};
    public DrawMode drawMode;
    public const int mapChunkSize = 241;
    [Range(0,6)]
    public int levelOfDetail;
    public TextureData textureData;
    public Material terrainMaterial;

    float[,]falloffMap;
    public TerrainData terrainData;
    public NoiseData noiseData;
    public float lastNoiseHeight;
    public bool autoUpdate;
    public TerrainType[] regions;
    private void Awake()
    {
        noiseData.seed = Random.Range(-10000, 10000);
        falloffMap = FalloffGen.GenerateFalloffMap(mapChunkSize+2);
        MapGenerator();
    }
    void OnValuesUpdated()
    {
        if (!Application.isPlaying)
        {
            MapGenerator();
        }
    }
   /* void OnTextureValuesUpdated()
    {
        textureData.ApplyToMaterial(terrainMaterial);

    }*/
    private void Start()
    {


    }

    public void MapGenerator()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize,mapChunkSize,noiseData.seed,noiseData.noiseSize,noiseData.octaves,noiseData.persistance,noiseData.lacinarity,noiseData.offset);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for(int y = 0; y < mapChunkSize; y++)
        {
            for(int x =0; x<mapChunkSize; x++)
            {
                if (terrainData.useFalloff)
                {
                    noiseMap[x,y]=Mathf.Clamp01(noiseMap[x, y]-falloffMap[x, y]);
                }
                float currentHeight = noiseMap[x,y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }        

            }
        }
        DisplayMap display=FindObjectOfType<DisplayMap>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGen.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGen.TextureFromColourMap(colorMap,mapChunkSize,mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap,terrainData.meshHeightMultip,terrainData.meshHeightCurve,levelOfDetail),TextureGen.TextureFromColourMap(colorMap,mapChunkSize,mapChunkSize));


        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGen.TextureFromHeightMap(FalloffGen.GenerateFalloffMap(mapChunkSize)));
        }                textureData.UpdateMeshHeights(terrainMaterial,terrainData.minHeight,terrainData.maxHeight);

    }

    private void OnValidate()
    {
        if(terrainData != null)
        {
            terrainData.OnValuesUpdated -= OnValuesUpdated;

            terrainData.OnValuesUpdated += OnValuesUpdated;
        }
        if (noiseData != null)
        {
            noiseData.OnValuesUpdated -= OnValuesUpdated;

            noiseData.OnValuesUpdated += OnValuesUpdated;
        }
        falloffMap = FalloffGen.GenerateFalloffMap(mapChunkSize+2);
        if (textureData != null)
        {
            textureData.OnValuesUpdated -= OnValuesUpdated;

            textureData.OnValuesUpdated += OnValuesUpdated;
        }
    }

}
[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}
