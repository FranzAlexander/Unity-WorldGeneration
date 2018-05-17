using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator
{

    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();

        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        Color[] colorMap = new Color[width * height];
        for (int yy = 0; yy < height; yy++)
        {
            for (int xx = 0; xx < width; xx++)
            {
                colorMap[yy * width + xx] = Color.Lerp(Color.black, Color.white, heightMap[xx, yy]);
            }
        }

        return TextureFromColorMap(colorMap, width, height);
    }
}
