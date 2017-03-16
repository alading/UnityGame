using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	private int count;
	public Text countText;

	Vector3 zeroAc;
	Vector3 curAc;
	float sensH = 5f;
	float sensV = 5f;
	float smooth = 0.5f;
	float GetAxisH  = 0f;
	float GetAxisV = 0f;

	void ResetAxes(){
		zeroAc = Input.acceleration;
		curAc = Vector3.zero;
	}


	// start the game
	void Start() {

		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		this.GetComponent<ParticleSystem>().Stop();
		ResetAxes ();
	

	}


	// physic update

	private void Update () {
		
	}

	void FixedUpdate() {

		if (SystemInfo.deviceType == DeviceType.Handheld) {
			Debug.Log ("mobile");
			curAc = Vector3.Lerp (curAc, Input.acceleration - zeroAc, Time.deltaTime / smooth);
			GetAxisV = Mathf.Clamp (curAc.y * sensV, -1, 1);
			GetAxisH = Mathf.Clamp (curAc.x * sensH, -1, 1);
			rb.AddForce (new Vector3 (GetAxisH, 0.0f, GetAxisV) * speed);
		} else {

			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (movement * speed);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}
	}

	void SetCountText ()
	{
		countText.text = "Score: " + count.ToString ();
		if (count > 14)
		{
			countText.text = "You win";
			this.GetComponent<ParticleSystem>().Play();
		}
	}
}
