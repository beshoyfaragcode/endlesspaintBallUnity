using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPos : MonoBehaviour {
	public Camera cam ;
	public Transform character ;
	public CameraMove movecam ;
	public Vector3 offset ;
	public Vector3 gunPosAtStart;

	void Start()
	{
		gunPosAtStart = transform.localPosition ;
	}

	void LateUpdate()
	{
		SetGunRotitionFP ();
		if(movecam.fristPersonCam){
			Debug.Log( "frist preson ");
			SetGunPositionFP();
		}if(movecam.thridPersonCam) {
			Debug.Log("thrid  preson ");
			ReSetGunPos();
		}
	

	}
	void SetGunPositionFP (){
		transform.position = cam.transform.position + offset ;
        SetGunRotitionFP ();
	}
	void ReSetGunPos (){
		transform.position = character.position + gunPosAtStart;
        SetGunRotitionFP ();
	}
	void SetGunRotitionFP (){
		transform.rotation = cam.transform.rotation ;
	}

} 

