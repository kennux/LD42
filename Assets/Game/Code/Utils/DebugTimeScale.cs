using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTimeScale : MonoBehaviour
{
    public float timeScale = 1f;
    
	// Update is called once per frame
	void Update () {
        Time.timeScale = this.timeScale;
	}
}
