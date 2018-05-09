using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
	public GameObject me;

	public bool isHidden;
	public Sprite cardTexture;
	public int shape, number;
	public int line, lineIdx;

	private Vector3 offset;
//	private bool dragFlag;

	public void initialize(bool flag, int i, int j, GameObject me){
		isHidden = flag;
		this.shape = i;
		this.number = j;
		this.me = me;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
/*		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("mouse down");
			offset = gameObject.transform.position -
				Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			printInfo ();
			dragFlag = true;
		}

		if (dragFlag) {
			Debug.Log ("mouse drag");
			Vector3 newPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f);
//			transform.position = Camera.main.ScreenToWorldPoint (newPosition) + offset;
//			transform.localPosition = Camera.main.ScreenToWorldPoint (newPosition) + offset;
			me.transform.localPosition = Camera.main.ScreenToWorldPoint (newPosition) + offset;
		}

		if (Input.GetMouseButtonUp (0)) {
			dragFlag = false;
		}*/
	}

	public void printInfo(){
		string debugStr = "";

		if (me == null)
			debugStr += "game object is null\n";

		if (isHidden)
			debugStr += "hidden\n";
		else
			debugStr += "not hidden\n";

		debugStr += "shape: " + shape + ", number: " + number;
		debugStr += ", line: " + line + ", " + lineIdx;
		
		Debug.Log (debugStr);
	}
}
