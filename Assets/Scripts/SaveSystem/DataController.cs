using UnityEngine;

public class DataController : MonoBehaviour
{
	#region Variables
	private static readonly int DEFAULT_INT = 0;
	private static readonly float DEFAULT_FLOAT = 0f;
	private static readonly string DATA_TOTAL_ATOMS = "TotalAtoms";
	private static readonly string DATA_ACTIVE_CHECKPOINT = "ActiveCheckpoint";

	


	public float TotalAtoms
	{
		get
		{
			return GetFloat(DATA_TOTAL_ATOMS);
		}
		set
		{
			SaveFloat(DATA_TOTAL_ATOMS, value);
		}
	}

	public int ActiveCheckpoint
	{
		get
		{
			return GetInt(DATA_ACTIVE_CHECKPOINT);
		}
		set
		{
			SaveInt(DATA_ACTIVE_CHECKPOINT, value);
		}
	}
	#endregion

	#region Custom Methods
	private void SaveInt(string data,int value)
	{
		PlayerPrefs.SetInt(data, value);
	}

	private void SaveFloat(string data, float value)
	{
		PlayerPrefs.SetFloat(data, value);
	}

	private int GetInt(string data)
	{
		return PlayerPrefs.GetInt(data, DEFAULT_INT);
	}

	private float GetFloat(string data)
	{
		return PlayerPrefs.GetFloat(data, DEFAULT_FLOAT);
	}
	#endregion
}
