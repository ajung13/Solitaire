using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour {
	private Vector2 initPosition;

	private static readonly float x_start = -3.6f;
	private static readonly float x_offset = 1.8f;
	private static readonly float y_start = 3.37f;
	private static readonly float y_offset = -0.3f;

	void OnMouseDown(){
		initPosition = transform.position;
	}

	void OnMouseDrag(){
		Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		transform.position = objPosition;
	}

	void OnMouseUp(){
		Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
		Vector2 finalPosition = Camera.main.ScreenToWorldPoint (mousePosition);

		int line = (int)((finalPosition.x - x_start) / x_offset);

		if (validCheck (line)) {
			int lineIdx = findLineIdx (line);
			Debug.Log ("move to line (" + line + ", " + lineIdx + ")");
			moveCards (line, lineIdx);
		} else {
			transform.position = initPosition;
		}

	}

	int findLineIdx(int line){
/*		int max = 0;
		Card[] cards = GameController.playCards;
		for (int i = 0; i < 4 * 13; i++) {
			if (cards [i] == null)
				break;
			if (cards[i].line == line && cards[i].lineIdx > max)
				max = cards[i].lineIdx;
		}
		max++;
		return max;*/
		return GameController.playCards [line].Count;
	}

	void moveCards(int line, int lineIdx){
		int myLine = GetComponent<Card> ().line;
		int myLineIdx = GetComponent<Card> ().lineIdx;

		List<GameObject> temp = GameController.playCards [myLine];
		int tmpCnt = temp.Count;
		for (int i = 0; i < tmpCnt - myLineIdx; i++) {
			Vector2 objPos = new Vector2 (x_start + line * x_offset, y_start + (lineIdx + i) * y_offset);
//			GameObject tmp = GameController.playCards [line].FindLast;
//			GameObject tmp = GameController.playCards [line][i];
			GameObject tmp = temp[myLineIdx];
			tmp.GetComponent<Card> ().line = line;
			tmp.GetComponent<Card> ().lineIdx = lineIdx;
			tmp.transform.position = objPos;
			GameController.playCards [myLine].Remove (tmp);
			GameController.playCards [line].Add (tmp);
		}

		Debug.Log ("----move complete (" + (tmpCnt-myLineIdx) + " objects)-----");
		foreach (GameObject tmp in GameController.playCards[myLine])
			tmp.GetComponent<Card> ().printInfo ();
		foreach (GameObject tmp in GameController.playCards[line])
			tmp.GetComponent<Card> ().printInfo ();

			
	}

	bool validCheck(int line){
		return true;
	}
}
