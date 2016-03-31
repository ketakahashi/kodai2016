using UnityEngine;
using System.Collections;
using System;

public class TouchManager : MonoBehaviour {
	public int listenPort = 2002; //UDP通信用ポート番号、Processingと合わせる必要がある。

	private static bool received = false;
	private static Vector3 vec;
	private RaycastHit hit = new RaycastHit();

	private struct UdpState {
		public System.Net.IPEndPoint e;
		public System.Net.Sockets.UdpClient u;
	}

	public static void ReceiveCallback(System.IAsyncResult ar) {
		System.Net.Sockets.UdpClient u = (System.Net.Sockets.UdpClient)((UdpState)(ar.AsyncState)).u;
		System.Net.IPEndPoint e = (System.Net.IPEndPoint)((UdpState)(ar.AsyncState)).e;
		var receiveBytes = u.EndReceive(ar, ref e);
		var receiveString = System.Text.Encoding.ASCII.GetString(receiveBytes);

		string[] values = receiveString.Split(',');
		vec.x = int.Parse(values[0]);
		vec.y = int.Parse(values[1]);
		//Debug.Log(string.Format("Received: {0}", receiveString)); // ここに任意の処理を書く
		received = true;
	}

	IEnumerator receive_loop() {
		var e = new System.Net.IPEndPoint(System.Net.IPAddress.Any, listenPort);
		var u = new System.Net.Sockets.UdpClient(e);
		u.EnableBroadcast = true;
		var s = new UdpState();
		s.e = e;
		s.u = u;
		for (;;) {
			received = false;
			u.BeginReceive(new System.AsyncCallback(ReceiveCallback), s);
			while (!received) {
				yield return null;
			}
			Ray ray = Camera.main.ScreenPointToRay(vec);
			if (Physics.Raycast(ray, out hit)){
				GameObject obj = hit.collider.gameObject;
				try
				{
					obj.GetComponent<HaniwaAI>().Touched(); //HaniwaAI.csのTouchedメソッドを呼び出す
				}
				catch(NullReferenceException)
				{
					Debug.Log("Error"); //タッチした位置いオブジェクトを見つけられない
				}
			}
		}
	}

	void Start () {
		StartCoroutine(receive_loop());
	}

	void Update() {
		//以下はデバッグ用（レンジセンサーがなくてもマウス左クリックでタッチ処理を再現する）
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				GameObject obj = hit.collider.gameObject;
				try {
					obj.GetComponent<HaniwaAI> ().Touched (); //HaniwaAI.csのTouchedメソッドを呼び出す
				} catch (NullReferenceException) {
					Debug.Log ("Error"); //タッチした位置いオブジェクトを見つけられない
				}
			}
		}
	}
}
