using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameMenuOpener : MonoBehaviour
{
    public IngameMenuUI ui;

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            ui.gameObject.SetActive(!ui.gameObject.activeSelf);
        }
	}
}
