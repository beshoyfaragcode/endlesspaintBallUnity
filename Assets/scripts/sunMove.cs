using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunMove : MonoBehaviour {
	public float Minspeed = 0 ;
	public float Maxspeed = 20;
	
	public float speed ;
	public Vector2 center ;
	// Use this for initialization
	void Start () {
		 speed = UnityEngine.Random.Range(Minspeed, Maxspeed);
		 center = new Vector2(UnityEngine.Random.Range(Minspeed, Maxspeed),UnityEngine.Random.Range(Minspeed, Maxspeed));

	}
	
	// Update is called once per frame
	void Update () {
		

		transform.RotateAround(center,Vector3.right,speed * Time.smoothDeltaTime);
		transform.LookAt( center);
	}
}
