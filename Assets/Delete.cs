using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// 画面外に出た時の処理
	void OnBecameInvisible(){
		Destroy(this.gameObject);
	}
}
