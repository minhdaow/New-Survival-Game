using UnityEngine;

public class SetTerrainSplatmap : MonoBehaviour
{
    public Terrain terrain;
    public Texture2D newSplatmap;

    void Start()
    {
        var terrainData = terrain.terrainData;

        // Ensure texture format matches number of terrain layers
        RenderTexture rt = new RenderTexture(terrainData.alphamapWidth, terrainData.alphamapHeight, 0);
        Graphics.Blit(newSplatmap, rt);

        RenderTexture.active = rt;
        Texture2D temp = new Texture2D(rt.width, rt.height, TextureFormat.RGBA32, false);
        temp.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        temp.Apply();
        RenderTexture.active = null;

        float[,,] newAlphaMaps = new float[rt.width, rt.height, terrainData.alphamapLayers];

        for (int y = 0; y < rt.height; y++)
        {
            for (int x = 0; x < rt.width; x++)
            {
                Color pixel = temp.GetPixel(x, y);
                float total = pixel.r + pixel.g + pixel.b + pixel.a;
                if (total == 0) total = 1f;

                newAlphaMaps[x, y, 0] = pixel.r / total;
                if (terrainData.alphamapLayers > 1) newAlphaMaps[x, y, 1] = pixel.g / total;
                if (terrainData.alphamapLayers > 2) newAlphaMaps[x, y, 2] = pixel.b / total;
                if (terrainData.alphamapLayers > 3) newAlphaMaps[x, y, 3] = pixel.a / total;
            }
        }

        terrainData.SetAlphamaps(0, 0, newAlphaMaps);
    }
}
