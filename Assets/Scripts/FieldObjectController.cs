using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEFINITION_COMMON_CONST;

public class FieldObjectController: MonoBehaviour
{
	private const int WALL_PREFAB_MAX = 3;
	private const string CURRENT_WALL_NAME = "CurrentWall";
	private const string NEXT_WALL_NAME = "NextWall";
	private const string OLD_WALL_NAME = "OldWall";
	private const float MARGIN_FOR_POS_JUDGE = 0.5f;
	private const float WALL_LENGTH = 25;
	private const float DISPLAY_HALF_LENGTH = 15;

	private const float POSX_HUMAN_GENERATE_OFFSET = 13.5f;
	private const float POSY_HUMAN_GENERATE_DEFALUT = -1.75f;
	private const float POSZ_HUMAN_GENERATE_DEFAULT = -0.1f;

	private GameObject MainCamera;
	private GameObject[] WallPrefabs = new GameObject[WALL_PREFAB_MAX];
	private int HumanGenerateWaitTimer;

	private GameStateManager GameStateManagerInstance;
	private HumanManager HumanManagerInstance;
	private ThroughHumanJudger ThroughHumanJudgerInstance;
	//==============================================================================//
	// 初期化																		//
	//==============================================================================//
	void Start()
    {
		MainCamera = GameObject.Find("Main Camera");
		GameStateManagerInstance = GameObject.Find("GameManager").GetComponent<GameStateManager>();
		HumanManagerInstance = GameObject.Find("DataManager").GetComponent<HumanManager>();
		ThroughHumanJudgerInstance = GameObject.Find("GameManager").GetComponent<ThroughHumanJudger>();
		HumanGenerateWaitTimer = 100;

		setWallPrefabs();
    }
	private void setWallPrefabs()
	{
		for(int i = 0; i < WALL_PREFAB_MAX; i++)
		{
			WallPrefabs[i] = (GameObject)Resources.Load("Prefabs/Wall/WallPrefab"+i);
		}
	}

	//==============================================================================//
	// 周期																			//
	//==============================================================================//
	void Update()
    {
		if(GameStateManagerInstance.GetGameState() == GAME_STATE.PLAY)
		{
			updateFieldObject();
			countHumanGenerateWaitTimer();
			observeFadeOutHuman();
		}
    }
	private void updateFieldObject()
	{
		if ((GameObject.Find(NEXT_WALL_NAME) != true) && (judgeGenerateNextWall() == true))
		{
			generateNextWall();
			deleteOldWall();
		}
	}
	private void countHumanGenerateWaitTimer()
	{
		if(HumanGenerateWaitTimer > 0)
		{
			HumanGenerateWaitTimer--;
			if(HumanGenerateWaitTimer<=0)
			{
				generateNextHuman();
			}
		}
	}
	private void observeFadeOutHuman()
	{
		if(GameObject.FindGameObjectWithTag("Human") == true)//人間がいるか
		{
			GameObject targetHuman = GameObject.FindGameObjectWithTag("Human");//見つかった人間を取得
			float humanPosX = targetHuman.transform.position.x;//見つかった人間の座標を取得
			if (humanPosX < MainCamera.transform.position.x)//そもそも通り過ぎてるか
			{
				ThroughHumanJudgerInstance.NotifyDetectedThroughHuman(targetHuman);//通過したことを報告

				if (humanPosX < (MainCamera.transform.position.x - DISPLAY_HALF_LENGTH))//しかも人間の座標が画面左外のとき = フェードアウトしてるとき
				{
					Destroy(targetHuman);
					HumanGenerateWaitTimer = Random.Range(100, 200);//次の人間の生成待ちタイマを起動する
				}
			}
		}
	}
	private bool judgeGenerateNextWall()//次の壁を生成するかどうかの判定を行う
	{
		bool ret = false;

		float mainCameraPos = MainCamera.transform.position.x;
		float currentWallPos = GameObject.Find(CURRENT_WALL_NAME).transform.position.x;
		if(Mathf.Abs(mainCameraPos - currentWallPos) < MARGIN_FOR_POS_JUDGE)//MainCameraのx座標が現在の壁の中心座標に来たら
		{
			ret = true;
		}

		return ret;
	}
	private void generateNextWall()//次の壁を生成する
	{
		/* 生成する壁を決める	*/
		GameObject createNextWallPrefab = WallPrefabs[Random.Range(0,WALL_PREFAB_MAX)];

		/* 生成場所を計算する	*/
		GameObject currentWall = GameObject.Find(CURRENT_WALL_NAME);
		Vector3 nextWallPos = new Vector3(0, currentWall.transform.position.y, currentWall.transform.position.z);//y軸、z軸の値を引き継ぐ
		float nextWallLeftEdgePosX = currentWall.transform.position.x + WALL_LENGTH;//次の壁の左端になる座標を計算
		nextWallPos.x = nextWallLeftEdgePosX + WALL_LENGTH;//次の壁の中心になる座標を計算

		/* 生成する	*/
		GameObject createdNextWall = Instantiate(createNextWallPrefab, nextWallPos, createNextWallPrefab.transform.rotation);
		Debug.Log("生成されました");

		/* オブジェクト名を設定する	*/
		createdNextWall.name = NEXT_WALL_NAME;
	}
	private void deleteOldWall()
	{
		Destroy(GameObject.Find(OLD_WALL_NAME));//CurrentWallを削除する

		/* 世代交代	*/
		GameObject.Find(CURRENT_WALL_NAME).name = OLD_WALL_NAME;
		GameObject.Find(NEXT_WALL_NAME).name = CURRENT_WALL_NAME;
	}
	private void generateNextHuman()
	{
		/* 生成する人間を決める	*/
		GameObject nextHumanPrefab = HumanManagerInstance.GetNextHuman();//生成時はNormal固定

		/* 生成場所を計算する	*/
		Vector3 nextHumanPos = new Vector3(0, POSY_HUMAN_GENERATE_DEFALUT, POSZ_HUMAN_GENERATE_DEFAULT);//生成場所の定義
		float nowDisplayCenterPosX = GameObject.Find("Main Camera").transform.position.x;//現在の画面中央の場所
		nextHumanPos.x = nowDisplayCenterPosX + POSX_HUMAN_GENERATE_OFFSET;//オフセットして画面右外にする

		/* 生成する	*/
		GameObject createdHuman = Instantiate(nextHumanPrefab, nextHumanPos, nextHumanPrefab.transform.rotation);

		/* 補正	*/
		createdHuman.layer = 9;
		/*
		Vector3 newPos = createdHuman.transform.position;
		newPos.y = -1;
		newPos.z = -3;
		createdHuman.transform.position = newPos;
		createdHuman.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		*/
		Debug.Log("生成距離" + (nextHumanPos.x - nowDisplayCenterPosX) );
		/* オブジェクト名を設定する	*/
	}
}
