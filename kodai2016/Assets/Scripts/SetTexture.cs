using UnityEngine;
using System.Collections;
using System.IO;

public class SetTexture : MonoBehaviour {
	//スキャン画像ディレクトリ
	string path = "/Users/" + System.Environment.GetEnvironmentVariable("USER") + "/Desktop/images";

	IEnumerator Start () {
		string[] files = Directory.GetFiles(path, "*.png");
		if (files.Length > 0) {
			WWW www = new WWW( "file://" + files[0]);
			yield return www;
			GetComponent<Renderer>().material.mainTexture = www.texture;
			File.Move (files[0] , Path.GetDirectoryName(files[0]) + "/done/" + Path.GetFileName(files[0]));
		}
	}
	void Update () {
	}
}
