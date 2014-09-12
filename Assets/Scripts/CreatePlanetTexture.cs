using UnityEngine;
using System.Collections;

public class CreatePlanetTexture : MonoBehaviour
{
    public int texWidth = -1;
    public int texHeight = -1;

    [Range(0, 10)]
    public int maxHeightDifference = 0; //0 creates completly flat planet, 10 wavy

    [Range(0.0f, 100.0f)]
    public float heightJitter = 0; //How packed are height changes

    private Texture2D tex;
    byte[] color;

    int[] pixelmap;

    void Awake()
    {
        if (texWidth == -1) texWidth = 128;
        if (texHeight == -1) texHeight = 64;
        if (maxHeightDifference == -1) maxHeightDifference = 0;

        tex = new Texture2D(texWidth, texHeight, TextureFormat.ARGB32, false, true);
    }

    void Start()
    {       
        renderer.material = new Material(Shader.Find("Self-Illumin/Diffuse"));

        GenerateTerrain();
        renderer.material.mainTexture = tex;
    }

    void GenerateTerrain()
    {
        CreateHeightmapShading();
        pixelmap = new int[texWidth * texHeight];

        for (int i = 0; i < texWidth * texHeight; i++)
        {
            pixelmap[i] = 0;
        }

        for (int x = 0; x < texWidth; x++)
        {
            for (int y = 0; y < texHeight; y++)
            {
                //pixelmap[y * texWidth + x] = Random.Range(0, maxHeightDifference+1);
            }
        }

        Color32[] colorSet = new Color32[texWidth * texHeight];

        for (int i = 0; i < pixelmap.Length; i++)
        {
            byte tmp = color[pixelmap[i]];
            colorSet[i] = new Color32(tmp, tmp, tmp, 255);
        }

        tex.SetPixels32(colorSet);
        tex.Apply();
    }

    void CreateHeightmapShading()
    {
        color = new byte[maxHeightDifference + 1];
        color[0] = 0;

        if (maxHeightDifference > 0)
        {
            byte part = (byte)(255 / maxHeightDifference);
            for (int i = 1; i < maxHeightDifference + 1; i++)
            {
                color[i] = (byte)(part * i);
                Debug.Log(i + ":" + color[i]);
            }
        }
    }

    int GrabColorFromPixel(int x, int y) 
    {
        return pixelmap[y * texWidth + x];
    }
}