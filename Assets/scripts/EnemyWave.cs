using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour {
    public int maxEnemies;
    public int EnemiesNow;
    public EnemyData[] enemiesToPickFrom;
    public Enemy Map;
    public float enemyYPos = 50f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       GetEnemyMaps();
    }
    public void GetEnemyMaps()
    {
        foreach(Vector2 key in Map.EnemyMaps.Keys)
        {
            float[,] MapNow = Map.EnemyMaps[key];
            Vector3[,] enemyPoes = SpownEnemiesInfo(key, MapNow);
            SetPoes(MapNow, enemyPoes);
        }
    }
    Vector3[,] SpownEnemiesInfo (Vector2 center, float[,] map)
    {
        Vector2 RealCenter = center * Map.map.enmayMapChunkSize;
        Vector3[,] allEnemyPostions = new Vector3[map.GetLength(0), map.GetLength(1)];

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                float xOffset = (Map.map.enmayMapChunkSize / 2) - (center.x - 1);
                float yOffSet = (Map.map.enmayMapChunkSize / 2) - (center.y - 1);
                Vector2 offSet = new Vector2(xOffset, yOffSet);
                Vector2 enemyPos2D = RealCenter + offSet;
                Vector3 enemyPos = new Vector3(enemyPos2D.x,enemyYPos,enemyPos2D.y);
                allEnemyPostions[x, y] = enemyPos;

            }
        }
        return allEnemyPostions;
    }
    void SetPoes (float[,] noise , Vector3[,] poes)
    {
        if(noise.Length != poes.Length)
        {
            //make noise and poes the same length 
        }
        else
        {
            for (int x = 0; x < noise.GetLength(0); x++)
            {
                for (int y = 0; y < noise.GetLength(0); y++)
                {
                    Vector3 pos = poes[x, y];
                    float noiseVal = noise[x, y];
                    SpownEnemy(pos, noiseVal);
                }
            }
        }
       
    }
    void SpownEnemy(Vector3 pos, float probablity)
    {
      
        int i = Random.Range(0, enemiesToPickFrom.Length);
        Quaternion rot = Quaternion.Euler(enemiesToPickFrom[i].rotion);

        if (EnemiesNow >= maxEnemies)
        {
            //Destroy enemy
            int x = Random.Range(0, enemiesToPickFrom.Length);
            GameObject[] Children = enemiesToPickFrom[i].parent.GetComponentsInChildren<GameObject>(true);
            for (int z = 0; z < enemiesToPickFrom.Length; z++)
            {
                Children = enemiesToPickFrom[z].parent.GetComponentsInChildren<GameObject>(true);
                for (int a = 0; a <Children.Length ; a++)
                {
                    if (Children[a].transform.position.y <= -5)
                    {
                        Destroy(Children[a]);
                    }
                    else
                    {
                        Children = enemiesToPickFrom[i].parent.GetComponentsInChildren<GameObject>(true);
                        int y = Random.Range(0, Children.Length);
                        Destroy(Children[y]);
                    }
                   
                }
            }
             



            
        }
        else
        {
            if (CheckProb(probablity))
            {

                Instantiate(enemiesToPickFrom[i].prefab, pos, rot, enemiesToPickFrom[i].parent);
                EnemiesNow++;
            }

        }
    }
    bool CheckProb (float prob)
    {
        int x = Mathf.RoundToInt(prob * 100);
        int check = Random.Range(0, 100);
        if(check <= x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    [System.Serializable]
    public struct EnemyData
    {

        public GameObject prefab;
        public Transform parent;
        public Vector3 rotion;

    }
}
