using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSky : MonoBehaviour {
    Material material;
    Shader sky;
    public mapMaker map;
    public luncher luncher ;
    public Gradient colors ;



	// Use this for initialization
	void Start () {
        setUp();
	    changeSkyBoxMat();
        SetSky (material);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void setUp () {
        sky = Shader.Find("Skybox/Procedural");
        material = new Material(sky);
        colors = luncher.GetLevelsGradient(map);
    }
    void changeSkyBoxMat()
    {
            Debug.Log(material.shader.name+ " is a skybox of type Skybox/Procedural ");
              material.SetColor("_SunTint", colors.Evaluate(Random.Range(0.0f,1.1f)));
              material.SetColor("_GroundColor",colors.Evaluate(Random.Range(0.0f,1.1f)));
              material.SetFloat("_SunSize" ,Random.Range(0.044f,1.1f));
              material.SetFloat("_SunSizeConvergence",Random.Range(1.0f,10.0f));
              material.SetFloat("_AtmosphereThickness",Random.Range(0.0f,5.0f));
              material.SetFloat(" _Exposure",Random.Range(0.044f,1.1f));
              


        
    }
    void SetSky (Material skybox ){
        Skybox sky = FindObjectOfType<Skybox>();
        sky.material = skybox;
    }
    

}
