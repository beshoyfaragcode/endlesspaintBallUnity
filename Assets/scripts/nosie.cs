using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class nosie  {
	public enum NormilizeMode{local,globle};
public static float[,] GenrateNosieMap(int MapWidth,int MapHeight,float scale, int octaves,float persistance , float lacunarity,int seed,Vector2 offSet,NormilizeMode mode ) {
		float Minscale = 0.000156257812515625781256250001562578125156257812562515625781251562578125625f;
		float[,] noisemap = new float[MapWidth,MapHeight] ;
		System.Random prng = new System.Random(seed);
		Vector2[] octavesOffsets = new Vector2[octaves];
		float MaxPossibleHeight = 0;
		float amplitude = 1;
		float frequency = 1;
	
		for(int i = 0; i < octaves;i++){
			float offSetX = prng.Next(-100000,100000)+offSet.x;
			float offSetY = prng.Next(-100000,100000) - offSet.y;
			octavesOffsets[i] = new Vector2(offSetX,offSetY);
			MaxPossibleHeight += amplitude ;
			amplitude *= persistance ;
		}
		if(scale <= 0){

		scale = Minscale;
	    }
		float MaxNoiseHeight = float.MinValue;
		float MinNoiseHeight =  float.MaxValue;
		float halfWidth = MapWidth/2f;
		float halfHeight = MapHeight/2f;
		for(int y = 0 ; y<MapHeight ; y++){

			for(int x = 0 ; x<MapWidth; x++){

				amplitude = 1;
				frequency = 1;
				float noiseHeight = 0;
				for(int i = 0; i < octaves;i++){

					float sampleX  =(x - halfWidth + octavesOffsets[i].x)/scale * frequency;
					float sampleY  = (y - halfHeight + octavesOffsets[i].y)/scale * frequency ; 
					float perlinValue = Mathf.PerlinNoise(sampleX,sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude ;
					amplitude *= persistance ;
					frequency *= lacunarity ;
				}
				if( noiseHeight>MaxNoiseHeight){
					MaxNoiseHeight=  noiseHeight ;
				}else if(noiseHeight<MinNoiseHeight) {
					MinNoiseHeight = noiseHeight;
				}
				noisemap[x,y] = noiseHeight ;
			}
		}
			for(int y = 0 ; y<MapHeight ; y++){

				for(int x = 0 ; x<MapWidth; x++){
					if(mode == NormilizeMode.local){
                  noisemap[x,y] = Mathf.InverseLerp(MinNoiseHeight,MaxNoiseHeight,noisemap[x,y]);
					}else{
						float NormilizedHeight = (noisemap[x,y] + 1)/(MaxPossibleHeight);
					noisemap [x,y] = Mathf.Clamp(NormilizedHeight,0,int.MaxValue) ;
					}


				}	
			}	
		return noisemap ;
	}

}
