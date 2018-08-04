using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;

public class mapMaker : MonoBehaviour {
    public enum DrawMode{nosieMap , colorMap,Mesh,FallOffMap} ;
    public DrawMode drawMode;
    public nosie.NormilizeMode mode;
    public const int mapChunckSize = 239 ;
    [Range (0,6)]
    public int LevelOfDetail ;
    public float scale ;
    public int octaves ;
    [Range(0,1)]
    public float presistance ;
    public float lacunarity ;
    public Int64 seed ;
    public Vector2 offSet;
    public  bool AtuoUpadate;
    public bool useFalloffmap;
    public float meshHeightMultiplier ;
    public AnimationCurve MeshCurve ;
    public terrainTypes[] regions;
    public Vector2 chunckOffset ;
    public int enmayMapChunkSize;
    public bool randomMapOnPlay;

    float[,] FallOffMap;


    Queue<MapDataThread<MapData>> MapQueue = new Queue<MapDataThread<MapData>> ();
    Queue<MapDataThread<MeshData>> MeshQueue = new Queue<MapDataThread<MeshData>> ();

    void Awake() {
        enmayMapChunkSize = mapChunckSize;
        seed = getRandomSeed.GetSeed();
        if (randomMapOnPlay)
        {
            mode = nosie.NormilizeMode.local;
            octaves = UnityEngine.Random.Range(0, 10);
            presistance = UnityEngine.Random.Range(0.0f, 0.77f);
            lacunarity = UnityEngine.Random.Range(0.0f, 3.0f);
            seed = getRandomSeed.GetSeed();

        }
        FallOffMap = FalloffMaker.MakefalloffMap(mapChunckSize);
    }

    public void DrawMap(){
        MapData mapData = MakeMap(chunckOffset);
         MapShow show = FindObjectOfType<MapShow>();
        
        if (drawMode == DrawMode.nosieMap){
            if (randomMapOnPlay)
            {
                mode = nosie.NormilizeMode.local;
                octaves = UnityEngine.Random.Range(0, 10);
                presistance = UnityEngine.Random.Range(0.0f, 0.77f);
                lacunarity = UnityEngine.Random.Range(0.0f, 3.0f);
                seed = getRandomSeed.GetSeed();

            }
            show.DrawTexture(textureMaker.textureFromHeightMap(mapData.HeightMap)) ;
	    }else if(drawMode == DrawMode.colorMap){
            if (randomMapOnPlay)
            {
                mode = nosie.NormilizeMode.local;
                octaves = UnityEngine.Random.Range(0, 10);
                presistance = UnityEngine.Random.Range(0.0f, 0.77f);
                lacunarity = UnityEngine.Random.Range(0.0f, 3.0f);
                seed = getRandomSeed.GetSeed();

            }
            show.DrawTexture(textureMaker.textureFromColorMap(mapData.colorMap,mapChunckSize,mapChunckSize));
	    }else if (drawMode == DrawMode.Mesh){
            if (randomMapOnPlay)
            {
                mode = nosie.NormilizeMode.local;
                octaves = UnityEngine.Random.Range(0, 10);
                presistance = UnityEngine.Random.Range(0.0f, 0.77f);
                lacunarity = UnityEngine.Random.Range(0.0f, 3.0f);
                seed = getRandomSeed.GetSeed();

            }
            show.DrawMesh(meshMaker.GenerateTerrainMesh(mapData.HeightMap,meshHeightMultiplier,MeshCurve,LevelOfDetail),textureMaker.textureFromColorMap(mapData.colorMap,mapChunckSize,mapChunckSize));
	    }else if (drawMode == DrawMode.FallOffMap){
		    show.DrawTexture(textureMaker.textureFromHeightMap(FalloffMaker.MakefalloffMap(mapChunckSize)));
	    }
	
    }


