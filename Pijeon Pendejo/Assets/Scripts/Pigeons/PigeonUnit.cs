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

	public static void SetAsMasterPigeon(PigeonUnit unit)
	{
		MasterPigeon = unit.gameObject;
	}

	public static void SetAsMasterPigeon(GameObject go)
	{
		MasterPigeon = go;
	}
}
