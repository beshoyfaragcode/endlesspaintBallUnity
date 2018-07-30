using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public endLessTerrain mapEndLess;
    public mapMaker map;
    public randomEnemyNoiseMap EmenyMap;
    public Dictionary<Vector2, float[,]> EnemyMaps = new Dictionary<Vector2, float[,]>();

    // Use this for initialization
    void Start() {
      
    }

    // Update is called once per frame
    void Update() {

    }
   
    public  void UpdateEnemyMaps()
    {
        for (int x = 0; x < (map.enmayMapChunkSize + 2); x++)
        {
           
                for (int y = 0; y < (map.enmayMapChunkSize + 2); y++)
                {
                    Vector2 key = new Vector2(x, y);
                    if (mapEndLess.chunkDictionary.ContainsKey(key))
                    {
                        float[,] map = EmenyMap.GetNoise(key);
                        if (EnemyMaps.ContainsKey(key))
                        {
                            //find off later 
                        }
                        else
                        {
                            EnemyMaps.Add(key, map);
                            Debug.Log(" added enemy map at " + key);
                            
                        }

                    }
                    else
                    {
                        Debug.Log(" no enemy map at " + key);
                        break;
                    }

                    
                }
            }
          
        }

    }

