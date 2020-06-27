using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEFINITION_COMMON_CONST;

public class WallPushingResultJudger : MonoBehaviour
{
	private const float JUDGE_THRESHOLD_1 = 2;//バッドとナイスの閾値
	private const float JUDGE_THRESHOLD_2 = 5;//ナイスと微妙の閾値

	private const float DISPLAY_HALF_LENGTH = 15;

	private int RestartWaitTimer;

	private AutoScrollController AutoScrollControllerInstance;
	private GameStateManager GameStateManagerInstance;
	private UiController UiControllerInstance;

    // Start is called before the first frame update
    void Start()
    {
		AutoScrollControllerInstance = GameObject.Find("Main Camera").GetComponent<AutoScrollController>();
		GameStateManagerInstance = GameObject.Find("GameManager").GetComponent<GameStateManager>();
		UiControllerInstance = GameObject.Find("UiController").GetComponent<UiController>();

		RestartWaitTimer = 0;
    }
	void Update()
	{
		if(RestartWaitTimer>0)
		{
			RestartWaitTimer--;
			if(RestartWaitTimer<=0)
			{
				GameStateManagerInstance.SetGameState(GAME_STATE.PLAY);//再始動
				UiControllerInstance.InactivateTempResultCanvas();//壁ドン結果を消す
			}
		}
	}

	public void JudgeWallPushingResult(float handPosX)//壁ドン結果を判定する(HandGenerator.csから呼ばれる)
	{
		if(isHumanInDisp())//そもそも人間が画面内にいるか(壁ドンなのか)
		{
			DEFINITION_COMMON_CONST.WALL_PUSHING_RESULT result = WALL_PUSHING_RESULT.OK;//デフォルトは微妙判定(なんでもいい)

			float humanPosX = GameObject.FindGameObjectWithTag("Human").transform.position.x;//人間の座標を取得(コードを見てわかる通り人間が二人いてはいけない)
			float distance = Mathf.Abs(handPosX - humanPosX);//人間と手のあいだの距離を計算

			if( (0<=distance) && (distance<=JUDGE_THRESHOLD_1) )//近いか
			{
				result = WALL_PUSHING_RESULT.BAD;//humanTypeごとに判定
			}
			else if( (JUDGE_THRESHOLD_1<=distance) && (distance<=JUDGE_THRESHOLD_2) )//中間か
			{
				result = WALL_PUSHING_RESULT.NICE;
			}
			else//遠いか
			{
				result = WALL_PUSHING_RESULT.OK;
			}
			Debug.Log(result);
			GameObject.FindGameObjectWithTag("Human").name = "Pushed" + GameObject.FindGameObjectWithTag("Human").name;
			GameStateManagerInstance.SetGameState(GAME_STATE.WALL_PUSHING_RESULT_DISP);//GameStateを壁ドン結果表示に更新する。
			RestartWaitTimer = 30;//再始動までの待ち時間タイマをセット
			UiControllerInstance.ActivateTempResultCanvas();//壁ドン結果表示処理
			AutoScrollControllerInstance.UpdateNowTimeFactor(result);//AutoScrollの係数の処理
		}
	}
	private bool isHumanInDisp()
	{
		bool ret = false;

		if(GameObject.FindGameObjectWithTag("Human") == true)
		{
			float humanPosX = GameObject.FindGameObjectWithTag("Human").transform.position.x;
			float cameraPosX = GameObject.Find("Main Camera").transform.position.x;

			if( (humanPosX <= cameraPosX + DISPLAY_HALF_LENGTH)//右端より内側
				|| (humanPosX >= cameraPosX - DISPLAY_HALF_LENGTH) )//左端より内側
			{
				ret = true;
			}
		}

		return ret;
	}
}
