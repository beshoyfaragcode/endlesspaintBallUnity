using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectWave : MonoBehaviour {
    public int maxObjects;
    public int ObjectsNow;
    public float timeBeforeDestroy = 2f;
    public ObjectData[] ObjectsToPickFrom;
    public Objects Map;
    public float ObjectYPos = 50f;
    public int[] randomInts;
    public  int randomIntsSize;
    public Transform[] Children;
    public int childsize;
    Vector3 pos;
    float noiseVal;
    private bool SpownObject;
    public float maxObjectsPrecent = 0.5f;
    public float ObjMinYPos;
   



    // Use this for initialization
    void Start() {
        
        maxObjects = maxObjects * ObjectsToPickFrom.Length;

        MakeParentsFromArray();
        randomInts = new int[randomIntsSize];
       
        
    }
    void MakeParentsFromArray()
    {
        for (int i = 0; i < ObjectsToPickFrom.Length; i++)
        {
            GameObject ObjParent = new GameObject(ObjectsToPickFrom[i].prefab.name);
            ObjParent = Instantiate(ObjParent, Vector3.zero, Quaternion.Euler(Vector3.zero), gameObject.transform);
            ObjectsToPickFrom[i].parent = ObjParent.transform;
        }
    }

    // Update is called once per frame
    void Update() {
        //SetRandomInts(Children.Length);
       // GetEnemyMaps();
        childsize = Children.Length;
        if (SpownObject)
        {
            SpownObjects(pos, noiseVal);
            SpownObject = false;
        }
    }
    public void GetObjectMaps()
    {
      

        foreach (Vector2 key in Map.ObjectMaps.Keys)
        {
            float[,] MapNow = Map.ObjectMaps[key];
            Vector3[,] ObjectPoes = SpownObjectsInfo(key, MapNow);
            SetPoes(MapNow, ObjectPoes);
        }
        
      
    }
    Vector3[,] SpownObjectsInfo(Vector2 center, float[,] map)
    {
        Vector2 RealCenter = center * Map.map.enmayMapChunkSize;
        Vector3[,] allObjectPostions = new Vector3[map.GetLength(0), map.GetLength(1)];

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                float xOffset = (Map.map.enmayMapChunkSize / 2) - (center.x - 1);
                float yOffSet = (Map.map.enmayMapChunkSize / 2) - (center.y - 1);
                Vector2 offSet = new Vector2(xOffset, yOffSet);
                Vector2 ObjectPos2D = RealCenter + offSet;
                Vector3 ObjectPos = new Vector3(ObjectPos2D.x, ObjectYPos, ObjectPos2D.y);
                allObjectPostions[x, y] = ObjectPos;

            }
        }
        return allObjectPostions;
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
                    SpownObject = true;
                    
                }
            }
        }

    }
    void SpownObjects(Vector3 pos, float probablity)
    {
        if (randomInts.Length == 0)
        {
            Debug.LogError(" randomInts has a a size of zero seting it to defalt values  ");
            int w = ObjectsToPickFrom.Length - 1;
            int x = ObjectsToPickFrom.Length - 1;
            Children = ObjectsToPickFrom[x].parent.GetComponentsInChildren<Transform>(true);
            int y = Children.Length - 1;
            int z = 50;
            randomInts[1] = w;
            randomInts[2] = x;
            randomInts[3] = y;
            randomInts[4] = z;

        }
        int i = randomInts[1];
        Quaternion rot = Quaternion.Euler(ObjectsToPickFrom[i].rotion);

        if (ObjectsNow >= maxObjects)
        {
            //Destroy enemy
            int x = randomInts[2];
            Children = ObjectsToPickFrom[x].parent.GetComponentsInChildren<Transform>(true);
            for (int z = 0; z < ObjectsToPickFrom.Length; z++)
            {
                Children = ObjectsToPickFrom[z].parent.GetComponentsInChildren<Transform>(true);
                for (int a = 0; a < Children.Length; a++)
                {
                    if (Children[a].transform.position.y <= -5)
                    {
                        Destroy(Children[a].gameObject, timeBeforeDestroy);
                        if (ObjectsNow <= 0)
                        {
                            ObjectsNow= 0;
                            SpownObjects(pos, probablity);
                        }
                        else
                        {
                           ObjectsNow--;

                        }


                    }
                    else
                    {
                        Children = ObjectsToPickFrom[i].parent.GetComponentsInChildren<Transform>(true);
                        int y = randomInts[3];
                        Destroy(Children[y].gameObject, timeBeforeDestroy);
                        if (ObjectsNow < 0)
                        {
                            ObjectsNow = 0;
                            SpownObjects(pos, probablity);
                        }
                        else
                        {
                            ObjectsNow--;

                        }

                    }

                }
            }





        }

        
        else
        {
            if ( probability.CheckProb(probablity))
            {

                
                if (ObjectsToPickFrom[i].prefab.GetComponent<Rigidbody>() == null)
                {
                    ObjectsToPickFrom[i].prefab.AddComponent<Rigidbody>();
                }
                if (ObjectsToPickFrom[i].prefab.GetComponent<MeshFilter>() == null)
                {
                    ObjectsToPickFrom[i].prefab.AddComponent<MeshFilter>();
                }
                if (ObjectsToPickFrom[i].prefab.GetComponent<MeshCollider>() == null)
                {
                    if (ObjectsToPickFrom[i].prefab.GetComponent<BoxCollider>() == null || ObjectsToPickFrom[i].prefab.GetComponent<SphereCollider>() == null)
                    {
                        ObjectsToPickFrom[i].prefab.AddComponent<MeshCollider>();
                    }
                    
                }
                MeshCollider collider = ObjectsToPickFrom[i].prefab.GetComponent<MeshCollider>();
                if (ObjectsToPickFrom[i].prefab.GetComponent<MeshRenderer>() == null)
                {
                    ObjectsToPickFrom[i].prefab.AddComponent<MeshRenderer>();
                }
                MeshRenderer Meshrenderer = ObjectsToPickFrom[i].prefab.GetComponent<MeshRenderer>();
                collider.convex = true;
                if (ObjectsToPickFrom[i].prefab.GetComponent<ClampYpos>() == null)
                {
                    ObjectsToPickFrom[i].prefab.AddComponent< ClampYpos>();
                }
                ClampYpos  OBjkiler = ObjectsToPickFrom[i].prefab.GetComponent<ClampYpos>();
                OBjkiler.minYPos = ObjMinYPos;
                OBjkiler.retrunYPos = ObjectYPos;
                OBjkiler.OffSetRange = ObjectYPos * Random.Range(-ObjectYPos , ObjectYPos );







                GameObject obj =  Instantiate(ObjectsToPickFrom[i].prefab, pos, rot, ObjectsToPickFrom[i].parent);
                obj.tag = ObjectsToPickFrom[i].tagName;
                ObjectsNow++;
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
  
    [System.Serializable]
    public struct ObjectData
    {
        public string tagName;
        public enum OBJType { tree, rock , other  };
        public GameObject prefab;
        public Transform parent;
        public Vector3 rotion;

    }
    
    public   void SetRandomInts( int childrenSize)
    {
        
        int w = Random.Range(0, ObjectsToPickFrom.Length);
        int x = Random.Range(0, ObjectsToPickFrom.Length);
        int y;
        if (childrenSize != 0)
        {
            y = Random.Range(0, childrenSize);
        }
        else
        {
            Children = ObjectsToPickFrom[x].parent.GetComponentsInChildren<Transform>(true);
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
