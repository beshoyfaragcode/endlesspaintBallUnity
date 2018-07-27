using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof( mapMaker))]
public class MapMakerEditer : Editor {

	public override void OnInspectorGUI(){

		mapMaker MapGen = (mapMaker) target;
		if(DrawDefaultInspector ()){
			if(MapGen.AtuoUpadate){
				MapGen. DrawMap();
			}
		}
		if(GUILayout.Button("Generate")){
			MapGen. DrawMap();
		}
	}
	
}
