using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public void TransitTitleScene()
	{
		SceneManager.LoadScene("TitleScene");
	}

	public void TransitPrivacyPolicyScene()
	{
		SceneManager.LoadScene("PrivacyPolicyScene");
	}

	public void TransitMainScene()
	{
		SceneManager.LoadScene("MainScene");
	}
}
