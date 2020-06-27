using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashEffectController : MonoBehaviour
{
	private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
		animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if (isAnimationFinished() && isCrashEffect())//エフェクトアニメーションが終了しているなら
		{
			Destroy(gameObject);//自分を削除
		}
	}

	private bool isCrashEffect()//このオブジェクトの現在再生中のアニメーションがCrashEffectなら真を返す
	{
		bool ret = false;

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Crash"))
		{
			ret = true;
		}

		return ret;
	}

	private bool isAnimationFinished()
	{
		bool ret = false;

		if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
		{
			ret = true;
		}

		return ret;
	}
}
