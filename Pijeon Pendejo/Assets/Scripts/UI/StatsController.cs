using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    public UpgradeType TypeOfUpgrade;
    public TextMeshProUGUI StatLevelText;
    public TextMeshProUGUI PriceText;
    void Start()
    {
        AssignTextValues();
    }

    public void Refresh()
    {
        AssignTextValues();
    }

    private void AssignTextValues()
    {
        string currentTier = Upgradeton.instance.GetCurrentUpgradeTier(TypeOfUpgrade);
        currentTier = currentTier.Equals(Upgradeton.instance.maxUpgradeLevelText) ? "MAX" : currentTier;
        StatLevelText.SetText(currentTier);
        PriceText.SetText(currentTier.Equals("MAX") ? "" : Upgradeton.instance.GetUpgradeCostString(TypeOfUpgrade));
    }
}
