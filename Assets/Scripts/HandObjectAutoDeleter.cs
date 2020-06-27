using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEFINITION_COMMON_CONST;

public class HandObjectAutoDeleter : MonoBehaviour
{
	private const int LIFE_TIME = 50;	//HandObjectを消滅させるまでの時間

	private int LifeTimer = 0;  //HandObjectを消滅させるまでの時間を保持する

	private GameStateManager GameStateManagerInstance;

    // Start is called before the first frame update
    void Start()
    {
		//ここでLifeTimerの初期化をしないこと(HandGeneratorからのSetLifeTimer()よりあとにStart()が実行されるらしく、ここで初期化すると0に上書きされてしまう)
		GameStateManagerInstance = GameObject.Find("GameManager").GetComponent<GameStateManager>();//この処理は後から実行されてもよいので。
    }

    // Update is called once per frame
    void Update()
    {
        if(GameStateManagerInstance.GetGameState() == GAME_STATE.PLAY)//PLAYのときのみ
		{
			countLifeTimer();
		}
    }
	private void countLifeTimer()
	{
		if (LifeTimer > 0)
		{
			LifeTimer--;

			if (LifeTimer <= 0)
			{
				deleteHandObject();
			}
		}
	}
	private void deleteHandObject()
	{
		Destroy(gameObject);//自分を削除
	}

	public void SetLifeTimer()
	{
		LifeTimer = LIFE_TIME;
	}
}
