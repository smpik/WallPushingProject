using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissDataManager : MonoBehaviour
{
	private const int MISS_LIMIT_NUM = 3;

	private int MissCount;

	private UiController UiControllerInstance;

    void Start()
    {
		MissCount = 0;
		UiControllerInstance = GameObject.Find("UiController").GetComponent<UiController>();
    }

	public void AddMissCount(int addValue)
	{
		MissCount += addValue;
		gameOverCheck();
		UiControllerInstance.UpdateMissCountValueText(MissCount);//ミス回数の表示更新要求
	}

	private void gameOverCheck()
	{
		if(MissCount >= MISS_LIMIT_NUM)
		{
			UiControllerInstance.ActivateFinalResultCanvas();
		}
	}
}
