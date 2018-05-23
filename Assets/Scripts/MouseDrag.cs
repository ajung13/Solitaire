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

		if (finalPosition.x >= x_start) {
			//destination is line 0 to 6
			int line = (int)((finalPosition.x - x_start) / x_offset);
			Debug.Log (finalPosition.x + " = " + x_start + " + " + x_offset + " * " + line);

			if (validCheck (line)) {
				int lineIdx = findLineIdx (line);
				Debug.Log ("move from line " + GetComponent<Card> ().line + " to line (" + line + ", " + lineIdx + ")");
				moveCards (line, lineIdx);
			} else {
				transform.position = initPosition;
			}
		} else if (finalPosition.y < -1) {
			//destination is line 8-11 (shape deck)
			int line = 8;
			if (finalPosition.x >= -5.8f)
				line++;
			if (finalPosition.y <= -3.3f)
				line += 2;

			switch (line) {
			case 8:
				Debug.Log ("clova");
				break;
			case 9:
				Debug.Log ("dia");
				break;
			case 10:
				Debug.Log ("heart");
				break;
			case 11:
				Debug.Log ("spade");
				break;
			default:
				Debug.Log ("error");
				break;
			}

			if (GetComponent<Card> ().line < 7) {
				if (GetComponent<Card> ().lineIdx != GameController.playCards [GetComponent<Card> ().line].Count - 1) {
					//not the last card
					transform.position = initPosition;
					return;
				}
			}
			if (validCheck (line)) {
				int lineIdx = findLineIdx (line);
				Debug.Log ("move from line " + GetComponent<Card> ().line + " to shape line (" + line + ", " + lineIdx + ")");
				moveCards (line, lineIdx);
			} else {
				transform.position = initPosition;
			}
		} else {
			//destination is line 7 (waiting line) or errors
			transform.position = initPosition;
		}
	}

	int findLineIdx(int line){
		return GameController.playCards [line].Count;
	}

	void moveCards(int line, int lineIdx){	//parameters mean destination
		int myLine = GetComponent<Card> ().line;		//these mean init line and lineIdx
		int myLineIdx = GetComponent<Card> ().lineIdx;

		if (line == 7) {
			Debug.Log ("cannot move to waiting line");
			return;
		}

		//move all cards behind me
		List<GameObject> temp = GameController.playCards [myLine];
		int tmpCnt = temp.Count;
		int cnt = 0;
		for (int i = 0; i < tmpCnt - myLineIdx; i++) {
			Vector2 objPos;
			if (line < 7)
				objPos = new Vector2 (x_start + line * x_offset, y_start + (lineIdx + i) * y_offset);
			else
				objPos = new Vector2 (-7.5f + ((line-8) % 2) * 1.7f, -1 - ((line-8) / 2) * 2.3f);
			GameObject tmp = temp[myLineIdx];
			if (tmp.GetComponent<Card> ().shape != GetComponent<Card> ().shape || tmp.GetComponent<Card> ().number != GetComponent<Card> ().number) {
				Debug.Log ("omg what's going on");
				tmp.GetComponent<Card> ().printInfo ();
			}
			tmp.GetComponent<Card> ().line = line;
			tmp.GetComponent<Card> ().lineIdx = lineIdx + i;
			tmp.transform.position = objPos;
			tmp.GetComponent<SpriteRenderer> ().sortingOrder = GameController.updateCardOrder();
			GameController.playCards [myLine].Remove (tmp);
			GameController.playCards [line].Add (tmp);
			cnt++;
			if (myLine >= 7) {
				Debug.Log ("I was waiting or going to shape : moving cnt " + cnt);
				break;
			}
		}

		Debug.Log ("----move complete (" + tmpCnt + "-" + myLineIdx + " objects)-----");

		if(myLine != 7)
			GameController.revealCard (myLine);
	}

	bool validCheck(int line){
		if (line == GetComponent<Card> ().line)
			return false;

		bool returnflag = false;
		if (GameController.lastCardinLine (line) == null) {
			//there's no card in this line
			Debug.Log("no card in line " + line);
			if (GetComponent<Card> ().number == 12)
				return true;
			else if (GetComponent<Card> ().number == 0 && line - 8 == GetComponent<Card> ().shape)
				return true;
			else
				return false;
		}

		Card prevCard = GameController.lastCardinLine (line).GetComponent<Card> ();

		//color check
		int prevColor = prevCard.shape;
		if (line >= 8) {
			if (prevColor == GetComponent<Card> ().shape)
				returnflag = true;
		} else {
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
		}
		if (!returnflag)
			return returnflag;
		Debug.Log ("color check ok");

		//number check
		returnflag = false;
		int prevnum = prevCard.number;
		if (GetComponent<Card> ().number + 1 - 2 * (line / 8) == prevnum) {
			returnflag = true;
			Debug.Log ("number check ok");
		} else
			Debug.Log ("number check failed");

		return returnflag;
	}
}
