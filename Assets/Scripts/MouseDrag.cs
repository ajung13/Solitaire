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
		Debug.Log (finalPosition.x + " = " + x_start + " + " + x_offset + " * " + line);

		if (validCheck (line)) {
			int lineIdx = findLineIdx (line);
			Debug.Log ("move from line " + GetComponent<Card> ().line + " to line (" + line + ", " + lineIdx + ")");
			moveCards (line, lineIdx);
		} else {
			transform.position = initPosition;
		}

	}

	int findLineIdx(int line){
		return GameController.playCards [line].Count;
	}

	void moveCards(int line, int lineIdx){
		int myLine = GetComponent<Card> ().line;
		int myLineIdx = GetComponent<Card> ().lineIdx;

		List<GameObject> temp = GameController.playCards [myLine];
		int tmpCnt = temp.Count;
		for (int i = 0; i < tmpCnt - myLineIdx; i++) {
			Vector2 objPos = new Vector2 (x_start + line * x_offset, y_start + (lineIdx + i) * y_offset);
			GameObject tmp = temp[myLineIdx];
			tmp.GetComponent<Card> ().line = line;
			tmp.GetComponent<Card> ().lineIdx = lineIdx + i;
			tmp.transform.position = objPos;
			tmp.GetComponent<SpriteRenderer> ().sortingOrder = GameController.updateCardOrder();
			GameController.playCards [myLine].Remove (tmp);
			GameController.playCards [line].Add (tmp);
		}

		Debug.Log ("----move complete (" + tmpCnt + "-" + myLineIdx + " objects)-----");

		GameController.revealCard (myLine);
	}

	bool validCheck(int line){
		if (line == GetComponent<Card> ().line)
			return false;

		bool returnflag = false;
		Card prevCard = GameController.lastCardinLine (line).GetComponent<Card> ();

		//color check
		int prevColor = prevCard.shape;
		switch (GetComponent<Card> ().shape) {
		case 0:
		case 3:
			if (prevColor == 1 || prevColor == 2)
				returnflag = true;
			break;
		case 1:
		case 2:
			if (prevColor == 0 || prevColor == 3)
				returnflag = true;
			break;
		default:
			break;
		}
		if (!returnflag)
			return returnflag;
		Debug.Log ("color check ok");

		//number check
		returnflag = false;
		int prevnum = prevCard.number;
		if (GetComponent<Card> ().number + 1 == prevnum) {
			returnflag = true;
			Debug.Log ("number check ok");
		} else
			Debug.Log ("number check failed");

		return returnflag;
	}
}
