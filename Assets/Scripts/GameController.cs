using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum shape {Heart, Diamond, Clova, Spade};

public class GameController : MonoBehaviour {
	public Sprite[] shape = new Sprite[4];
	public Sprite hiddenCard;
	public Sprite[] cardSprites = new Sprite[4*13];

	public GameObject Card;
	private Card[] playCards = new Card[4*13];
	private int cardIdx = 0;
	private bool[] setFlag = new bool[4*13];

	public Vector2[] playDeck = new Vector2[7];
	public Vector2[] shapeDeck = new Vector2[4];

	// Use this for initialization
	void Start () {
		preCardSet ();
		for (int i = 0; i < 4 * 13; i++)
			setFlag [i] = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void preCardSet(){
		for (int i = 0; i < 7; i++) {
			playDeck [i] = new Vector2 (-3.6f + 1.8f * i, 3.37f);
			for (int j = 0; j < i; j++)
				Instantiate (Card, playDeck [i] + new Vector2 (0.0f, -0.3f * j), Quaternion.identity);
			newCardSet (i, playDeck[i] + new Vector2(0.0f, -0.3f * i));
		}
		for (int i = 0; i < 4; i++) {
			shapeDeck [i] = new Vector2 (-7.5f + (i % 2) * 1.7f, -1 - (i / 2) * 2.3f);
			GameObject tmp = Instantiate (Card, shapeDeck [i], Quaternion.identity) as GameObject;
			tmp.GetComponent<SpriteRenderer> ().sprite = shape[i];
		}
	}

	void newCardSet(int line, Vector2 position){
		int newRandomNum = (int) Random.Range (0.0f, 52.0f);
		while (newRandomNum == 52 || setFlag [newRandomNum]) {
			//find another random number;
			newRandomNum = (int) Random.Range (0.0f, 52.0f);
		}
		setFlag [newRandomNum] = true;

		playCards [cardIdx] = new Card (newRandomNum / 13, newRandomNum % 13);
		playCards [cardIdx].cardTexture = cardSprites[newRandomNum];
		playCards [cardIdx].line = line;
		playCards [cardIdx].lineIdx = line;

		GameObject tmp = Instantiate (Card, position, Quaternion.identity) as GameObject;
		tmp.GetComponent<SpriteRenderer> ().sprite = playCards [cardIdx].cardTexture;
		cardIdx++;
	}
}
