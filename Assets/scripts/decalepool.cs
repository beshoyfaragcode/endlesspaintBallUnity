using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decalepool : MonoBehaviour {
	
	
	
	public int max = 100 ;
	public float minSize ;
	public float maxSize ;

	private int Index ;
	public ParticleSystem splatterDecales ;
	decaleData[] data;
	ParticleSystem.Particle [] decales;

	


	// Use this for initialization
	void Start () {
		
		data = new decaleData[max] ;
		decales = new ParticleSystem.Particle[max];
		for (int i = 0; i < max; i++)
		{
			data[i] = new decaleData();
		}
		
		
	}
	public void shot (ParticleCollisionEvent hit , Gradient colors){
		Debug.Log(" shot is called ");
		SetDecaleData(hit,colors);
		
		display();
	}
	void SetDecaleData (ParticleCollisionEvent hit , Gradient colors){
		if(Index >= max){
			Index = 0 ;
		}
		data[Index].pos = hit.intersection ;
		Vector3 rotion  = Quaternion.LookRotation(hit.normal).eulerAngles;
		rotion.y = Random.Range(0.0f,360.0f);
		data[Index].rot = rotion;
		data[Index].color = colors.Evaluate(Random.Range(0f,1.0f));
		data[Index].size = Random.Range(minSize,maxSize);
		



		Index++;

	}
	void display (){
		Debug.Log("display is called ");
		for (int i = 0; i < data.Length; i++)
		{
			decales[i].position = data[i].pos;
			decales[i].rotation3D = data[i].rot;
			decales[i].startColor = data[i].color;
			decales[i].startSize = data[i].size;
		}
		splatterDecales.SetParticles(decales,decales.Length);
		Debug.Log("Decales are set");
	}
	
	

 	

	
	
}
