using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class probability : MonoBehaviour {

    public static bool CheckProb(float prob)
    {
        int x = Mathf.RoundToInt(prob * 100);
        int check = Random.Range(0, 100);
        if (check <= x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
