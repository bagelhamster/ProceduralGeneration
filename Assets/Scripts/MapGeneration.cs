using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public enum DrawMode {NoiseMap,ColourMap,Mesh,FalloffMap};
    public DrawMode drawMode;
    const int mapChunkSize = 241;
    [Range(0,6)]
    public int levelOfDetail;
    public float noiseSize;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacinarity;

    public bool useFalloff;
    float[,]falloffMap;
    public int seed;
    public Vector2 offset;
    public float meshHeightMultip;
    public AnimationCurve meshHeightCurve;
    public bool autoUpdate;
    public TerrainType[] regions;
    private void Awake()
    {
        seed = Random.Range(-10000, 10000);
        falloffMap = FalloffGen.GenerateFalloffMap(mapChunkSize);
        MapGenerator();
    }
    private void Start()
    {


    }
    public void MapGenerator()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize,mapChunkSize,seed,noiseSize,octaves,persistance,lacinarity,offset);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for(int y = 0; y < mapChunkSize; y++)
        {
            for(int x =0; x<mapChunkSize; x++)
            {
                if (useFalloff)
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
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap,meshHeightMultip,meshHeightCurve,levelOfDetail),TextureGen.TextureFromColourMap(colorMap,mapChunkSize,mapChunkSize));

        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGen.TextureFromHeightMap(FalloffGen.GenerateFalloffMap(mapChunkSize)));
        }
    }


    void OnValidate()
    {
        if (lacinarity < 1)
        {
            lacinarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
        falloffMap = FalloffGen.GenerateFalloffMap(mapChunkSize);

    }
}
[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}
