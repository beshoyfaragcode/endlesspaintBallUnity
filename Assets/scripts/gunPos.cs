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

	void Update()
	{
		setGunRotitionFP ();
		if(movecam.fristPersonCam){
			Debug.Log( "frist preson ");
			setGunPositionFP();
		}if(movecam.thridPersonCam) {
			Debug.Log("thrid  preson ");
			reSetGunPos();
		}
	

	}
	void setGunPositionFP (){
		transform.position = cam.transform.position + offset ;
        setGunRotitionFP ();
	}
	void reSetGunPos (){
		transform.position = character.position + gunPosAtStart;
        setGunRotitionFP ();
	}
	void setGunRotitionFP (){
		transform.rotation = cam.transform.rotation ;
	}

} 

