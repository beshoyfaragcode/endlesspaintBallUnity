using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class decalepool : MonoBehaviour {
	
	
	
	public int max = 100 ;
	public float minSize ;
	public float maxSize ;
	public Transform shotFrom;

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
		data[Index].angleVal = (data[Index].rot - shotFrom.rotation.eulerAngles)/Time.smoothDeltaTime;
		data[Index].axis = hit.normal;
		// collider val = (mass1 X val1(shoot val ) )/mass1(shot) + mass2(land) 
		//shot val used change to collider val
		data[Index].val = 	(data[Index].pos - shotFrom.position )/Time.smoothDeltaTime;
		data[Index].seed = (uint)getRandomSeed.GetSeed();
		



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
			decales[i].axisOfRotation = data[i].axis;
			decales[i].angularVelocity3D = data[i].angleVal;
			decales[i].velocity = data[i].val;
			decales[i].randomSeed = data[i].seed;
		}
		splatterDecales.SetParticles(decales,decales.Length);
		Debug.Log("Decales are set");
	}
	
	

 	

	
	
}
