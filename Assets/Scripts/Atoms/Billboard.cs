using UnityEngine;

public class Billboard : MonoBehaviour
{
	#region Variables
	public Camera camera;

	public bool useStaticBillboard;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	private void LateUpdate()
	{
		if (!useStaticBillboard)
		{
			transform.LookAt(camera.transform);
		}
		else
		{
			transform.rotation = camera.transform.rotation;
		}

		transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
	}
	#endregion

	#region Custom Methods

	#endregion
}
