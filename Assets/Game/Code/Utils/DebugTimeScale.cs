using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DebugTimeScale : MonoBehaviour
{
    public float timeScale = 1f;
    public GameObject mouseOverDebug;
    
	// Update is called once per frame
	void Update () {
        Time.timeScale = this.timeScale;
        this.mouseOverDebug = EventSystem.current.currentSelectedGameObject;
	}
}
