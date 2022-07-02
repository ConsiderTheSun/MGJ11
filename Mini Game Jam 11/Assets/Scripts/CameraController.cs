using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Set in Inspector")]
	public Transform playerTransform;
	public Vector3 cameraOffset;
	public float cameraSpeed = 0.1f;
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


/* i believe this is unneeded with vector3 lerp added ~fredddie
 * 
	// Update is called once per frame
	void Update(){
		transform.position = new Vector3(playerTransform.position.x,playerTransform.position.y, -10);
	}
*/
}
