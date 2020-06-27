using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopularityDataManager : MonoBehaviour
{
	private int Popularity;
	private UiController UiControllerInstance;

    void Start()
    {
		UiControllerInstance = GameObject.Find("UiController").GetComponent<UiController>();
		Popularity = 0;
    }

	public void AddPopularity(int addValue)
	{
		Popularity += addValue;
		UiControllerInstance.UpdatePopularityValueText(Popularity);
	}

	public int GetPopularity()
	{
		return Popularity;
	}

	public int ReadHighScore()
	{
		int ret = 0;

		return ret;
	}
	public void SaveHighScore()
	{

	}
}
