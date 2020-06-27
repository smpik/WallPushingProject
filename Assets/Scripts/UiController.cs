using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
	private GameObject FinalResultCanvas;
	private GameObject TempResultCanvas;
	private Text PopularityValueText;
	private Text MissCountValueText;
	private Slider MustleGauge;

    // Start is called before the first frame update
    void Start()
    {
		FinalResultCanvas = GameObject.Find("FinalResultCanvas");
		TempResultCanvas = GameObject.Find("TempResultCanvas");
		PopularityValueText = GameObject.Find("PopularityValueText").GetComponent<Text>();
		MissCountValueText = GameObject.Find("MissCountValueText").GetComponent<Text>();
		MustleGauge = GameObject.Find("MustleGauge").GetComponent<Slider>();

		inactivationByInit();
		InactivateTempResultCanvas();
    }
	private void inactivationByInit()
	{
		inactivateFinalResultCanvas();
	}

	public void UpdatePopularityValueText(int value)
	{
		PopularityValueText.text = value.ToString();
	}
	public void UpdateMissCountValueText(int value)
	{
		MissCountValueText.text = value.ToString();
	}
	public void UpdateMustleGauge(float value)
	{
		MustleGauge.value = value;
	}

	public void ActivateFinalResultCanvas()
	{
		FinalResultCanvas.SetActive(true);
	}
	private void inactivateFinalResultCanvas()
	{
		FinalResultCanvas.SetActive(false);
	}

	public void ActivateTempResultCanvas()
	{
		TempResultCanvas.SetActive(true);
	}
	public void InactivateTempResultCanvas()
	{
		TempResultCanvas.SetActive(false);
	}
}
