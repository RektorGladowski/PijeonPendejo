using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonUnit : MonoBehaviour
{
	private static GameObject MasterPigeon;

	public static Vector3 GetMasterPigeonPosition()
	{
		return MasterPigeon.transform.position;
	}

	private void SetAsMasterPigeon(PigeonUnit unit)
	{
		MasterPigeon = unit.gameObject;
	}
}
