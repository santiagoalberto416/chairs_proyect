using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GestureRecognizer : MonoBehaviour {

	private int count;
	//public GameObject player;
	public Text countText;
	public float speed = 0.1F;

	void Start () {
		count = 0;
		countText.text = "Count: " + count.ToString ();
	}

	/*
	void Update()
	{
		if (Input.touchCount > 0) {
			count = count + 1;
			countText.text = "Count: " + count.ToString ();
			Debug.Log (Input.touchCount);
		}
	}
	*/
	void Update()
	{
		if (Input.touchCount > 0) {
			// The screen has been touched so store the touch
			Touch touch = Input.GetTouch(0);

			// the phase can define the cicle of life of the touch
			if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
				// If the finger is on the screen, move the object smoothly to the touch position
				Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));                
				transform.position = touchPosition;
			}
		}
	}




}
