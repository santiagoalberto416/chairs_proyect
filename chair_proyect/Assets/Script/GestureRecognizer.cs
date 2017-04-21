using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GestureRecognizer : MonoBehaviour {

	private int count;
	//public GameObject player;
	public Text countText;
	public Text debugText;
	public float speed = 0.1F;
	public float safeDistance = 1.2f;



	int pulse = 0;
	static int maxPulse = 400;


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
	void FixedUpdate()
	{
		
		Debug.Log (GetClosestEnemy().name);


		if (Input.touchCount > 0) {


			// The screen has been touched so store the touch
			Touch touch = Input.GetTouch (0);
			debugText.text = touch.deltaTime.ToString();
			// the phase can define the cicle of life of the touch
			if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
				// If the finger is on the screen, move the object smoothly to the touch position
				Vector3 touchPosition = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 10));


				///new Vector3.Lerp(transform.position, touchPosition, Time.deltaTime)
				transform.position = touchPosition;
			}


			if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began) {
				modifyPulse (true);
				countText.text = "moving finger , pulse:" + pulse;

			} else {
				modifyPulse (false);
				countText.text = "isnt moving , pulse:" + pulse;
			}

		} else {
			modifyPulse (false);
			countText.text = "isnt moving" + pulse;
		}
	}

	private void modifyPulse(bool isAdding){
		if (isAdding) {
			if (pulse < maxPulse) {
				pulse = pulse + 5;
			} else {
				pulse = maxPulse;
			}
		} else {
			if (pulse > 0) {
				pulse = pulse - 5;
			} else {
				pulse = 0;
			}
		}
	}
		
	GameObject GetClosestEnemy()
	{
		GameObject[] chairs = GameObject.FindGameObjectsWithTag("chair");
		Transform[] enemies = new Transform[chairs.Length];
		int position = 0;
		foreach(GameObject gObject in chairs){
			enemies [position] = gObject.transform;
			position++;
		}

		GameObject tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		float dist = 0;
		foreach (GameObject t in chairs)
		{
			dist = Vector3.Distance(t.transform.position, currentPos);
			Debug.Log (dist.ToString());
			if (dist < minDist)
			{
				tMin = t;
				minDist = dist;
			}
		}


		// formula

		if (minDist >= safeDistance) {
			float reason = safeDistance / minDist;
			float x1 = currentPos.x;
			float x2 = tMin.transform.position.x;
			float y1 = currentPos.y;
			float y2 = tMin.transform.position.y;

			// new x
			float nX = (x1 + (reason * x2)) / (1 + reason); 

			// new y
			float nY = (y1 + (reason * y2)) / (1 + reason); 

			Vector3 newPosition = new Vector3 (nX, nY, 0);



			transform.Translate (newPosition * Time.deltaTime);
			Debug.Log ("enter wirh distance equal to : " + dist);
		} else {
			// move arround the chair
			transform.RotateAround(tMin.transform.position, Vector3.forward, 20*Time.deltaTime);
		}
		return tMin;
	}

	/// we are going to try calcule the "velocity"




}
