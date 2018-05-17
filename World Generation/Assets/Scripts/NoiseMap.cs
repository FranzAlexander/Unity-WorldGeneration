using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseMap
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offSet)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int ii = 0; ii < octaves; ii++)
        {
            float offSetX = prng.Next(-100000, 100000) + offSet.x;
            float offSetY = prng.Next(-100000, 100000) + offSet.y;
            octaveOffsets[ii] = new Vector2(offSetX, offSetY);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int yy = 0; yy < mapHeight; yy++)
        {
            for (int xx = 0; xx < mapWidth; xx++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int ii = 0; ii < octaves; ii++)
                {
                    float sampleX = (xx - halfWidth) / scale * frequency + octaveOffsets[ii].x;
                    float sampleY = (yy - halfHeight) / scale * frequency + octaveOffsets[ii].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                    noiseHeight += perlinValue * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[xx, yy] = noiseHeight;
            }
        }

        for (int yy = 0; yy < mapHeight; yy++)
        {
            for (int xx = 0; xx < mapWidth; xx++)
            {
                noiseMap[xx, yy] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[xx, yy]);
            }
        }

        return noiseMap;
    }
}
