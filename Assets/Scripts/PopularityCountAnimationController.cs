using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopularityCountAnimationController : MonoBehaviour
{
	private const int DURATION = 1;

	private PopularityDataManager PopularityDataManagerInstance;
	private UiController UiControllerInstance;

    // Start is called before the first frame update
    void Start()
    {
		PopularityDataManagerInstance = GameObject.Find("DataManager").GetComponent<PopularityDataManager>();
		UiControllerInstance = GameObject.Find("UiController").GetComponent<UiController>();
    }

	public void PlayRollCountAnimation(int addValue)
	{
		StartCoroutine(RollCountAnimation(addValue));
	}
	private IEnumerator RollCountAnimation(int addValue)
	{
		float startTime = Time.time;
		float endTime = startTime + DURATION;
		float startScore = PopularityDataManagerInstance.GetPopularity();
		float endScore = startScore + addValue;

		do
		{
			float timeRate = (Time.time - startTime) / DURATION;
			float updateValue = (float)((endScore - startScore) * timeRate + startScore);
			UiControllerInstance.UpdatePopularityValueText((int)updateValue);//人気ptテキスト更新要求
			yield return null;
		} while (Time.time < endTime);

		PopularityDataManagerInstance.AddPopularity(addValue);
	}
}
