using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luncher : MonoBehaviour {
	public ParticleSystem Mainluncher;
	public ParticleSystem splatter;
	public List <ParticleCollisionEvent> hits ;
	public Gradient colorsGradient;
	public mapMaker Map;
	public decalepool splatterDecales;
	
	
	
	

	// Use this for initialization
	void Start () {
		 colorsGradient =  GetLevelsGradient.GetLevelsGradientFromMap(Map);
		
		hits = new  List <ParticleCollisionEvent> ();
	}
	
	// Update is called once per frame
	void OnParticleCollision(GameObject other){
		ParticlePhysicsExtensions.GetCollisionEvents(Mainluncher,other,hits);
		for (int i = 0; i < hits.Count; i++){
			Debug.Log(" hit " + i);
			splatterDecales.shot(hits[i], colorsGradient);
			Debug.Log(" Decale " + i);
			 EmitAtPos(hits[i]);
			 Debug.Log(" splater " + i);
			 

		}
		
	}
	void EmitAtPos(ParticleCollisionEvent hit){
		splatter.transform.position = hit.intersection;
		splatter.transform.rotation = Quaternion.LookRotation(hit.normal);
		ParticleSystem.MainModule splatterMain;
		splatterMain = splatter.main ;
		splatterMain.startColor = colorsGradient.Evaluate(Random.Range(0f,1.1f));
		splatter.Emit(1);
	}
	void Update () {
		
		if(Input.GetButton("Fire1")){
			ParticleSystem.MainModule main;
			ParticleSystem.TrailModule trails ;
			main = Mainluncher.main ;
			trails = Mainluncher.trails ;
			main.startColor = colorsGradient.Evaluate(Random.Range(0f,1.1f));
			trails.colorOverTrail = colorsGradient.Evaluate(Random.Range(0f,1.1f));
			trails.colorOverLifetime = colorsGradient.Evaluate(Random.Range(0f,1.1f));
			trails.lifetime = Random.Range(0f,2.0f) ;
			Mainluncher.Emit(1);
			Debug.Log("one shot");
		}
		
	}
	
}
