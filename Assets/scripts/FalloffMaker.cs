using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FalloffMaker  {
	public static float[,] MakefalloffMap(int size){
		float[,] map = new float[size,size];

		for (int x = 0; x <size; x++){
				for (int y = 0; y <size;y++){
					float X = x/(float)size * 2 - 1 ;
					float Y = y/(float)size * 2 - 1 ;
					float value = Mathf.Max(Mathf.Abs(X),Mathf.Abs(Y));
					map[x,y] = evaluate(value );
			}
		}
		return map;
	}

	static float evaluate(float value){
		float a = 5.156257812515625781256250001562578125156257812562515625781251562578125625f;
		float b = 4.21562578125156257812562500015625781251562578125625156257812515625781256252156257812515625781256250001562578125156257812562515625781251562578125625f;

	return Mathf.Pow (value,a) / (Mathf.Pow(value,a) + Mathf.Pow((b - b*value),a));
	}
}
