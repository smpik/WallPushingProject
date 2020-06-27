using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEFINITION_COMMON_CONST;

public class HandGenerator : MonoBehaviour
{
	private GameObject RightHandPrefab;
	private GameObject LeftHandPrefab;

	private WallPushingResultJudger WallPushingResultJudgerInstance;
	private GameStateManager GameStateManagerInstance;

    void Start()
    {
		RightHandPrefab = (GameObject)Resources.Load("Prefabs/Hand/RightHandPrefab");
		LeftHandPrefab = (GameObject)Resources.Load("Prefabs/hand/LeftHandPrefab");

		WallPushingResultJudgerInstance = GameObject.Find("GameManager").GetComponent<WallPushingResultJudger>();
		GameStateManagerInstance = GameObject.Find("GameManager").GetComponent<GameStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
		if(GameStateManagerInstance.GetGameState() == GAME_STATE.PLAY)//PLAY中のときだけ
		{
			observeInput();//入力を監視する(受け付ける)
		}
	}

	private void observeInput()//タップ/クリック入力があった時の処理
	{
		if (Input.GetMouseButtonDown(0) || isTap())
		{
			var tapPos = Input.mousePosition + Camera.main.transform.forward;
			tapPos.z = 10f;//z軸修正
			var createPos = Camera.main.ScreenToWorldPoint(tapPos);//生成させたい座標(タップ位置をワールド座標に変換)

			GameObject hand = getProperHand(createPos.x);//右手か左手か判定

			GameObject createdHand = Instantiate(hand, createPos, hand.transform.rotation);//生成

			createdHand.GetComponent<HandObjectAutoDeleter>().SetLifeTimer();//消滅までのカウントダウン開始

			WallPushingResultJudgerInstance.JudgeWallPushingResult(createPos.x);//壁ドンしたことを通知
		}
	}
	private bool isTap()
	{
		bool ret = false;//タップされた瞬間だったなら上書きする

		if (Input.touchCount > 0)
		{
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				ret = true;
			}
		}

		return ret;
	}
	private GameObject getProperHand(float handPosX)//どっちの手を表示するか決めて渡す。
	{
		GameObject ret = RightHandPrefab;//初期値はどっちでもいい(人間がいなかったとき、初期値の手が表示される)

		if(GameObject.FindGameObjectWithTag("Human"))
		{
			float humanPosX = GameObject.FindGameObjectWithTag("Human").transform.position.x;
			float distance = handPosX - humanPosX;
			if(distance >= 0)//右手がふさわしいとき(顔面ど真ん中のときも)
			{
				ret = RightHandPrefab;
			}
			else
			{
				ret = LeftHandPrefab;
			}
		}

		return ret;
	}
}
