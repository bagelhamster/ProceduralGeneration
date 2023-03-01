using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mWidth, int mHeight,int seed, float size,int octaves, float persistance, float lacunarity,Vector2 offset)
    {
        float[,] noiseMap = new float[mWidth, mHeight];
        System.Random randomseed = new System.Random(seed);
        Vector2[] octavesOffsets = new Vector2[octaves];
            for(int i = 0; i < octaves; i++)
            {
            float offsetx = randomseed.Next(-1000000, 1000000)+offset.x;
            float offsety=randomseed.Next(-1000000, 1000000)+offset.y;
            octavesOffsets[i] = new Vector2(offsetx, offsety);
        }   
        if (size <= 0){
            size = 0.00001f;
        }
        float maxNoiseHeight=float.MinValue;
        float minNoiseHeight=float.MaxValue;
        float halfw = mWidth / 2f;
        float halfh = mHeight / 2f;

        for(int y = 0; y < mHeight; y++)
        {
            for (int x = 0; x < mWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float XSample = (x-halfw) / size * frequency + octavesOffsets[i].x;
                    float YSample = (y-halfh) / size * frequency + octavesOffsets[i].y;
                    float perlinValue = Mathf.PerlinNoise(XSample, YSample)*2-1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude*=persistance;
                    frequency*=lacunarity;
                }
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }else if(noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[y,x] = noiseHeight;
            }
        }
        for (int y = 0; y < mHeight; y++)
        {
            for (int x = 0; x < mWidth; x++)
            {
                noiseMap[x,y]=Mathf.InverseLerp(minNoiseHeight,maxNoiseHeight,noiseMap[x,y]);
            }
        }
                return noiseMap;
    }


}
