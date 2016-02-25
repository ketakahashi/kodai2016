using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPReceive : MonoBehaviour
{
	int LOCA_LPORT = 22222;
	static UdpClient udp;
	Thread thread;

	void Start ()
	{
		Debug.Log ("START");
		udp = new UdpClient(LOCA_LPORT);
		udp.Client.ReceiveTimeout = 1000;
		thread = new Thread(new ThreadStart(ThreadMethod));
		thread.Start(); 
	}

	void Update ()
	{
	}

	void OnApplicationQuit()
	{
		Debug.Log ("Quit");
		thread.Abort();
	}

	private static void ThreadMethod()
	{
		while(true)
		{
			Debug.Log("Receive Start:");
			IPEndPoint remoteEP = null;
			byte[] data = udp.Receive(ref remoteEP);
			string text = Encoding.ASCII.GetString(data);
			Debug.Log("Receive Data:" + text);
		}
	} 
}
