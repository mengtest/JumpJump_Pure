using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{


	public Transform m_target;
	public float m_distance = 10.0f;
	public float m_height = 5.0f;
	public float m_heightDamping = 2.0f;
	public float m_rotationDamping = 3.0f;
	public CameraFollowType m_type = CameraFollowType.BY_TARGET_DIR;
	public float specEulerAngleY = 0;
	public bool isFixHeight = false;

	void Awake ()
	{
		Init ();
	}
	
	void LateUpdate ()
	{
		if (!m_target)
			return;
		UpdatePotAndDir ();
	
	}

	void ComputePosition (float eulerAngleY)
	{
		// Calculate the current rotation angles
		float wantedRotationAngle = eulerAngleY;
		float wantedHeight = m_target.position.y + m_height;
		
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, m_rotationDamping * Time.deltaTime);
		
		if (!isFixHeight) {
			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, m_heightDamping * Time.deltaTime);
		} else {
			ComputeAdjustPot ();
			if (m_Adjusting) {
				currentHeight = Mathf.Lerp (currentHeight, m_Adjust_WantedHeight, m_heightDamping * Time.deltaTime);
			}
		}
		
		
		// Convert the angle into a rotation
		m_currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = m_target.position; 
		transform.position -= m_currentRotation * Vector3.forward * m_distance;
		
		// Set the height of the camera
		Vector3 tmp = transform.position;
		tmp.y = currentHeight;
		transform.position = tmp;

//		Debug.Log(" target.postion="+target.position+" currentRotationAngle="+currentRotationAngle);
	}

	Quaternion m_currentRotation;

	void UpdatePotAndDir ()
	{
		switch (m_type) {
		case CameraFollowType.BY_TARGET_DIR:
			ComputePosition (m_target.eulerAngles.y);
			transform.LookAt (m_target);
			break;
		case CameraFollowType.BY_SELF_DIR:
			ComputePosition (transform.eulerAngles.y);
			break;
		case CameraFollowType.BY_SPEC_DIR:
			ComputePosition (specEulerAngleY);
			transform.rotation = m_currentRotation;
			break;
		}
	}

	float m_Dy_Camera_Target = 0;
	float m_AdjustWindow_ToUpDst_Ratio = 0.2f;
	float m_AdjustWindow_ToDownDst_Ratio = 0.2f;
	bool m_Adjusting;
	float m_Adjust_WantedHeight;

	void ComputeAdjustPot ()
	{
		Vector3 screenPot = Camera.main.WorldToScreenPoint (m_target.position);
		float screenHeight = UnityEngine.Screen.height;
		if (screenPot.y < screenHeight * m_AdjustWindow_ToDownDst_Ratio 
			|| screenPot.y > screenHeight * (1 - m_AdjustWindow_ToUpDst_Ratio)) {
			m_Adjust_WantedHeight = m_target.position.y + m_Dy_Camera_Target;
			m_Adjusting = true;
		} else {
			m_Adjusting = false;
		}
	}

	void Init ()
	{
		m_Dy_Camera_Target = transform.position.y - m_target.position.y;

	}


}

public enum CameraFollowType
{
	BY_TARGET_DIR=0,
	BY_SELF_DIR=1,
	BY_SPEC_DIR=2
}