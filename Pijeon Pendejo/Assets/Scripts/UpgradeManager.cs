using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
   public StatsController SpeedController;
   public StatsController ShitController;
   public StatsController TeamController;
   public BalanceController AccountBalanceController;
   
   public void OnClickSpeedUpgrade()
   {
         PerformUpgrade(UpgradeType.SpeedUpgrade);
         SpeedController.Refresh();
   }
   
   public void OnClickShitUpgrade()
   {
      Debug.Log("UPGRade!");
      PerformUpgrade(UpgradeType.ShitUpgrade);
      ShitController.Refresh();
   }
   
   public void OnClickTeamUpgrade()
   {
      PerformUpgrade(UpgradeType.TeamUpgrade);
      TeamController.Refresh();
   }

   private void PerformUpgrade(UpgradeType upgradeType)
   {
      int upgradeCost = Upgradeton.instance.GetUpgradeCostInt(upgradeType);
      if (PlayerBanketon.instance.CanPayWithPigeons(upgradeCost) && !Upgradeton.instance.GetCurrentUpgradeTier(upgradeType).Equals(Upgradeton.instance.maxUpgradeLevelText))
      {
         PlayerBanketon.instance.PayWithPigeons(upgradeCost);
         Upgradeton.instance.UpgradePigeon(upgradeType);
         AccountBalanceController.Refresh();
      }
   }
}
