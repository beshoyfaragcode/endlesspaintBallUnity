using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLevelsGradient : MonoBehaviour {
    public static Gradient GetLevelsGradientFromMap(mapMaker mapToGetFrom)
    {
        //
        mapMaker.terrainTypes[] levels;
        levels = mapToGetFrom.regions;
        int clamp = levels.Length;
        clamp = Mathf.Clamp(clamp, levels.Length, 8);
        GradientColorKey[] MyKeys = new GradientColorKey[clamp];


        for (int i = 0; i < clamp; i++)
        {


            MyKeys[i].color = levels[i].color;
            MyKeys[i].time = levels[i].hieght;



        }
        Gradient retrunGradient = new Gradient();
        retrunGradient.SetKeys(MyKeys, retrunGradient.alphaKeys);
        return retrunGradient;


    }
}
