using UnityEngine;
using System.Collections;
using System.IO;

public class HaniwaGenerator : MonoBehaviour {
	//スキャン画像ディレクトリ
	string path = "/Users/" + System.Environment.GetEnvironmentVariable("USER") + "/Desktop/images";

	public GameObject HaniwaPrefab;

	void Start () {
		InvokeRepeating("CheckScannedFile", 0, 5); //定周期呼び出し（スキャン画像有無検知のため）
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(1)) { //for debug: SPACEキーだと連続して生成されるのでマウス右クリックにした。
			InstantiateHaniwa();
		}
	}

	void InstantiateHaniwa() {
		// 配置する座標を設定
		Vector3 placePosition = new Vector3 (0, 0, 0);

		// 配置する回転角を設定
		Quaternion q = new Quaternion ();
		q = Quaternion.identity;

		// 配置
		Instantiate (HaniwaPrefab, placePosition, q);
	}

	void CheckScannedFile() {
		string[] files = Directory.GetFiles(path, "*.png");
		if (files.Length > 0) { //１つでのスキャン画像があれば生成実行
			InstantiateHaniwa ();
		}
	}
}
