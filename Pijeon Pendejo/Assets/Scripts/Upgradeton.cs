using System;
using System.Collections.Generic;
using UnityEngine;

public class Upgradeton : MonoBehaviour
{
	public static Upgradeton instance;
	public Action<UpgradeType> OnPigeonUpgraded;

	[Header("Upgrade lists")]
	public List<SpeedUpgrade> SpeedUpgrades = new List<SpeedUpgrade>();
	public List<ShitUpgrade> ShitUpgrades = new List<ShitUpgrade>();
	public List<TeamUpgrade> TeamUpgrades = new List<TeamUpgrade>();

	[Header("Other stuff")]
	public string maxUpgradeLevelText = "Max Upgrade level";

	private int speedUpLevel;
	private int shitUpLevel;
	private int teamUpLevel;

    private void Awake()
    {
		instance = this;
		SetStartingLevels();
    }

	private void SetStartingLevels()
	{
		speedUpLevel = 0;
		shitUpLevel = 0;
		teamUpLevel = 0;
	}

	public void UpgradePigeon (UpgradeType upgradeType)
	{
		switch (upgradeType)
		{
			case UpgradeType.SpeedUpgrade:
				if (speedUpLevel + 1 == SpeedUpgrades.Count) Debug.LogError("You tried to fuck something up, clean your code man");
				else speedUpLevel += 1;
				break;
				
			case UpgradeType.ShitUpgrade:
				if (shitUpLevel + 1 == ShitUpgrades.Count) Debug.LogError("You tried to fuck something up, clean your code man");
				else shitUpLevel += 1;
				break;

			case UpgradeType.TeamUpgrade:
				if (teamUpLevel + 1 == TeamUpgrades.Count) Debug.LogError("You tried to fuck something up, clean your code man");
				else teamUpLevel += 1;
				break;
		}

		OnPigeonUpgraded?.Invoke(upgradeType);
	}

	public string GetCurrentUpgradeTier(UpgradeType upgradeType)
	{
		switch(upgradeType)
		{
			case UpgradeType.SpeedUpgrade:
				return (speedUpLevel + 1 == SpeedUpgrades.Count) ? maxUpgradeLevelText : SpeedUpgrades[speedUpLevel].upgradeTier;
			case UpgradeType.ShitUpgrade:
				return (shitUpLevel + 1 == ShitUpgrades.Count) ? maxUpgradeLevelText : ShitUpgrades[shitUpLevel].upgradeTier;
			case UpgradeType.TeamUpgrade:
				return (teamUpLevel + 1 == TeamUpgrades.Count) ? maxUpgradeLevelText : TeamUpgrades[teamUpLevel].upgradeTier;
		}

		Debug.LogError("I don't know how you did this, but you fucked up");
		return "You fucked up the Upgradeton config";
	}

	public int GetUpgradeCostInt(UpgradeType upgradeType)
	{
		switch (upgradeType)
		{
			case UpgradeType.SpeedUpgrade:
				return (speedUpLevel + 1 == SpeedUpgrades.Count) ? -1 : SpeedUpgrades[speedUpLevel + 1].upgradeCost;
			case UpgradeType.ShitUpgrade:
				return (shitUpLevel + 1 == ShitUpgrades.Count) ? -1 : ShitUpgrades[shitUpLevel + 1].upgradeCost;
			case UpgradeType.TeamUpgrade:
				return (teamUpLevel + 1 == TeamUpgrades.Count) ? -1 : TeamUpgrades[teamUpLevel + 1].upgradeCost;
		}

		Debug.LogError("I don't know how you did this, but you fucked up");
		return -404; // Total fuckup
	}

	public string GetUpgradeCostString(UpgradeType upgradeType)
	{
		return GetUpgradeCostInt(upgradeType).ToString();
	}

	public SpeedUpgrade GetSpeedStats()
	{
		return SpeedUpgrades[speedUpLevel];
	}

	public ShitUpgrade GetShitStats()
	{
		return ShitUpgrades[speedUpLevel];
	}

	public TeamUpgrade GetTeamStats()
	{
		return TeamUpgrades[teamUpLevel];
	}
}

[System.Serializable]
public class Upgrade
{
	public string upgradeTier;
	public int upgradeCost;
}

[System.Serializable]
public class ShitUpgrade : Upgrade
{
	public float shitCooldown;
	public float shitInitialCooldown;
	public float shitSizeMultiplier;
}

[System.Serializable]
public class SpeedUpgrade : Upgrade
{
	public MasterPigeonStatsPack masterStats;
	public FollowerStatsPack followerStats;
}

[System.Serializable]
public class TeamUpgrade : Upgrade
{
	public int numberOfStarterFollowers;
}

[System.Serializable]
public struct FollowerStatsPack
{
	public float minMass;
	public float maxMass;
	public float maxFollowSpeed;
	public float minFollowForce;
	public float maxFollowForce;

	public float minLinearDrag;
	public float maxLinearDrag;
	public float angularDrag;
}

[System.Serializable]
public struct MasterPigeonStatsPack
{
	public float mass;
	public float maxSpeed;
	public float thrustForce;
	public float thrustTorque;

	public float linearDrag;
	public float angularDrag;
}

public enum UpgradeType
{
	SpeedUpgrade,
	ShitUpgrade,
	TeamUpgrade,
}