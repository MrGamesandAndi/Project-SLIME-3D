using System.Collections;
using TMPro;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
	#region Variables
	private GameObject pressZone;
	private TextMeshProUGUI timerText;
	private DisableIfFarAway despawner;

	public float time = 5f;
	public GameObject timerObjects;
	#endregion

	#region Basic Methods
	private void Start()
	{
		TimerEvent.current.OnTimerTriggerEnter += OnTimerStarted;
		pressZone = transform.Find("Press_Zone").gameObject;
		timerText = GameObject.Find("Timer_Text").GetComponent<TextMeshProUGUI>();
		despawner = GetComponentInChildren<DisableIfFarAway>();
	}

	private void OnDestroy()
	{
		TimerEvent.current.OnTimerTriggerEnter -= OnTimerStarted;
	}
	#endregion

	#region Custom Methods
	private void OnTimerStarted()
	{
		StopCoroutine("Countdown");
		StartCoroutine("Countdown");
	}

	private IEnumerator Countdown()
	{
		pressZone.transform.position = new Vector3(pressZone.transform.position.x, pressZone.transform.position.y - 0.29f, pressZone.transform.position.z);
		timerObjects.SetActive(true);
		float normalizedTime = 0;

		while (normalizedTime <= time)
		{
			normalizedTime += Time.deltaTime;
			var integer = (int)normalizedTime;
			timerText.text = "";
			timerText.text = (time - Mathf.RoundToInt(integer)).ToString();
			yield return null;
		}

		pressZone.transform.position = new Vector3(pressZone.transform.position.x, pressZone.transform.position.y + 0.29f, pressZone.transform.position.z);
		timerObjects.SetActive(false);
		pressZone.GetComponent<SphereCollider>().enabled = true;
		timerText.text = "";
	}
	#endregion
}
