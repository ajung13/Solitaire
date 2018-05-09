using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum shape {Heart, Diamond, Clova, Spade};

public class GameController : MonoBehaviour {
	public Sprite[] shape = new Sprite[4];
	public Sprite hiddenCard;
	public Sprite[] cardSprites = new Sprite[4*13];

	public GameObject Card;
//	public static Card[] playCards = new Card[4*13];
	public static List<GameObject>[] playCards = new List<GameObject>[7];
	private int cardIdx = 0;
	private bool[] setFlag = new bool[4*13];

	public Vector2[] playDeck = new Vector2[7];
	public Vector2[] shapeDeck = new Vector2[4];

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4 * 13; i++)
			setFlag [i] = false;
		for (int i = 0; i < 7; i++)
			playCards [i] = new List<GameObject> ();
		preCardSet ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void preCardSet(){
		for (int i = 0; i < 7; i++) {
			playDeck [i] = new Vector2 (-3.6f + 1.8f * i, 3.37f);
			for (int j = 0; j < i; j++)
				newCardSet (true, i, j, playDeck [i] + new Vector2 (0.0f, -0.3f * j));
//				Instantiate (Card, playDeck [i] + new Vector2 (0.0f, -0.3f * j), Quaternion.identity);
			newCardSet (false, i, i, playDeck[i] + new Vector2(0.0f, -0.3f * i));
		}
		for (int i = 0; i < 4; i++) {
			shapeDeck [i] = new Vector2 (-7.5f + (i % 2) * 1.7f, -1 - (i / 2) * 2.3f);
			GameObject tmp = Instantiate (Card, shapeDeck [i], Quaternion.identity) as GameObject;
			tmp.GetComponent<SpriteRenderer> ().sprite = shape[i];
		}
	}

	void newCardSet(bool hidden, int i, int j, Vector2 position){
		int newRandomNum;
		newRandomNum = (int) Random.Range (0.0f, 52.0f);
		while (newRandomNum == 52 || setFlag [newRandomNum]) {
			//find another random number;
			newRandomNum = (int) Random.Range (0.0f, 52.0f);
		}
		setFlag [newRandomNum] = true;

		GameObject tmp = Instantiate (Card, position, Quaternion.identity) as GameObject;
		tmp.GetComponent<Card> ().initialize (hidden, newRandomNum / 13, newRandomNum % 13, tmp);
		tmp.GetComponent<Card> ().line = i;
		tmp.GetComponent<Card> ().lineIdx = j;
		if (!hidden) {
			tmp.GetComponent<Card> ().cardTexture = cardSprites [newRandomNum];
			tmp.GetComponent<SpriteRenderer> ().sprite = cardSprites [newRandomNum];
			tmp.GetComponent<BoxCollider2D> ().enabled = true;
		}
//		playCards [cardIdx] = tmp.GetComponent<Card> ();
		playCards[i].Add(tmp);
		cardIdx++;
	}
}
