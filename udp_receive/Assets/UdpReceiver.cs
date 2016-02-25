using UnityEngine;
using System.Collections;
using System;

public class UdpReceiver : MonoBehaviour {
	public int listenPort = 2002; // ポートはサーバ・クライアントで合わせる必要がある

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
			Debug.Log (vec);
			Ray ray = Camera.main.ScreenPointToRay(vec);
			if (Physics.Raycast(ray, out hit)){
				GameObject obj = hit.collider.gameObject;
				//Debug.Log (obj.name);
				try
				{
					obj.GetComponent<Cube>().ChangeState();
				}
				catch(NullReferenceException)
				{
					Debug.Log("Error");
				}
			}
		}
	}

	void Start () {
		StartCoroutine(receive_loop());
	}

	void Update() {
		
	}
}
