using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEFINITION_COMMON_CONST;

public class AutoScrollController : MonoBehaviour
{
	private const float VALUE_AUTO_SCROLL = 0.1f;
	private const float ADD_VALUE_FOR_FACTOR = 1;

	private float NowTimeFactor;

	private GameObject MainCamera;
	private GameStateManager GameStateManagerInstance;

    // Start is called before the first frame update
    void Start()
    {
		MainCamera = GameObject.Find("Main Camera");
		GameStateManagerInstance = GameObject.Find("GameManager").GetComponent<GameStateManager>();

		NowTimeFactor = ADD_VALUE_FOR_FACTOR;//初期値1
    }

    // Update is called once per frame
    void Update()
    {
        if(GameStateManagerInstance.GetGameState() == GAME_STATE.PLAY)//PLAY中のみ
		{
			autoScroll();
		}
    }
	private void autoScroll()/* カメラをx軸正方向に進める	*/
	{
		Vector3 newPos = MainCamera.transform.position;
		newPos.x += NowTimeFactor * VALUE_AUTO_SCROLL;
		MainCamera.transform.position = newPos;
	}

	public void UpdateNowTimeFactor(WALL_PUSHING_RESULT result)
	{
		switch(result)
		{
			case WALL_PUSHING_RESULT.BAD:
				break;//壁ドン失敗なら係数変えない
			case WALL_PUSHING_RESULT.NICE:
			case WALL_PUSHING_RESULT.OK:
				NowTimeFactor += ADD_VALUE_FOR_FACTOR;//成功したときだけ加算する
				break;
			default:
				break;
		}
	}
	public void test(int i)//デバッグキャンバスからデバッグするためだけの関数(enum型だとボタンに設定できないっぽい)
	{
		UpdateNowTimeFactor((WALL_PUSHING_RESULT)i);
	}
}
