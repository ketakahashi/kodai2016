using UnityEngine;
using System.Collections;

public class changeTexture : MonoBehaviour {

	Texture texture1;

	float duration = 0;

	private AudioSource audioSource;    // AudioSorceコンポーネント格納用
	public AudioClip sound;        // 効果音の格納用。インスペクタで。

	// Use this for initialization
	IEnumerator Start () {

		WWW www = new WWW( "https://pbs.twimg.com/profile_images/479856017438556160/TUPKOb9i.jpeg" );
//		WWW www = new WWW( "file:///Users/ktakahas/Desktop/a.jpg" );
		yield return www;
		GetComponent<Renderer>().material.mainTexture = www.texture;

		// Resources.LoadでGrassHillオブジェクトをインスタンスし、Texture型にキャスト
//		texture1 = (Texture)Resources.Load ("Texture/dora");
//		GetComponent<Renderer>().material.mainTexture = texture1;
		// 現在の色を取得
//		color1 = GetComponent<Renderer>().material.color;
		//Destroy (gameObject, 3);

		audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.loop    = false;   

		//audioSource.PlayOneShot(sound);   

	}

	// Update is called once per frame
	private int playtimes = 0;

	void Update () {
		//transform.Translate (Random.Range (-0.1f, 0.1f), 0.0f, Random.Range (-0.1f, 0.1f));
		duration += Time.deltaTime;
		if (duration > 3) {
			if (audioSource.isPlaying) {
				return;
			} else {
				if (playtimes > 0) {
					Destroy (gameObject);
				} else {
					audioSource.PlayOneShot (sound);
					playtimes++;
				}
			}
		}
	}
}
