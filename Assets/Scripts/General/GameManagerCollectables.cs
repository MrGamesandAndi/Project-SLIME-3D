using TMPro;
using UnityEngine;

public class GameManagerCollectables : MonoBehaviour
{
	#region Variables
	private GameManagerMaster gameManagerMaster;

	public float currentAtomCounter;
	public int currentBoltCounter;
	public TextMeshProUGUI atomText;
	public TextMeshProUGUI boltText;
	public static GameManagerCollectables instance;
	#endregion

	#region Basic Methods
	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		CollectAtom(gameManagerMaster.dataController.TotalAtoms);
	}

	private void OnEnable()
	{
		SetInitialReferences();
		gameManagerMaster.AtomEvent += CollectAtom;
		gameManagerMaster.GearEvent += CollectBolt;
	}

	private void OnDisable()
	{
		gameManagerMaster.AtomEvent -= CollectAtom;
		gameManagerMaster.GearEvent -= CollectBolt;
	}
	#endregion

	#region Custom Methods
	private void SetInitialReferences()
	{
		gameManagerMaster = GetComponent<GameManagerMaster>();
	}

	public void CollectAtom(float value)
	{
		currentAtomCounter += value;
		atomText.text = "";
		atomText.text = ((int)currentAtomCounter).ToString();		
	}

	public float GetTotalAtomQuantity()
	{
		return currentAtomCounter;
	}

	public void CollectBolt()
	{
		currentBoltCounter += 1;
		boltText.text = "";
		boltText.text = currentBoltCounter.ToString();
	}

	public void DeleteBolts()
	{
		currentBoltCounter -= 5;

		if (currentBoltCounter < 0)
		{
			currentBoltCounter = 0;
		}

		boltText.text = "";
		boltText.text = currentBoltCounter.ToString();
	}
	#endregion
}