    public void RequsetMap(Vector2 center ,Action<MapData> callBack){
        ThreadStart threadStart = delegate {
            MapDataThreadVoid( center ,callBack);
        };
        new Thread(threadStart).Priority = System.Threading.ThreadPriority.Lowest;
      new Thread(threadStart).IsBackground = true;
        Debug.Log("RequsetingMap");
       new Thread(threadStart).Start();
        Debug.Log("RequsetedMap");
        
    
    }
    public void MapDataThreadVoid(Vector2 center ,Action<MapData> callBack){
        MapData mapData = MakeMap(center);
        lock (MapQueue) {
        MapQueue.Enqueue(new MapDataThread<MapData>(callBack,mapData));

        }
    }
    public void RequsetMesh(MapData mapData ,int LOD ,Action<MeshData> callBack){
       ThreadStart threadStart = delegate {
            MeshDataThreadVoid(mapData ,LOD,callBack);
        };
           new Thread(threadStart).Priority = System.Threading.ThreadPriority.Lowest;
      new Thread(threadStart).IsBackground = true;
        Debug.Log("RequsetingMesh");
       new Thread(threadStart).Start();
        Debug.Log("RequsetedMesh");
        
        

    }
    public void MeshDataThreadVoid(MapData mapData,int LOD ,Action<MeshData> callBack){
        MeshData meshData = meshMaker.GenerateTerrainMesh(mapData.HeightMap,meshHeightMultiplier,MeshCurve,LOD);
        lock (MeshQueue){
            MeshQueue.Enqueue(new  MapDataThread<MeshData>(callBack, meshData))  ;
        }
    }
    public void Update(){
        if (MapQueue.Count > 0){
            for(int i = 0 ; i < MapQueue.Count ; i++){
                MapDataThread<MapData> info = MapQueue.Dequeue();
                info.callback(info.prameter);
            }
        }
        if (MeshQueue.Count > 0){
            for(int i = 0 ; i < MeshQueue.Count ; i++){
                MapDataThread<MeshData> info = MeshQueue.Dequeue();
                info.callback(info.prameter);
            }
        }
        chunckOffset = new Vector2 (UnityEngine.Random.Range(0.7f,70.75f),UnityEngine.Random.Range(0.7f,70.75f));
    }

    MapData MakeMap(Vector2 center){

	    float[,] nosieMap = nosie.GenrateNosieMap(mapChunckSize + 2,mapChunckSize + 2,scale,octaves,presistance,lacunarity,seed, center + offSet,mode);
	    Color32[] colorMap = new Color32[mapChunckSize*mapChunckSize];
	    for(int y = 0 ; y <mapChunckSize;y++){

		    for(int x = 0 ; x <mapChunckSize;x++){
                if(useFalloffmap){
                 nosieMap[x,y] = Mathf.Clamp01(nosieMap[x,y] - FallOffMap[x,y]) ;
                }
			    float currentHeight = nosieMap[x,y];

			    for(int i = 0 ; i <regions.Length;i++){
				    if(currentHeight>= regions[i].hieght){
					    colorMap[y*mapChunckSize+x] = regions[i].color ;
					   
				    }else{
                         break;
                    }

	    }
	    }
            
	    }

	   
	return new  MapData (nosieMap,colorMap);
    }
    
    void OnValidate(){
			    if(lacunarity< 1 ){
			    lacunarity = 1;
		    }
			    if(octaves < 0){
			    octaves = 1;
		    }
          FallOffMap = FalloffMaker.MakefalloffMap(mapChunckSize);
	    }
        
    struct MapDataThread<T>{
        public Action<T> callback;
        public T prameter ;

         public MapDataThread ( Action<T> callback,T prameter){
            this. callback =  callback;
            this.prameter = prameter;
        }

    }


    [System.Serializable]
    public struct terrainTypes{
	    public string name ;
	    public float hieght ;
	    public Color color;

    }

    public struct MapData {
        public float[,] HeightMap;
        public Color32[] colorMap;
         
        public MapData ( float[,] HeightMap,Color32[] colorMap){
            this.HeightMap = HeightMap;
            this.colorMap = colorMap;
        }

        }
    }

