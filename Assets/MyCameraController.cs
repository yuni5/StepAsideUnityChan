using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {

	//unitychanのオブジェクトをこの変数に代入する
	private GameObject unitychan;

	//unitychanとカメラの距離(差:difference)
	private float difference;

	// Use this for initialization
	void Start () {

		//unitychanのオブジェクトを取得して指定する
		this.unitychan = GameObject.Find ("unitychan");

		//Unityちゃんとカメラの位置（z座標）の差を求める
		/*unitychanがカメラよりも基点から離れているため
		 基点からunitychanまでの距離ー基点からカメラまでの距離*/
		this.difference = unitychan.transform.position.z - this.transform.position.z;
		
	}
	
	// Update is called once per frame
	void Update () {
		// オブジェクト（カメラ）の位置(transform.position)
		// unitychanの現在のz軸の位置からunitychanからカメラまでの距離(difference)を引く
		this.transform.position = new Vector3 (0, this.transform.position.y, this.unitychan.transform.position.z - difference);

	}
}
