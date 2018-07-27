using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShow : MonoBehaviour {

public Renderer plane;
public MeshFilter meshFilter;
public MeshRenderer Meshrenderer;

public void DrawTexture(Texture2D Texture ){
	
	plane.sharedMaterial.mainTexture = Texture ;
	plane.transform.localScale = new Vector3 (Texture.width,1,Texture.height);
}
public void DrawMesh(MeshData meshData,Texture2D Texture){
	meshFilter.sharedMesh = meshData.CreateMesh (); 
	Meshrenderer.sharedMaterial.mainTexture = Texture ;
}
}
