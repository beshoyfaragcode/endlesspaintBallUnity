using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
public float Mousesenc = 10;
float camX;
 float camY;
float camZ;
float X = 0;
public float  zSpred = 10;
public float  zFrecncy = 5;
Vector3 rotion ;
 Vector3 rotionFP ;
 Vector3 PrepositionFP;
Vector3 positionFP;
public Transform target;

public Transform targetFP;
public float distancefromtraget = 2 ;
public float  targetSpred= 2;
public float  targetFrecncy = 0.015625f;
public Vector2 XMinMax = new Vector2(-40,85) ;
public Vector2 YMinMax = new Vector2(0,50) ;
public Vector2 YMinMaxFP = new Vector2(11.9f,60.43f) ;
public float rotSmoothTime = 1.5015625f;
 Vector3 rotSmoothVal;

 Vector3 rotNow;
 Vector3 rotNowFP;
public bool lockmouse;
 Vector3 posNow;
Vector3 prePos;
 Vector3 Pos;
public float posSmoothTime = 1.5015625f;
 Vector3 posSmoothVal;
 Vector2 ClampX;
 public Vector2 clampXOffSet = new Vector2(- 3, 2);
 Vector2 ClampY;
 public Vector2 clampYOffSet  = new Vector2(- 1.04f,1.47f);
 Vector2 ClampZ;
  public Vector2 clampZOffSet  = new Vector2( - 9.58f,- 3);
public bool thridPersonCam;
public bool fristPersonCam;
public Transform charcater;

 void Start()
{
    resetCamera ();
	if(lockmouse){

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}

	// Update is called once per frame
	void Update () {
		playerInput();
		setCamType();


		
	}
	void playerInput (){
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (thridPersonCam)
			{
                resetCamera ();
				thridPersonCam = false ;
				fristPersonCam = true;
			}else if (fristPersonCam){
                resetCamera ();
				fristPersonCam = false;
				thridPersonCam = true ;
			}
		}else if ((thridPersonCam == false  && fristPersonCam == false) || (thridPersonCam == true && fristPersonCam == true)){
            resetCamera ();
			thridPersonCam = false ;
			fristPerson();
		}
	}
	void thridPerson (){
		fristPersonCam = false;
		thridPersonCam = true ;
		ClampY.x =  target.position.y + clampYOffSet.x ;
		ClampY.y =  target.position.y + clampYOffSet.y ;
	    ClampZ.x =  target.position.z + clampZOffSet.x;
		ClampZ.y =  target.position.z + clampZOffSet.y ;
		ClampX.x = target.position.x + clampXOffSet.x ;
		ClampX.y = target.position.y + clampXOffSet.y;
		camX += Input.GetAxis("Mouse X") * Mousesenc;
		camX = Mathf.Clamp(camX,XMinMax.x,XMinMax.y);
		camY -= Input.GetAxis("Mouse Y") * Mousesenc;
		camY = Mathf.Clamp(camY,YMinMax.x,YMinMax.y);
    
		camZ = Mathf.Sin(X * zFrecncy) * zSpred  ;
		distancefromtraget = Mathf.Sin(X * targetFrecncy) * targetSpred ;
		distancefromtraget = Mathf.Abs(distancefromtraget) + 1.5015625f;
        posNow.x =Mathf.Sin(X * Time.smoothDeltaTime) * distancefromtraget;
		posNow.y = Mathf.Cos(X * Time.smoothDeltaTime) * distancefromtraget;
		posNow.z =  Mathf.Tan(X * Time.smoothDeltaTime) * distancefromtraget;
		


		X++;
		rotion = new Vector3 (camX,camY,camZ);
		rotNow = Vector3.SmoothDamp(rotNow,rotion,ref rotSmoothVal,rotSmoothTime);
		transform.eulerAngles = rotNow;
		//prePos = target.position - posNow;
		prePos.x = Mathf.Clamp (Pos.x,ClampX.x ,ClampX.y );
		prePos.y = Mathf.Clamp (Pos.y,ClampY.x ,ClampY.y );
		prePos.z = Mathf.Clamp (Pos.z, ClampZ.x,ClampZ.y );
		
		Pos = Vector3.SmoothDamp(Pos,prePos,ref posSmoothVal,posSmoothTime);
		
		transform.position = Pos;

	}
	void fristPerson (){
		fristPersonCam = true;
		thridPersonCam = false ;
		camX += Input.GetAxis("Mouse X") * Mousesenc;
		camX = Mathf.Clamp(camX,XMinMax.x,XMinMax.y);
		
		camY -= Input.GetAxis("Mouse Y") * Mousesenc;
		camY = Mathf.Clamp(camY,YMinMaxFP.x,YMinMaxFP.y);
		camY += targetFP.rotation.eulerAngles.y;
		camZ = targetFP.rotation.z;
		rotionFP = new Vector3 (camX,camY,camZ);
		positionFP = targetFP.position;
	


		transform.position = positionFP;
		
		transform.eulerAngles = rotionFP ;


	}
	void setCamType(){
		
		if(thridPersonCam){
			fristPersonCam = false;
			thridPerson();
		}else if (fristPersonCam)
		{ 	thridPersonCam = false ;
			fristPerson();
		}
		
	}
    void resetCamera (){
        transform.position = Vector3.zero ;
        transform.rotation = Quaternion.Euler(Vector3.zero);

    }
}
