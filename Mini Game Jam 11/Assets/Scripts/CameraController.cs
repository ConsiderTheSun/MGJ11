using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Set in Inspector")]
	public Transform playerTransform;
	public Vector3 cameraOffset;
	public float cameraSpeed = 0.1f;
	// max speed if plaer is at edge of screen
	public float maxSpeedUp = 1f;

	// distance between center and right of screen
	private float distanceCenterToRight;
	// Start is called before the first frame update
	
	void Start()
	{
		transform.position = playerTransform.position + cameraOffset;
	}

	void FixedUpdate ()
    {
		Vector3 finalPosition = playerTransform.position + cameraOffset;
		Vector3 lerpPosition = Vector3.Lerp(transform.position, finalPosition, cameraSpeed);
		transform.position = (lerpPosition.x, lerpPosition.y, -10);
    }
	// lerpPosition is the current camera position

	void lateUpdate()
    {
		float speed = minSpeed;

		// if player is to the right of the screen
		if (transform.position.x < playerTransform.position.x)
        {
			// calc speed that increases further over the player is
			speed += Mathf.Lerp(
				0,
				maxSpeedUp,
				// how far from center play has moved, normalized
				(playerTransform.position.x - transform.position.x) / distanceCenterToRight);
        }
		// move the camera
		transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
	// need to impliment for left as well


/* i believe this is unneeded with vector3 lerp added ~fredddie
 * 
	// Update is called once per frame
	void Update(){
		transform.position = new Vector3(playerTransform.position.x,playerTransform.position.y, -10);
	}
*/
}
