using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	public int id = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeState() {
		Debug.Log (++id);
		transform.Translate (Random.Range (-1.0f, 1.0f), 5.0f, Random.Range (-1.0f, 1.0f));
	}
}
