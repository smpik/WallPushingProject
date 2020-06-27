using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DEFINITION_COMMON_CONST;

public class GameStateManager : MonoBehaviour
{
	private GAME_STATE GameState;

    void Start()
    {
		GameState = GAME_STATE.PLAY;
    }

	public void SetGameState(GAME_STATE newState)
	{
		GameState = newState;
	}
	public GAME_STATE GetGameState()
	{
		return GameState;
	}
}
