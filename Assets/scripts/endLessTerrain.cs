using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLessTerrain : MonoBehaviour {
    public Enemy enemy;
    static float scale = 1;
	public float realscale = 1;
	public const float viewerMoveFromChunkUpdate = 25f;
	public const float sqrViewerMoveFromChunkUpdate = viewerMoveFromChunkUpdate * viewerMoveFromChunkUpdate;
	public static float MaxView ;
	public Transform veiwer;
	public static Vector2 viewerPos;
	public  Vector2 viewerPosOld;
    static mapMaker MapGen ;
	 public mapMaker RealMapGen ;
	 public int chunkSize ;
    public int chunksInView;
	public Material mapMat;
	public LODinfo[] Levels;
    



	public Dictionary<Vector2,Chunk> chunkDictionary = new Dictionary<Vector2,Chunk>() ;
    public static List<Chunk> chunksShowen = new List<Chunk>();

	public void Start(){
	scale = realscale;
		
     MapGen = FindObjectOfType<mapMaker>();
		chunkSize = mapMaker.mapChunckSize - 1;
		MaxView = Levels[Levels.Length - 1].visableDstTreshhold ;
		chunksInView = Mathf.RoundToInt( MaxView / chunkSize) ;
		UpdateChunks();
	}
	public void Update(){

		viewerPos = new Vector2 (veiwer.position.x,veiwer.position.z) /scale;
		if((viewerPosOld - viewerPos ).sqrMagnitude > viewerMoveFromChunkUpdate ){
			viewerPosOld = viewerPos ;
			UpdateChunks();
				
		}
		
		
	}

	public void UpdateChunks(){

		
        for(int i = 0 ; i < chunksShowen.Count;i++){
            chunksShowen[i].setvisable(false);
        }
        chunksShowen.Clear();

		int  ChunkNowX = Mathf.RoundToInt(viewerPos.x/chunkSize);
		int  ChunkNowY = Mathf.RoundToInt(viewerPos.y/chunkSize);


		 for(int y = -chunksInView ; y <=chunksInView;y++){
				for(int x = -chunksInView ; x <=chunksInView;x++){
				Vector2 ViewedChunk = new Vector2 (ChunkNowX + x, ChunkNowY + y);

				if (chunkDictionary.ContainsKey(ViewedChunk)){
					chunkDictionary[ViewedChunk].updateChunk();

                    

				}else{

					chunkDictionary.Add(ViewedChunk,new Chunk(ViewedChunk,chunkSize, Levels,transform, mapMat));
				}
			}
		 }

        enemy.UpdateEnemyMaps();
    }
	public class Chunk {
		 GameObject meshObj;
		 static Vector2 position ;
		 Bounds bounds ;
		 mapMaker.MapData mapData ;
		 MeshRenderer meshRenderer;
		 MeshFilter meshFilter;
		 MeshCollider collider ;
		 LODinfo [] Levels;
		 LODmesh[] LODMeshes;
		 LODmesh LODcollider ;

		mapMaker.MapData mapDataChunck ;
		bool HasMapDataChunck ;
		int PreLevelsIndex = -1 ;
       // public Enemy enemy;

        public Chunk (Vector2 cord,int size,LODinfo [] Levels,Transform parent,Material Mat) {
			this.Levels = Levels;
			position = cord * size ;
			bounds = new Bounds (position,Vector2.one * size);
			Vector3 pos3D = new Vector3 (position.x,0,position.y);
			meshObj = new GameObject("chunk");
			meshRenderer = meshObj.AddComponent<MeshRenderer>();
			meshFilter = meshObj.AddComponent<MeshFilter>();
			collider = meshObj.AddComponent<MeshCollider>();
			meshRenderer.material = Mat;
			meshObj.transform.position = pos3D * scale;
            meshObj.transform.parent = parent ;
			meshObj.transform.localScale = Vector3.one * scale;
			setvisable(false);
			LODMeshes = new LODmesh[Levels.Length];

			for (int i = 0; i < Levels.Length; i++){
				LODMeshes[i] = new LODmesh(Levels[i].LOD,updateChunk);
				if(Levels[i].useForCollider){
					LODcollider = LODMeshes[i];
				}
			}
			MapGen.RequsetMap(position,OnMapDataRevired);

	         }

		    void OnMapDataRevired(mapMaker.MapData  mapData){
				this.mapDataChunck = mapData ;
				HasMapDataChunck = true;
				Texture2D texture =  textureMaker.textureFromColorMap(mapData.colorMap,mapMaker.mapChunckSize,mapMaker.mapChunckSize);
				meshRenderer.material.mainTexture = texture;
				
				 updateChunk();
				//MapGen.RequsetMesh(mapData,OnMesh);

		}
		/* 
		void OnMesh(MeshData meshData){
			meshFilter.mesh = meshData.CreateMesh();
			
		}
		*/
		
		public void updateChunk(){
            
			float viewNear = Mathf.Sqrt(bounds.SqrDistance(viewerPos));
			bool visable = viewNear  <= MaxView ;
			setvisable(visable);
			if(HasMapDataChunck){
					if(visable){
						int LevelsIndex = 0 ;
						for (int i = 0; i < Levels.Length - 1 ; i++){
							if(viewNear >  Levels[i].visableDstTreshhold ){
								LevelsIndex = i + 1;
							} else{
								break;
							}
						}
						if(LevelsIndex != PreLevelsIndex){
							LODmesh lodMesh = LODMeshes[LevelsIndex];
							if(lodMesh.HasMesh){
								PreLevelsIndex = LevelsIndex;
								meshFilter.mesh = lodMesh.mesh;
								collider.sharedMesh = lodMesh.mesh ;
								
							}else if (!lodMesh.requestedMesh){
								lodMesh.requestMesh(mapDataChunck);
							}
						}
						if(LevelsIndex == 0){
							if(LODcollider.HasMesh){
								LODmesh lodMesh = LODMeshes[LevelsIndex];
								collider.sharedMesh = lodMesh.mesh;
							}else if ( !LODcollider.requestedMesh){
								LODcollider.requestMesh(mapData);
							}
						}
						chunksShowen.Add(this);
					}
					setvisable(visable);
					
		}	
	setvisable(visable);
           

        }
		public void setvisable (bool visable){

		 	meshObj.SetActive (visable);
			 
            
		}
        public bool isshowen(){
            return meshObj.activeSelf;
}
        	}

			class LODmesh {

				public Mesh mesh;
				public bool requestedMesh;
				public bool HasMesh;
				int LOD;
				System.Action updateChunkcallBack;
			


				public LODmesh(int lod,System.Action updateChunkcallBack){
					this.LOD = lod ;
					this.updateChunkcallBack = updateChunkcallBack;
				}
				 void onMeshRetruned(MeshData meshData){
					 mesh = meshData.CreateMesh();
					 
					 HasMesh = true;
					 updateChunkcallBack ();
				}
				public void requestMesh(mapMaker.MapData  mapData){
					requestedMesh = true;
					MapGen.RequsetMesh(mapData,LOD,onMeshRetruned);
					
				}

				
			}
			[System.Serializable]
			public struct LODinfo{
					public int LOD;
					public float visableDstTreshhold;
					public bool useForCollider ;
				}
		}
	

