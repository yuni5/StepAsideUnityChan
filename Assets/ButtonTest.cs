using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour {
	private Animator myAnimator;
	// Use this for initialization
	void Start () {
		this.myAnimator = GetComponent<Animator> ();
		this.myAnimator.SetFloat ("Speed", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		bool button = GUI.Button(new Rect(20, 300, 100, 50), "Button");
		if (button == true)
			this.myAnimator.SetBool("Jump", true);
	}
}
