using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterChangeColor : MonoBehaviour {
		
	public Gradient colors ;
	public mapMaker map;
	public luncher MapToColors ;
	//hide later 
	 SkinnedMeshRenderer characterMesh;
	 SkinnedMeshRenderer newCharacterMesh;
	 Material[] characterMats;
	 Material[] NewcharacterMats;
	 float[] randomFloats ;
	 Color32 [] randomColorsFromGradient ;
	MeshRenderer characterMeshRend;
	// to here

	// Use this for initialization
	void Start () {
		getCharacterInfo();
		randomFloats = getFloatsArray();
		checkFloatsArray(randomFloats);
		randomColorsFromGradient = GetColorFromGradient(colors,randomFloats);
		setMaterialColor(randomColorsFromGradient,characterMats); 
		if(characterMesh == null ){
			characterMeshRend.materials =  characterMats ;
		} 
		characterMesh.materials =  characterMats ;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void getCharacterInfo (){
	GameObject character = this.gameObject ;
		characterMesh = this.GetComponent<SkinnedMeshRenderer>();
		if(characterMesh == null ){
	    characterMeshRend = this.GetComponent<MeshRenderer>();
		}
		colors = MapToColors.GetLevelsGradient(map);
		if(characterMesh == null ){
			characterMats = characterMeshRend.materials ;
		} else {
				characterMats = characterMesh.materials ;
		}
	
	}
	public float[] getFloatsArray (){
	  float[] FloatsArray = new float[characterMats.Length];
		for (int i = 0; i < characterMats.Length; i++)
		{
			float x = Random.Range(0.0f , 1.0f);
			FloatsArray[i]  = x ;
		}
		return FloatsArray ;


	}

	public void  checkFloatsArray (float[] floatsToCheck ){
		for (int i = 0; i < floatsToCheck.Length; i++)
		{
			
			if(!(i == floatsToCheck.Length - 1)){
				float a = floatsToCheck[i];
				float b = floatsToCheck[i + 1];
				if(Mathf.RoundToInt(a) == Mathf.RoundToInt(b)){
					floatsToCheck[i] = Random.Range(0.0f , 1.0f);
					floatsToCheck[i + 1] = Random.Range(0.0f , 1.0f);
					recheckFloats(floatsToCheck);
				}

			} else {
				float a = floatsToCheck[i];
				if(Mathf.RoundToInt(a) == 0  ){
					floatsToCheck[i] = Random.Range(0.0f , 1.0f);
					recheckFloats(floatsToCheck);
				}
			}
			

			
			
		}
	}
	void recheckFloats (float[] floatsToRecheck) {
		for (int i = 0; i < floatsToRecheck.Length; i++)
		{
			
			if(!(i == floatsToRecheck.Length - 1)){
				float a = floatsToRecheck[i];
				float b = floatsToRecheck[i + 1];
				if(Mathf.RoundToInt(a) == Mathf.RoundToInt(b)){
					floatsToRecheck[i] = Random.Range(0.0f , 1.0f);
					floatsToRecheck[i + 1] = Random.Range(0.0f , 1.0f);
					recheckFloats(floatsToRecheck);
				}

			} else {
				float a = floatsToRecheck[i];
				if(Mathf.RoundToInt(a) == 0 ){
					floatsToRecheck[i] = Random.Range(0.0f , 1.0f);
					recheckFloats(floatsToRecheck);
				}
			}
			

			
			
		}

	}

	public Color32[] GetColorFromGradient (Gradient GetFrom , float[] postions ){
		Color32[] setTo  = new Color32[postions.Length];
		for (int i = 0; i < postions.Length; i++)
		{
			setTo[i] = GetFrom.Evaluate(postions[i]);
		}
		
		return setTo ;
	}
	 public void setMaterialColor (Color32 [] colorsForCharacters,Material[] characterMaterials  ){
		
		for (int i = 0; i < characterMaterials.Length; i++)
		{
			
			characterMaterials[i].SetColor("_EmissionColor",colorsForCharacters[i]);
		
			characterMaterials[i].color = colorsForCharacters[i];
		}

		
	}

	
}
