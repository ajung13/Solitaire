using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum shape {Heart, Diamond, Clova, Spade};

public class GameController : MonoBehaviour {
	public static List<GameObject>[] playCards = new List<GameObject>[7];
	private static int cardOrdering;

	public Sprite[] shape = new Sprite[4];
	public Sprite hiddenCard;
	public Sprite[] cardSprites = new Sprite[4*13];

	public GameObject Card;
	private bool[] setFlag = new bool[4*13];

	public Vector2[] playDeck = new Vector2[7];
	public Vector2[] shapeDeck = new Vector2[4];

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4 * 13; i++)
			setFlag [i] = false;
		for (int i = 0; i < 7; i++)
			playCards [i] = new List<GameObject> ();
		cardOrdering = 0;
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
		tmp.GetComponent<Card> ().cardTexture = cardSprites [newRandomNum];
		if (!hidden) {
			tmp.GetComponent<SpriteRenderer> ().sprite = cardSprites [newRandomNum];
			tmp.GetComponent<BoxCollider2D> ().enabled = true;
		}
		playCards[i].Add(tmp);
	}

	public static int updateCardOrder(){
		return ++cardOrdering;
	}

	public static void revealCard(int line){
		GameObject tmp = lastCardinLine(line);
		if (tmp == null)
			return;
		if (!tmp.GetComponent<Card> ().isHidden) {
			Debug.Log ("is not hidden");
			return;
		}
		tmp.GetComponent<Card> ().isHidden = false;
		tmp.GetComponent<SpriteRenderer> ().sprite = tmp.GetComponent<Card> ().cardTexture;
		tmp.GetComponent<BoxCollider2D> ().enabled = true;
	}

	public static GameObject lastCardinLine(int line){
		List<GameObject> tmpLine = playCards [line];
		if (tmpLine.Count == 0)
			return null;
		GameObject tmp = tmpLine [tmpLine.Count - 1];
		return tmp;
	}
}
