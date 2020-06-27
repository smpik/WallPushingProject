using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustleDataManager : MonoBehaviour
{
	private const float MUSTLE_CYCLIC_DECREASE_VALUE = 0.0001f;
	private float Mustle;

	private UiController UiControllerInstance;

	// Start is called before the first frame update
	void Start()
    {
		UiControllerInstance = GameObject.Find("UiController").GetComponent<UiController>();
		Mustle = 0;
    }

	void Update()
	{
		if(Mustle > 0)
		{
			Mustle -= MUSTLE_CYCLIC_DECREASE_VALUE;
			UiControllerInstance.UpdateMustleGauge(Mustle);
		}
	}

	public void AddMustle(float addValue)
	{
		Mustle += addValue;
		UiControllerInstance.UpdateMustleGauge(Mustle);
	}
}
