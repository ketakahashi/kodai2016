#pragma strict

var MyTofu:GameObject;
private var TofuCount:int = 0;

function Start () {

//	InvokeRepeating("DropOne", 2f, 1f);
}

/*
function DropOne() {
	TofuCount++;
	Instantiate(MyTofu, Vector3(0,5,0), Quaternion.identity);
	if (TofuCount == 20) {
		CancelInvoke();
	}
}
*/
function Update() {
	if (Input.GetButtonDown("Jump")) {
		Instantiate(MyTofu, Vector3(0,5,0), Quaternion.identity);
	}
}