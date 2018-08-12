using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {

    public int rangeFromPlayer;
    float[,] EnemyMap;
    float scale;
    int octaves;
    float presistance;
    float lacunarity;
    long seed;
    Vector2 offset;
    

    public void Start()
    {
        GetRandomVal();
        EnemyMap = nosie.GenrateNosieMap(rangeFromPlayer, rangeFromPlayer, scale, octaves, presistance, lacunarity, seed, offset, nosie.NormilizeMode.local);
    }
    // Update is called once per frame
    void Update () {
		
	}
    void GetRandomVal()
    {
        scale = Random.Range(0.0f, 26.0f);
        octaves = Random.Range(0, 10);
        presistance = Random.Range(0.0f, 1.0f);
        lacunarity = Random.Range(0.0f, 3.0f);
        seed = getRandomSeed.GetSeed();
        offset = new Vector2(Random.Range(-rangeFromPlayer, rangeFromPlayer), Random.Range(-rangeFromPlayer, rangeFromPlayer));




    }
    
}
