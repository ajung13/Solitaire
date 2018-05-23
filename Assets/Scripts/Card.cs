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

	public void initialize(bool flag, int i, int j, GameObject me){
		isHidden = flag;
		this.shape = i;
		this.number = j;
		this.me = me;
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
