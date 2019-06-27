using System.Threading;
using UnityEngine;

public class ThreadTest : MonoBehaviour
{
    private bool isAThing;
    public int width = 1000;
    public int height = 1000;
    private Texture2D tex;
    public float scale = 0.1f;

    // Define: Note the comma
    Color[] perlinValues;

    Thread thread;
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialise
        perlinValues = new Color[width*height];

        tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        GetComponent<Renderer>().material.mainTexture = tex;
        
        
        // Start new thread
        thread = new Thread(Perlin);
        thread.Start();
        
        
    }

    private void Update()
    {
        if (thread != null && !thread.IsAlive)
        {
            // Write old values if they're there
            tex.SetPixels(perlinValues);
            tex.Apply();

//            Start new thread
            thread = new Thread(Perlin);
            thread.Start();
        }
    }

    public void Perlin()
    {
        float perlinNoise;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                perlinNoise = Mathf.PerlinNoise(x * scale/100f, y * scale/100f);
                perlinValues[x+(y*width)] = new Color(perlinNoise, perlinNoise, perlinNoise);
            }
        }
    }
    
    
}
