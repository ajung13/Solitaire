using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
	public bool isHidden;
	private Sprite cardTexture;
	private int shape, number;
	private int line, lineIdx;

	public Card(int i, int j, Sprite texture){
		isHidden = true;
		this.cardTexture = texture;
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
