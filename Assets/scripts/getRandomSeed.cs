using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getRandomSeed : MonoBehaviour {


	// Use this for initialization
public static long GetSeed (){
	int seed = Random.Range(int.MinValue,int.MaxValue);
	return seed ;
}
	
	
}
