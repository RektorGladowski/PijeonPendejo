using System;
using UnityEngine;

public class PlayerBanketon : MonoBehaviour
{
	public static PlayerBanketon instance;
	public Action OnTransactionCommited;

	public int initialPlayerPigeons;
	private int playerPigeons;

	private void Awake()
	{
		instance = this;
		playerPigeons = initialPlayerPigeons;
        DontDestroyOnLoad(this.gameObject);
    }

	public bool CanPayWithPigeons(int cost)
	{
		if (cost <= playerPigeons)
		{
			Debug.Log("Player has enough pigeons");
			return true;
		}
		else
		{
			Debug.Log("Player has insufficient pigeons");
			return false;
		}
	}

	public void PayWithPigeons(int cost)
	{
		if (cost <= playerPigeons)
		{
			playerPigeons -= cost;
			OnTransactionCommited?.Invoke();
		}
		else
		{
			Debug.LogError("You forgot to check your wallet before paying. Use CanPayWithPigeons to check wallet and show something appropriate");
		}
	}

	public int GetPlayerPigeonsInt()
	{
		return playerPigeons;
	}

	public string GetPlayerPigeonsString()
	{
		return playerPigeons.ToString();
	}
}
