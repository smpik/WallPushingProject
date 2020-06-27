using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadThroughDataManager : MonoBehaviour
{
	private const int MAX = 2;
	private int BadThroughCount;

    // Start is called before the first frame update
    void Start()
    {
		BadThroughCount = 0;
    }

	public void NotifyBadThrough()
	{
		BadThroughCount++;//カウント

		if(BadThroughCount>MAX)
		{
			//ゲームオーバー
			Debug.Log("ゲームオーバー");
		}
	}
}
