using UnityEngine;
using System.Collections;

public class HaniwaAI : MonoBehaviour {

	Vector3 pos;
	NavMeshAgent agent;
	Animator anim;
	AnimatorStateInfo animStateInfo;

	public float DestroyTime = 180; // GameObjectの消滅時間
	public float RoopTime = 0; // アニメーションのループ回数
	public float IdleAnimationTime = 2.11f; // Idleのアニメーション時間

	float agentToPatroldistance; // 目的地までの距離
	bool walkToIdle = false; // WalkからIdleへ遷移
	int test = 0; //(kuboi) テスト用にフレーム数える変数を置いてます
	// int dice;

	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		doPatrol ();
		Destroy (gameObject, DestroyTime);
	}

	// Update is called once per frame
	void Update () {
		
		// 目的地までの距離を毎フレーム取得する
		agentToPatroldistance = Vector3.Distance (this.agent.transform.position, pos);
		//Debug.Log (agentToPatroldistance);

		// Animatorのstate情報を毎フレーム取得する
		animStateInfo = anim.GetCurrentAnimatorStateInfo (0);

		// 距離がn未満かつwalkToIdleがtrueかの判別
		if ((agentToPatroldistance < 0.5) && walkToIdle) {
			// (kuboi) Idle時間が一定超過でIdleから抜けるようにしてみる(こうしないとwalkToIdleがFalseにならない気がした，必要なのかは分からない)
			// (kuboi) 定数は適当
			if ( test > 500 ) {
				walkToIdle = false; // (kuboi) これでこの上のif通らなくなる
				test = 0; // (kuboi) 単にfps見てるだけなんで0に戻しておく
				return;
			}
			// (kuboi) Idle時間をfps単位で数えてみる
			test++;
			
			agent.Stop ();

			// Idleのパラメータをtrueにする
			anim.SetBool ("Walk", false);
			anim.SetBool ("Idle", true);
		}

		// Idleのアニメーションをしているかの判別
		// (kuboi) ifじゃなくelse ifのほうが適切に思えた
		// (kuboi) walkToIdleがTrueからFalseになるようにすれば，の前提があればInvoke使わなくてもいけそう...じゃない？
		// (kuboi) 少なくともアニメーションが2つしかない現状においてはwalkToidleがFalseになった時点で歩き始めていいはず

		//if (animStateInfo.IsName ("Idle")) {
		else if (animStateInfo.IsName ("Idle")) {
			
			// roopTimeに応じてWalkのパラメータをTrueにする
			//Invoke ("setWalk", (RoopTime * IdleAnimationTime));
			// (kuboi) Idle状態を抜けてしまえば状態遷移自体を時間で管理する必要があるのか無いのか分からなかったのでとりあえずぶち込んでみた
			setWalk ();

			// Idleのアニメーションが終わるころにdoPatrol ()を呼び出す
			//Invoke ("doPatrol", (RsoopTime + 1) * IdleAnimationTime);
			// (kuboi) 上のsetWalkと同じ
			doPatrol ();
		}

		// 目的地からn離れているかの判別
		// n未満でIdleのパラメータをTrueにしてしまうため
		// (kuboi) ifじゃなくelse ifのほうが適切に思えた
		else if (agentToPatroldistance >= 0.5f) {
			walkToIdle = true;
		}

		// 未完成
		/*
		if (タッチ検知 == true) {
			ここの処理はジャンプのparametorをtrueにします．
		}
		*/
	}

	// Agentが向かう先をランダムに指定するメソッド
	public void doPatrol () {
		float x, y, z;

		// 今の位置と次の目的地までの距離の差がn未満なら繰り返す
		/*
		Vector2 start = new Vector2 (this.agent.transform.position.x, this.agent.transform.position.z);
		Vector2 end;
		do {
			x = Random.Range (-10.0f, 10.0f);
			y = transform.position.y;
			z = Random.Range (-10.0f, 10.0f);

			end = new Vector2 (x, z);
			distance = Vector2.Distance (start, end);
		} while (distance < 2.0f);
		*/
		x = Random.Range (-10.0f, 10.0f);
		y = transform.position.y;
		z = Random.Range (-10.0f, 10.0f);

		// 目的地をセットする
		pos = new Vector3 (x, y, z);
		agent.Resume ();

		agent.SetDestination (pos);
	}

	// Walkのパラメータをセットするメソッド
	public void setWalk () {
		anim.SetBool ("Walk", true);
		anim.SetBool ("Idle", false);
	}

	//タッチされたときにTouchManager.csから呼ばれる（タッチへの反応内容をここに書いてください）added by takahashi
	public void Touched() {
		anim.SetBool ("Jump", true);
		Debug.Log ("Touched");
	}
}
