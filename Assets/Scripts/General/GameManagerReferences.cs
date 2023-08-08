using TMPro;
using UnityEngine;

public class GameManagerReferences : MonoBehaviour
{
	#region Variables
	public string playerTag;
	public static string PLAYER_TAG;
	public string enemyTag;
	public static string ENEMY_TAG;
	public static GameObject player;
	public TextMeshProUGUI atomText;
	#endregion

	#region Basic Methods
	private void OnEnable()
	{
		if (playerTag == "" || enemyTag == "")
		{
			return;
		}

		PLAYER_TAG = playerTag;
		ENEMY_TAG = enemyTag;
		player = GameObject.FindGameObjectWithTag(playerTag);
	}
	#endregion

	#region Custom Methods

	#endregion
}
