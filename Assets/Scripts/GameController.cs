﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum shape {Heart, Diamond, Clova, Spade};

public class GameController : MonoBehaviour {
	public static List<GameObject>[] playCards = new List<GameObject>[12];
	private static int cardOrdering;
	public static int hiddenCardNum;

	public Sprite[] shape = new Sprite[4];
	public Sprite hiddenCard;
	public Sprite[] cardSprites = new Sprite[4*13];

	public GameObject Card;
	private static bool[] setFlag = new bool[4*13];

	public Vector2[] playDeck = new Vector2[7];
	public Vector2[] shapeDeck = new Vector2[4];
	private readonly Vector2 hiddenDeckPos = new Vector2 (-7.45f, 3.37f);
	private readonly Vector2 cardDeckPos = new Vector2(-5.65f, 3.37f);

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 4 * 13; i++)
			setFlag [i] = false;
		for (int i = 0; i < 12; i++)
			playCards [i] = new List<GameObject> ();
		cardOrdering = 1;
		hiddenCardNum = 0;
		preCardSet ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)){
			if (Input.mousePosition.x >= 25 && Input.mousePosition.x <= 65 && Input.mousePosition.y >= 238 && Input.mousePosition.y <= 295)
				newCard ();
		}
		if (playCards[7].Count != 0 && gameClearCheck ()) {
			Debug.Log ("Game Clear");
		}
	}

	bool gameClearCheck(){
		return false;
	}

	void preCardSet(){
		//cards in line
		for (int i = 0; i < 7; i++) {
			playDeck [i] = new Vector2 (-3.6f + 1.8f * i, 3.37f);
			for (int j = 0; j < i; j++)
				newCardSet (true, i, j, playDeck [i] + new Vector2 (0.0f, -0.3f * j));
			newCardSet (false, i, i, playDeck[i] + new Vector2(0.0f, -0.3f * i));
		}

		//waiting cards
		for(int i = 0; i < 24; i++)
			newCardSet (true, 7, i, new Vector2 (-7.45f, 3.37f));

		//cards in shape
		for (int i = 0; i < 4; i++) {
			shapeDeck [i] = new Vector2 (-7.5f + (i % 2) * 1.7f, -1 - (i / 2) * 2.3f);
			GameObject tmp = Instantiate (Card, shapeDeck [i], Quaternion.identity) as GameObject;
			tmp.GetComponent<SpriteRenderer> ().sprite = shape[i];
			tmp.name = "shape" + i;
		}

		Debug.Log("Card set completed");
	}

	public void newCardSet(bool hidden, int i, int j, Vector2 position){
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

		switch (newRandomNum / 13) {
		case 0:
			tmp.name = "clova ";
			break;
		case 1:
			tmp.name = "dia ";
			break;
		case 2:
			tmp.name = "heart ";
			break;
		case 3:
			tmp.name = "spade ";
			break;
		}
		tmp.name += (newRandomNum % 13);

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

	void newCard(){
		List<GameObject> tmp = GameController.playCards [7];
		int ordering = GameController.hiddenCardNum;
		if (ordering >= tmp.Count) {
			GameController.hiddenCardNum = 0;
			foreach (GameObject eachObj in tmp) {
				eachObj.transform.position = hiddenDeckPos;
				eachObj.GetComponent<Card> ().isHidden = true;
				eachObj.GetComponent<SpriteRenderer> ().sprite = hiddenCard;
				eachObj.GetComponent<BoxCollider2D> ().enabled = false;
			}
			return;
		}

		GameObject nextCard = tmp [ordering];
		nextCard.transform.position = cardDeckPos;
		nextCard.GetComponent<Card> ().isHidden = false;
		nextCard.GetComponent<SpriteRenderer> ().sprite = nextCard.GetComponent<Card>().cardTexture;
		nextCard.GetComponent<BoxCollider2D> ().enabled = true;

		GameController.hiddenCardNum++;
	}
}
