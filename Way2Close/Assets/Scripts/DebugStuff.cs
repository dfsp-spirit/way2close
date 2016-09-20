using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugStuff : MonoBehaviour {

    public Text text;
	
    //void Update()
    //{
    //    ShowMousePosition();
    //}

	void ShowMousePosition() {
        text.text = "s:" + Input.mousePosition + " / s2v:" + Camera.main.ScreenToViewportPoint(Input.mousePosition) + " / w2v:" + Camera.main.WorldToViewportPoint(Input.mousePosition);	
	}
}
