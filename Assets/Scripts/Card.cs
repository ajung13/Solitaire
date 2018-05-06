using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
	public bool isHidden;
	public Sprite cardTexture;
	private int shape, number;
	public int line, lineIdx;

	public Card(int i, int j){
		isHidden = true;
		this.shape = i;
		this.number = j;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
