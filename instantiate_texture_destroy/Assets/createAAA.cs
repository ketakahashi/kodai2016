using UnityEngine;
using System.Collections;

public class createAAA : MonoBehaviour {

	public Transform cube;

	// クリックした位置座標
	private Vector3 clickPosition;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
//		if (Input.GetButtonDown("Fire1")) {
//			Instantiate(cube, new Vector3(0,5,0), Quaternion.identity);
//		}
		//http://buravo46.tumblr.com/post/70380413072/unityクリックした位置にprefabを生成する	
		if (Input.GetMouseButtonDown(0)) {
			// ここでの注意点は座標の引数にVector2を渡すのではなく、Vector3を渡すことである。
			// Vector3でマウスがクリックした位置座標を取得する
			clickPosition = Input.mousePosition;
//			Debug.Log (clickPosition.x);
//			Debug.Log (clickPosition.y);
			Debug.Log (Camera.main.ScreenToWorldPoint(clickPosition));
			// Z軸修正
			clickPosition.z = 10f;
			// オブジェクト生成 : オブジェクト(GameObject), 位置(Vector3), 角度(Quaternion)
			// ScreenToWorldPoint(位置(Vector3))：スクリーン座標をワールド座標に変換する
			Instantiate(cube, Camera.main.ScreenToWorldPoint(clickPosition), cube.transform.rotation);
//			Instantiate(cube, clickPosition, cube.transform.rotation);

		}
	}
}
