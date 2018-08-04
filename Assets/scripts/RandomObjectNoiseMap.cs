using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomObjectNoiseMap : MonoBehaviour {
    int height;
    int width;
    float scale;
    int octaves;
    float persistance;
    float lacunarity;
    long Seed;
    Vector2 offset;
    public mapMaker map;
    public float[,] noise;
    //public GameObject plane;




    // Use this for initialization
    void Start() {
        SetFromMap();
        noise = GetNoise(offset);
        //flipNoise(noise);
        



    }

    // Update is called once per frame
    void Update() {

    }
    void SetFromMap()
    {
        scale = map.scale;
        octaves = map.octaves;
        persistance = map.presistance;
        lacunarity = map.lacunarity;
        Seed = getRandomSeed.GetSeed();
        offset = map.offSet;
        width = map.enmayMapChunkSize;
        height = map.enmayMapChunkSize;



    }
    public float[,] GetNoise(Vector2 offset)
    {
        float[,] noiseToReturn = nosie.GenrateNosieMap(width, height, scale, octaves, persistance, lacunarity, Seed, offset, nosie.NormilizeMode.local);
        noiseToReturn = flipNoise(noiseToReturn);
        return noiseToReturn;

    }
    float[,] flipNoise(float[,] noise)
    {
        float[,] noiseReturn = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                noiseReturn[x, y] = 1 - noise[x, y];
            }
        }
        return noiseReturn;
    }
   


}
