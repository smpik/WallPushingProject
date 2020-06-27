using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour
{
	private enum HUMAN_TYPE
	{
		GIRL = 0,
		BOY,
		BAD,
		GAY,
	}
	private const int HUMAN_TYPE_MAX = 4;

	private const string NORMAL_HUMAN_PATH = "Prefabs/Human/Normal";
	private const string CRASH_HUMAN_PATH = "Prefabs/Human/Crash";
	private const string HEART_HUMAN_PATH = "Prefabs/Human/Heart";
	private const string SURPRISED_HUMAN_PATH = "Prefabs/Human/Surprised";
	private const string ANGRY_HUMAN_PATH = "Prefabs/Human/Angry";
	private const int GIRL_PREFAB_MAX = 9;
	private const int BOY_PREFAB_MAX = 3;
	private const int BAD_PREFAB_MAX = 3;
	private const int GAY_PREFAB_MAX = 6;

	private GameObject[] GirlNormalPrefabs = new GameObject[GIRL_PREFAB_MAX];
	private GameObject[] GirlCrashPrefabs = new GameObject[GIRL_PREFAB_MAX];
	private GameObject[] GirlHeartPrefabs = new GameObject[GIRL_PREFAB_MAX];
	private GameObject[] BoyNormalPrefabs = new GameObject[BOY_PREFAB_MAX];
	private GameObject[] BoyCrashPrefabs = new GameObject[BOY_PREFAB_MAX];
	private GameObject[] BoySurprisedPrefabs = new GameObject[BOY_PREFAB_MAX];
	private GameObject[] BadNormalPrefabs = new GameObject[BAD_PREFAB_MAX];
	private GameObject[] BadCrashPrefabs = new GameObject[BAD_PREFAB_MAX];
	private GameObject[] BadAngryPrefabs = new GameObject[BAD_PREFAB_MAX];
	private GameObject[] GayNormalPrefabs = new GameObject[GAY_PREFAB_MAX];
	private GameObject[] GayCrashPrefabs = new GameObject[GAY_PREFAB_MAX];
	private GameObject[] GayHeartPrefabs = new GameObject[GAY_PREFAB_MAX];

	//==============================================================================//
	// 初期化																		//
	//==============================================================================//
	void Start()
    {
		setHumanPrefabs();
	}
	private void setHumanPrefabs()
	{
		setGirlPrefabs();
		setBoyPrefabs();
		setBadPrefabs();
		setGayPrefabs();
	}
	private void setGirlPrefabs()
	{
		for (int i = 0; i < GIRL_PREFAB_MAX; i++)
		{
			GirlNormalPrefabs[i] = (GameObject)Resources.Load(NORMAL_HUMAN_PATH + "/Girl_" + i + "_Normal");
			GirlCrashPrefabs[i] = (GameObject)Resources.Load(CRASH_HUMAN_PATH + "/Girl_" + i + "_Crash");
			GirlHeartPrefabs[i] = (GameObject)Resources.Load(HEART_HUMAN_PATH + "/Girl_" + i + "_Heart");
		}
	}
	private void setBoyPrefabs()
	{
		for (int i = 0; i < BOY_PREFAB_MAX; i++)
		{
			BoyNormalPrefabs[i] = (GameObject)Resources.Load(NORMAL_HUMAN_PATH + "/Boy_" + i + "_Normal");
			BoyCrashPrefabs[i] = (GameObject)Resources.Load(CRASH_HUMAN_PATH + "/Boy_" + i + "_Crash");
			BoySurprisedPrefabs[i] = (GameObject)Resources.Load(SURPRISED_HUMAN_PATH + "/Boy_" + i + "_Surprised");
		}
	}
	private void setBadPrefabs()
	{
		for (int i = 0; i < BAD_PREFAB_MAX; i++)
		{
			BadNormalPrefabs[i] = (GameObject)Resources.Load(NORMAL_HUMAN_PATH + "/Bad_" + i + "_Normal");
			BadCrashPrefabs[i] = (GameObject)Resources.Load(CRASH_HUMAN_PATH + "/Bad_" + i + "_Crash");
			BadAngryPrefabs[i] = (GameObject)Resources.Load(ANGRY_HUMAN_PATH + "/Bad_" + i + "_Angry");
		}
	}
	private void setGayPrefabs()
	{
		for (int i = 0; i < GAY_PREFAB_MAX; i++)
		{
			GayNormalPrefabs[i] = (GameObject)Resources.Load(NORMAL_HUMAN_PATH + "/Gay_" + i + "_Normal");
			GayCrashPrefabs[i] = (GameObject)Resources.Load(CRASH_HUMAN_PATH + "/Gay_" + i + "_Crash");
			GayHeartPrefabs[i] = (GameObject)Resources.Load(HEART_HUMAN_PATH + "/Gay_" + i + "_Heart");
		}
	}

	//==============================================================================//
	// 外部公開																		//
	//==============================================================================//
	public GameObject GetNextHuman()//ランダムで生成する人間を決める
	{
		GameObject ret = (GameObject)Resources.Load(NORMAL_HUMAN_PATH + "/Girl_0_Normal");//エラー回避。なんでもいい。
		int humanId = 0;

		HUMAN_TYPE humanType = (HUMAN_TYPE)Random.Range(0, HUMAN_TYPE_MAX);

		switch (humanType)
		{
			case HUMAN_TYPE.GIRL:
				humanId = Random.Range(0, GIRL_PREFAB_MAX);
				ret = GirlNormalPrefabs[humanId];
				break;
			case HUMAN_TYPE.BOY:
				humanId = Random.Range(0, BOY_PREFAB_MAX);
				ret = BoyNormalPrefabs[humanId];
				break;
			case HUMAN_TYPE.BAD:
				humanId = Random.Range(0, BAD_PREFAB_MAX);
				ret = BadNormalPrefabs[humanId];
				break;
			case HUMAN_TYPE.GAY:
				humanId = Random.Range(0, GAY_PREFAB_MAX);
				ret = GayNormalPrefabs[humanId];
				break;
			default:
				break;
		}

		return ret;
	}
}
