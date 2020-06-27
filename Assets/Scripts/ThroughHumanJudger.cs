using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEFINITION_COMMON_CONST;

public class ThroughHumanJudger : MonoBehaviour
{
	private const string SPLIT_CHAR = "_";
	private const string BAD = "Bad";
	private const string DETECTED = "Detected";

	private BadThroughDataManager badThroughDataManager;

	void Start()
	{
		badThroughDataManager = GameObject.Find("DataManager").GetComponent<BadThroughDataManager>();
	}

	private void researchHumanType(GameObject human)
	{
		/* GameObject名を取得	*/
		string humanObjectName = human.name;
		
		/* GameObject名から人間の種類を割り出す	*/
		string[] words = humanObjectName.Split(new string[] { SPLIT_CHAR }, System.StringSplitOptions.None);
		string humanType = words[0];//最初の区切りまでが人間の種類を表してる + 検出済みなら名前を「検出済み」としてある
		
		/* 不良ならカウント	*/
		if(humanType == BAD)
		{
			badThroughDataManager.NotifyBadThrough();//不良をスルーしてしまったことを報告
			human.name = DETECTED + human.name;//カウントした不良は名前を変えとく(これで同じ不良を複数回カウントすることはない)
		}
	}
	//==============================================================================//
	// 外部公開																		//
	//==============================================================================//
	public void NotifyDetectedThroughHuman(GameObject throughHuman)
	{
		researchHumanType(throughHuman);
	}
}
