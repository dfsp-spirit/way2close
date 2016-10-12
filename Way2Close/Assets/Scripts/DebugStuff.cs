using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugStuff : MonoBehaviour {

    public Text text;
    bool debugOn = true;

    void SetDebug(bool state)
    {
        debugOn = state;
    }
	
    void Update()
    {
        if (debugOn)
        {
            ShowMousePosition();
        }
    }

	void ShowMousePosition() {
        text.text = "scr:" + Input.mousePosition.ToString("n3") + " / viewp:" + Camera.main.ScreenToViewportPoint(Input.mousePosition).ToString("n3") + " / wrld:" + Camera.main.ScreenToWorldPoint(Input.mousePosition).ToString("n3");
    }
}
