using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum shape {Heart, Diamond, Clova, Spade};

public class GameController : MonoBehaviour {
//	public static Card[,] cards = new Card[4, 13];
	public Sprite shape;
	public Sprite hiddenCard;

	public GameObject Card;

	public Vector2[] playDeck = new Vector2[7];
	public Vector2[] shapeDeck = new Vector2[4];

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 7; i++) {
			playDeck [i] = new Vector2 (-1.0f + 1.8f * i, 3.37f);
			for (int j = 0; j < i + 1; j++)
				Instantiate (Card, playDeck [i] + new Vector2 (0.0f, -0.3f * j), Quaternion.identity);
		}
		for (int i = 0; i < 4; i++) {
			shapeDeck [i] = new Vector2 (-7.5f + (i % 2) * 2, -1 - (i / 2) * 2);
			GameObject tmp = Instantiate (Card, shapeDeck [i], Quaternion.identity) as GameObject;
			tmp.GetComponent<SpriteRenderer> ().sprite = shape;
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
