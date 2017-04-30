using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

	private Animator myAnimator;

	private Rigidbody myRigidbody;

	// 全進する(forward)ための力(Force)を追加
	private float forwardForce = 800.0f;

	//左右に移動するための力
	private float turnForce = 500.0f;

	//左右の移動できる範囲
	private float movableRange = 3.4f;

	//ジャンプするための力
	private float upForce = 500.0f;

	//動きを原則させる係数
	private float coefficient = 0.95f;

	//ゲーム終了判定
	private bool isEnd = false;

	//ゲーム終了時に表示するテキスト
	private GameObject stateText;

	//スコアを表示するテキスト
	private GameObject scoreText;

	//得点
	private int score = 0;

	//左ボタン押下の判定
	private bool isLButtonDown = false;

	//右ボタン押下の判定
	private bool isRButtonDown = false;

	// Use this for initialization
	void Start () {

		//Animatorを代入
		this.myAnimator = GetComponent<Animator> ();
		//AnimatorにSetFloat(floatの値)Speedと値を代入
		this.myAnimator.SetFloat ("Speed", 1);

		//Rigidbodyを代入
		this.myRigidbody = GetComponent<Rigidbody> ();

		//シーン中のstateTextオブジェクトを取得
		this.stateText = GameObject.Find("GameResultText");

		//シーン中のstateTextオブジェクトを取得
		this.scoreText = GameObject.Find("ScoreText");
	}
	
	// Update is called once per frame
	//フレーム単位で実行する
	void Update () {
		
		
		//ゲーム終了ならunitychanの動きを減速する
		//毎フレーム値が減少し、止まる
		if (this.isEnd) {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAnimator.speed *= this.coefficient;
		}

		//Unityちゃんに前方向の力を加える
		//(Rigidbody.AddForce)力を加える
		// (transform.forward)ゼット軸方向に(forwardForce)800.0fの力を加えて全進させる
		this.myRigidbody.AddForce (this.transform.forward * this.forwardForce);

		//Unityちゃんを矢印キーまたはボタン（if文の条件の中身）に応じて左右に移動させる
		//GetKeyは押している間中ずっと
		if((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && - this.movableRange < this.transform.position.x){

			//左(-x軸)に移動
			this.myRigidbody.AddForce(-this.turnForce,0,0);

		}else if((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown)&& this.transform.position.x < this.movableRange){
			//右(x軸)に移動
			this.myRigidbody.AddForce (this.turnForce, 0, 0);
		}

		//Jumpステートの場合はJumpにfalseをセットする
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			this.myAnimator.SetBool ("Jump", false);
		}

		/*ジャンプしていない時にスペースが押されたらジャンプする
		  GetKeyDownは押した瞬間だけ値を返す
		  多段ジャンプを防ぐために地面付近(y < 0.5f)の時だけ*/
		if (Input.GetKeyDown (KeyCode.Space) && this.transform.position.y < 0.5f) {
			
			//ジャンプアニメを再生
			this.myAnimator.SetBool ("Jump", true);

			//Unityちゃんに上方向(transform.up)に500.0f(upForce)の力を加える
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}

	}

	void OnTriggerEnter(Collider other){

		//障害物に衝突した場合
		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
			
			this.isEnd = true;
			//stateTextにGAME OVERを表示
			this.stateText.GetComponent<Text>().text = "GAME OVER";
		}

		//ゴール地点に到達した場合
		if (other.gameObject.tag == "GoalTag") {
			this.isEnd = true;
			//stateTextにGAME CLEARを表示
			this.stateText.GetComponent<Text> ().text = "CLEAR!!";
		}

		//コインに接触した場合
		if (other.gameObject.tag == "CoinTag") {

			//スコアを加算
			this.score += 10;

			//scoreText獲得した点数を表示
			this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

			//パーティクルを再生
			GetComponent<ParticleSystem>().Play();

			//接触したコインのオブジェクトを破棄
			Destroy(other.gameObject);
		}
	}

	//ジャンプボタンを押した場合の処理
	public void GetMyJumpButtonDown(){

		if (this.transform.position.y < 0.5f) {

			this.myAnimator.SetBool ("Jump", true);
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	//左ボタンを押し続けた場合の処理
	public void GetMyLeftButtonDown() {
		
		this.isLButtonDown = true;
	}
	//左ボタンを離した場合の処理
	public void GetMyLeftButtonUp() {
		
		this.isLButtonDown = false;
	}

	//右ボタンを押し続けた場合の処理
	public void GetMyRightButtonDown() {
		
		this.isRButtonDown = true;
	}
	//右ボタンを離した場合の処理
	public void GetMyRightButtonUp() {
		
		this.isRButtonDown = false;
	}



}
