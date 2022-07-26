using UnityEngine;
using System.Collections;

public class ObjectRotator : MonoBehaviour
{
	float rotationSpeed = 0.2f;

	void OnMouseDrag()
	{
		float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
		float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
		if (Input.touchCount > 0)
		{
			XaxisRotation = 0.002f * Input.touches[0].deltaPosition.x;
			YaxisRotation = 0.002f * Input.touches[0].deltaPosition.y;
		}

		transform.Rotate(Vector3.down, XaxisRotation);
		transform.Rotate(Vector3.right, YaxisRotation);
	}

}