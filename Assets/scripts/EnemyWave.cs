using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyWave : MonoBehaviour {
    public int maxEnemies;
    public int EnemiesNow;
    public float timeBeforeDestroy = 2f;
    public EnemyData[] enemiesToPickFrom;
    public Enemy Map;
    public float enemyYPos = 50f;
    public int[] randomInts;
    public  int randomIntsSize;
    public Transform[] Children;
    public int childsize;
    Vector3 pos;
    float noiseVal;
    private bool SpownEnemies;
    public float maxEnemiesPrecent = 0.5f;
    


    // Use this for initialization
    void Start() {
       
        
        randomInts = new int[randomIntsSize];
       
        
    }

    // Update is called once per frame
    void Update() {
        
        childsize = Children.Length;
        if (SpownEnemies)
        {
            SpownEnemy(pos, noiseVal);
            SpownEnemies = false;
        }
    }
    public void GetEnemyMaps()
    {
        //Debug.Log(" getEnemyMaps has been called there are  " + EnemiesNow  + " Enemies on screen "  + " out of " + maxEnemies + " Enemies total ");

        foreach (Vector2 key in Map.EnemyMaps.Keys)
        {
            float[,] MapNow = Map.EnemyMaps[key];
            Vector3[,] enemyPoes = SpownEnemiesInfo(key, MapNow);
            SetPoes(MapNow, enemyPoes);
        }
        /*
        if (EnemiesNow < (maxEnemies * maxEnemiesPrecent))
        {
            Debug.Log(" calling GetEnemyMaps for the secend time because EnemiesNow  is  " + EnemiesNow + " this is less than  (maxEnemies * maxEnemiesPrecent) :  (maxEnemies * maxEnemiesPrecent) is " + (maxEnemies * maxEnemiesPrecent));
            GetEnemyMaps();
          
        }
        */
      
    }
    Vector3[,] SpownEnemiesInfo(Vector2 center, float[,] map)
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
                Vector3 enemyPos = new Vector3(enemyPos2D.x, enemyYPos, enemyPos2D.y);
                allEnemyPostions[x, y] = enemyPos;

            }
        }
        return allEnemyPostions;
    }
    void SetPoes(float[,] noise, Vector3[,] poes)
    {
        if (noise.Length != poes.Length)
        {
            //make noise and poes the same length 
        }
        else
        {
            for (int x = 0; x < noise.GetLength(0); x++)
            {
                for (int y = 0; y < noise.GetLength(0); y++)
                {
                    pos = poes[x, y];
                    noiseVal = noise[x, y];
                    SpownEnemies = true;
                    //SpownEnemy(pos, noiseVal);
                }
            }
        }

    }
    void SpownEnemy(Vector3 pos, float probablity)
    {
        if (randomInts.Length == 0)
        {
            Debug.LogError(" randomInts has a a size of zero seting it to defalt values  ");
            int w = enemiesToPickFrom.Length - 1;
            int x = enemiesToPickFrom.Length - 1;
            Children = enemiesToPickFrom[x].parent.GetComponentsInChildren<Transform>(true);
            int y = Children.Length - 1;
            int z = 50;
            randomInts[1] = w;
            randomInts[2] = x;
            randomInts[3] = y;
            randomInts[4] = z;

        }
        int i = randomInts[1];
        Quaternion rot = Quaternion.Euler(enemiesToPickFrom[i].rotion);

        if (EnemiesNow >= maxEnemies)
        {
            //Destroy enemy
            int x = randomInts[2];
            Children = enemiesToPickFrom[x].parent.GetComponentsInChildren<Transform>(true);
            for (int z = 0; z < enemiesToPickFrom.Length; z++)
            {
                Children = enemiesToPickFrom[z].parent.GetComponentsInChildren<Transform>(true);
                for (int a = 0; a < Children.Length; a++)
                {
                    if (Children[a].transform.position.y <= -5)
                    {
                        Destroy(Children[a].gameObject, timeBeforeDestroy);
                        if (EnemiesNow <= 0)
                        {
                            EnemiesNow = 0;
                            SpownEnemy(pos, probablity);
                        }
                        else
                        {
                            EnemiesNow--;

                        }


                    }
                    else
                    {
                        Children = enemiesToPickFrom[i].parent.GetComponentsInChildren<Transform>(true);
                        int y = randomInts[3];
                        Destroy(Children[y].gameObject, timeBeforeDestroy);
                        if (EnemiesNow < 0)
                        {
                            EnemiesNow = 0;
                            SpownEnemy(pos, probablity);
                        }
                        else
                        {
                            EnemiesNow--;

                        }

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
            else
            {
                /* 
                if (EnemiesNow < (maxEnemies * maxEnemiesPrecent))
                {
                   
                        Debug.Log(" about to call SpownEnemy from SpownEnemy ");
                        SpownEnemy( pos,  probablity);
                        Debug.Log(" about to call SpownEnemy from SpownEnemy ");

                }
                */

            }

        }
    }
    bool CheckProb(float prob)
    {
        int x = Mathf.RoundToInt(prob * 100);
        int check = randomInts[4];
        if (check <= x)
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
    
    public   void SetRandomInts( int childrenSize)
    {
        
        int w = Random.Range(0, enemiesToPickFrom.Length);
        int x = Random.Range(0, enemiesToPickFrom.Length);
        int y;
        if (childrenSize != 0)
        {
            y = Random.Range(0, childrenSize);
        }
        else
        {
            Children = enemiesToPickFrom[x].parent.GetComponentsInChildren<Transform>(true);
            y = Random.Range(0, childrenSize);
        }
        
        int z = Random.Range(0, 100);
        if (randomInts.Length >= 4)
        {
            randomInts[1] = w;
            randomInts[2] = x;
            randomInts[3] = y;
            randomInts[4] = z;

        }
        else
        {
            randomInts = new int[5];
            SetRandomInts(childrenSize);

        }



    }
}
