using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public  class meshMaker {
  

public static MeshData GenerateTerrainMesh(float[,] HeightMap,float HeightMultiplier,AnimationCurve _HeightCurve,int LevelOfDetail ){
        
        
        int simpleIncrent = (LevelOfDetail == 0)? 1:LevelOfDetail*2;
	AnimationCurve HeightCurve = new AnimationCurve(_HeightCurve.keys);
	int brordedWidth = HeightMap.GetLength(0);
	int meshSize = brordedWidth - 2*simpleIncrent;
	int meshSizeunSimple = brordedWidth - 2 ;
	float TopLeftX = ( meshSizeunSimple-1)/-2f;
	float TopLeftZ = ( meshSizeunSimple-1)/2f;
	
	int verticesPerLine = (meshSize-1)/simpleIncrent + 1;
	MeshData meshData = new MeshData (verticesPerLine);
	 int[,] vertexInices = new int[brordedWidth,brordedWidth] ;
	int meshvertexIndex = 0 ;
	int BordervertexIndex = - 1 ;
	int vertexIndex  = 0;

	for (int y = 0; y < brordedWidth; y += simpleIncrent){
			for (int x = 0; x < brordedWidth; x  += simpleIncrent){
				bool isBordervertex = y == 0 || y == brordedWidth - 1 || x == 0 || x == brordedWidth - 1   ;
				if(isBordervertex){
					vertexInices[x,y] = BordervertexIndex ;
					BordervertexIndex -- ;
				}else{
					vertexInices[x,y] = meshvertexIndex ;
					meshvertexIndex++;
				}
		}
	}
	for (int y = 0; y < brordedWidth; y += simpleIncrent){
			for (int x = 0; x < brordedWidth; x  += simpleIncrent)
		{
			int VertexIndex = vertexInices[x,y];
			Vector2 precent  = new Vector2((x-simpleIncrent)/(float)meshSize,(y-simpleIncrent)/(float)meshSize);
			float height = HeightCurve.Evaluate(HeightMap[x,y]) * HeightMultiplier;
			Vector3 vertexPos = new Vector3(TopLeftX + precent.x *  meshSizeunSimple,height,TopLeftZ-precent.y * meshSizeunSimple);
			meshData.addvertex(vertexPos,precent,VertexIndex);
			
			if(x<brordedWidth-1 && y< brordedWidth -1 ){
				int a = vertexInices[x,y];
				int b = vertexInices[x + simpleIncrent,y];
				int c = vertexInices[x,y + simpleIncrent];
				int d = vertexInices[x + simpleIncrent,y + simpleIncrent];
                meshData.AddTriangle(a,d,c);
				meshData.AddTriangle(d,a,b);
			}
			vertexIndex ++;
		}
	}
	meshData.Bake();

	return meshData ;
}
}
public class MeshData{
	public Vector3[] vertices ;
	public int[] triangles ;
	int trianglesIndex;
	public Vector2[] uvs ;
	public Vector3[] Bordervertices;
	public Vector3[] BakedNormals ;
	public int[] brordedTriangles;
	int brordedTrianglesIndex;
    

    public MeshData(int verticesPerLine){

		vertices = new Vector3[verticesPerLine * verticesPerLine ];
		uvs = new Vector2[verticesPerLine*verticesPerLine ];
		triangles = new int[(verticesPerLine-1)*(verticesPerLine -1)*6];
		Bordervertices = new Vector3[verticesPerLine * 4 + 4];
		brordedTriangles =  new int[24 * verticesPerLine];
		



	}
	public void addvertex (Vector3 pos ,Vector2 uv, int index ){
		if(index < 0 ){
			Bordervertices[-index - 1] =  pos;
		}else {
			vertices [index] = pos ;
			uvs[index] = uv ;
		}
	
	}
	public void AddTriangle (int a,int b, int c){
		if(a < 0||b <0 || c < 0){
			brordedTriangles[brordedTrianglesIndex] = a ;
			brordedTriangles[brordedTrianglesIndex + 1 ] = b ;
			brordedTriangles[brordedTrianglesIndex + 2 ] = c ;
			brordedTrianglesIndex += 3 ;
		}else{
			triangles[trianglesIndex] = a ;
			triangles[trianglesIndex + 1 ] = b ;
			triangles[trianglesIndex + 2 ] = c ;
			trianglesIndex += 3 ;
		}
	}
	Vector3[] calculateNormals (){
		Vector3 [] vertexNormals = new Vector3[vertices.Length];
		int trianglesCount = triangles.Length / 3;
		for (int i = 0; i < trianglesCount; i++){
			int triIndex = i * 3;
			int vertexIndexA = triangles[triIndex] ;
			int vertexIndexB = triangles[triIndex + 1] ;	
			int vertexIndexC = triangles[triIndex+ 2 ] ;
			Vector3 triNormals = surfaceNormals( vertexIndexA ,vertexIndexB,vertexIndexC);
			vertexNormals[vertexIndexA ] += triNormals ;
			vertexNormals[vertexIndexB ] += triNormals ;
			vertexNormals[vertexIndexC ] += triNormals ;		
			}

			int brodertrianglesCount = brordedTriangles.Length / 3;
		for (int i = 0; i < brodertrianglesCount; i++){
			int triIndex = i * 3;
			int vertexIndexA = brordedTriangles[triIndex] ;
			int vertexIndexB = brordedTriangles[triIndex + 1] ;	
			int vertexIndexC = brordedTriangles[triIndex+ 2 ] ;
			Vector3 triNormals = surfaceNormals( vertexIndexA ,vertexIndexB,vertexIndexC);
			if(vertexIndexA >=0 ){
				vertexNormals[vertexIndexA ] += triNormals ;
			}
			if(vertexIndexB >=0 ){
				vertexNormals[vertexIndexB ] += triNormals ;
			}
			if(vertexIndexC >=0 ){
				
				vertexNormals[vertexIndexC ] += triNormals ;	
			}

				
			}

			for (int i = 0; i < vertexNormals.Length; i++){
				vertexNormals[i].Normalize () ;
			}
			return vertexNormals;
	}
	Vector3 surfaceNormals ( int IndexA ,int IndexB,int IndexC){
		Vector3 PointA = (IndexA < 0 )?Bordervertices[ -IndexA-1]:vertices[ IndexA];
		Vector3 PointB =(IndexB < 0 )?Bordervertices[ -IndexB-1]: vertices[ IndexB];
		Vector3 PointC = (IndexC < 0 )?Bordervertices[ -IndexC-1]:vertices[ IndexC];
		Vector3 sideAB = PointB - PointA ;
		Vector3 sideAC = PointC - PointA ;
		return Vector3.Cross(sideAB,sideAC).normalized;
	}
		public void Bake(){
			BakedNormals = calculateNormals();
		}
	public Mesh CreateMesh (){
        
		Mesh mesh = new Mesh();
        mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs ;
		mesh.normals = BakedNormals;
		mesh.RecalculateTangents();
		mesh.RecalculateBounds();
       
        
        return mesh ;


	} 
    
}
