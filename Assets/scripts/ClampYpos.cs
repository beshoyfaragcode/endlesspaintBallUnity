using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampYpos : MonoBehaviour {

    public float minYPos;
    public float retrunYPos;
    public float OffSetRange;
    Vector3 pos;
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < minYPos)
        {
            pos = Vector3.zero;
            pos.y = retrunYPos;
            float OffsetStyle =  Random.Range(0.0f, 4.0f);

            if (OffsetStyle >= 0) {
                pos.x = transform.position.x + Random.Range(-OffSetRange, OffSetRange);
                pos.z = transform.position.z + Random.Range(-OffSetRange, OffSetRange);
                transform.position = pos;
            }
            else if (OffsetStyle >= 2)
            {
                pos.x = transform.position.x - Random.Range(-OffSetRange, OffSetRange);
                pos.z = transform.position.z - Random.Range(-OffSetRange, OffSetRange);
                transform.position = pos * Random.Range(-OffSetRange, OffSetRange);
            }
            else if (OffsetStyle >= 3)
            {
                pos.x = transform.position.x  * Random.Range(-OffSetRange, OffSetRange);
                pos.z = transform.position.z * Random.Range(-OffSetRange, OffSetRange);
                transform.position = pos / Random.Range(-OffSetRange, OffSetRange);
            }
            else if (OffsetStyle >= 4)
            {
                pos.x = transform.position.x / Random.Range(-OffSetRange, OffSetRange);
                pos.z = transform.position.z / Random.Range(-OffSetRange, OffSetRange);
                transform.position = pos * Random.Range(-OffSetRange, OffSetRange);
            }




            transform.position = pos;
        }
	}
}
