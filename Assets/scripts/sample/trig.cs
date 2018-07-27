using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trig : MonoBehaviour {
	public GameObject cube;
	 Vector3 pos;
	 float x;
	 float y;
	 float z ;
	public float ampute ;
	public float  frequency ;
	public bool UseSinForX;
	public bool UseCosForY;
	public bool UseTanForZ;
	Vector3 posVal ;
	Vector3 Nextpos;
	

	float value ;
	


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			
			
			
			if(UseSinForX){
				float x = Mathf.Sin(value * frequency) * ampute ;
				Debug.Log( "x is " + x);
				Nextpos.x = x;
			}
			if(UseCosForY){
				float y = Mathf.Cos(value *   frequency) * ampute ;
				Debug.Log( "y is " + y);
				Nextpos.y = y;
			}
			if(UseTanForZ){
				float z = Mathf.Tan(value *  frequency) * ampute ;
				Debug.Log( "z is " + z);
				Nextpos.z = z;
			}
			
			Debug.Log( "Nextpos is " + Nextpos);
			cube.transform.Translate (Nextpos * Time.smoothDeltaTime)  ;
			value ++ ;
		
		
	}
}
