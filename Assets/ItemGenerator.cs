using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {

	public GameObject carPrefab;

	public GameObject coinPrefab;

	public GameObject conePrefab;

	//スタート地点
	public int startPos = -160;

	//ゴール地点
	private int goalPos = 120;

	//アイテムを出すx方向の範囲
	private float posRange = 3.4f;

	//unitychanのオブジェクト
	private GameObject unitychan;

	//unitychanの位置
	private float unitychanPosition;

	// Use this for initialization
	void Start () {
		//一定の距離(15)ごとにアイテムを生成
	for (int i = startPos; i < goalPos; i+=15){
			
			//アイテムをランダムに設定
			int num = Random.Range (0, 10);
			//値が0,1になった場合
			if (num <= 1) {
				//コーンをx軸方向に一直線に生成
				for (float j = -1; j <= 1; j += 0.4f) {
					//(Instantiate)オブジェクトのコピーを返す
					GameObject cone = Instantiate (conePrefab) as GameObject;
					cone.transform.position = new Vector3 (4 * j, cone.transform.position.y, i);
				}
			} else {

				//レーンごと(全3レーン)にアイテムを生成
				for (int j = -1; j < 2; j++) {
					//アイテムの種類を決める
					int item = Random.Range (1, 11);
					//アイテムを置くz座標のオフセットをランダムに設定
					int offsetZ = Random.Range (-5, 6);
					//60％コイン配置:30％車配置:10％何もなし
					//(60％)
					if (1 <= item && item <= 6) {
						//コインを生成
						GameObject coin = Instantiate (coinPrefab)as GameObject;
						coin.transform.position = new Vector3 (posRange * j, coin.transform.position.y, i + offsetZ);
					//(30％)
					} else if (7 <= item && item <= 9) {
						//車を生成
						GameObject car = Instantiate (carPrefab)as GameObject;
						car.transform.position = new Vector3 (posRange * j, car.transform.position.y, i + offsetZ);
					}
				}
			}

		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}





}
