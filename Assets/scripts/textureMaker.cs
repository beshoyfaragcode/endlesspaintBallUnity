using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class textureMaker  {

public static Texture2D textureFromColorMap(Color32[] colorMap,int Width,int Height ) {
	
	Texture2D texture = new Texture2D(Width,Height);
	texture.filterMode = FilterMode.Point;
	texture.wrapMode = TextureWrapMode.Clamp;
	texture.SetPixels32(colorMap);
	texture.Apply();
	return texture;
}

public static Texture2D textureFromHeightMap(float[,] HeightMap) {

int width =  HeightMap.GetLength(0);
	int height =  HeightMap.GetLength(1);

	Color32[] colorMap = new Color32[width * height];
	for(int y = 0;y<height;y++){
		for(int x = 0;x<width;x++){
		colorMap[y * width + x] = Color32.Lerp(Color.black,Color.white, HeightMap[x,y]);
	}
	}
	
	return textureFromColorMap(colorMap,width,height);
}
}
