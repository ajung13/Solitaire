using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
	public bool isHidden;
	public Sprite hidden;
	private Sprite cardTexture;

	public Card(int i, int j, Sprite texture){
		isHidden = true;
		this.cardTexture = texture;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
