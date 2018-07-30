using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_controller : MonoBehaviour {
	public float walkspeed = 2;
	public float runspeed = 6;
	public Animator animator ;
	public static Vector2 input;
	public static Vector2 inputDir;
	public bool ruuning;
	float target_speed;
	float anmationspeedprect;
	public float trunsmooth = 0.2f;
	float target   ;
	float trunvalsity ;
	public float speedsmooth = 0.1f;
	float speedvalsity;
	float speednow ;
	public Transform cameraT;
    public CameraMove camera;
    float X;
	float smoothRunSpeed;
	float smoothWalkSpeed;
	float smoothRunAn;
	float smoothWalkAn ;
	public float RunAn = 1;
	public float WalkAn = 0.5f;
	public float gravity = -9.8f;
	float valY;
	public CharacterController controller;
	Vector3 val;
	float jumpHeight;
	float jumpVal;
	float acceleration;
	Vector3  valStart;
	Vector3  valEnd;
	Vector3  valAvg;
	public float minJump;

	

	// Use this for initialization
	void Start () {
		controller.GetComponent<CharacterController>();
		animator.GetComponent<Animator>();
		//cameraT = Camera.main.transform ;
		
	}
	
	// Update is called once per frame
	void Update () {
       

		if (Input.GetKey(KeyCode.Escape)){
			Debug.Break();
		}
     valStart = controller.velocity;
		
	 input = new Vector2 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
     inputDir = input;
	
	if(Input.GetKeyDown(KeyCode.Space)){
		jump();
	}
	if (inputDir != Vector2.zero){
            
            
            target =  Mathf.Atan2(inputDir.x,inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            /*
		 if(target < 0){
        target = 360 + target ;


		}
        */

            if (camera.fristPersonCam)
            {
                target = cameraT.rotation.eulerAngles.y + target;
            }
            transform.eulerAngles = Vector3.up * Mathf.SmoothDamp(transform.eulerAngles.y,target, ref trunvalsity, trunsmooth)  ;
	}
		
	smoothRunSpeed = Mathf.Abs(Mathf.Sin(X * Time.smoothDeltaTime) * runspeed)  ;
	smoothWalkSpeed = Mathf.Abs(Mathf.Sin(X * Time.smoothDeltaTime) * walkspeed ) ;
	ruuning = Input.GetKey(KeyCode.LeftShift);
	target_speed =( (ruuning)?smoothRunSpeed : smoothWalkSpeed ) * inputDir.magnitude;
	speednow = Mathf.SmoothDamp(speednow,target_speed,ref speedvalsity,speedsmooth);
    valY += Time.smoothDeltaTime* gravity ;
	val = transform.forward * speednow + Vector3.up * valY;
	controller.Move(val * Time.smoothDeltaTime);
	valEnd = controller.velocity;
	speednow = new Vector2 (controller.velocity.x,controller.velocity.y).magnitude;
	valAvg = (valStart  + valEnd )/2;
	acceleration = (valEnd - valStart).magnitude / Time.smoothDeltaTime ;
	if(controller.isGrounded){

		valY = 0;
	} 
	
		RunAn = speednow/runspeed ;
		WalkAn =speednow/walkspeed *.5f;
		smoothRunAn = Mathf.Abs(Mathf.Sin(X * Time.smoothDeltaTime) * RunAn)  ;
		smoothWalkAn = Mathf.Abs(Mathf.Sin(X * Time.smoothDeltaTime) * WalkAn) ;
		
		X++;
	
      anmationspeedprect = ( (ruuning)? smoothRunAn : smoothWalkAn ) * inputDir.magnitude;

	animator.SetFloat("speed %",anmationspeedprect,speedsmooth,Time.smoothDeltaTime);
	}
   public void jump(){
Debug.Log("jumping");
if(controller.isGrounded){
	jumpHeight = Mathf.Clamp(Mathf.Pow(valAvg.magnitude,2)/4.9f,minJump,Mathf.Infinity);

	jumpVal = Mathf.Sqrt(-2*gravity*jumpHeight);
	
	valY = jumpVal;
}
   }
}
