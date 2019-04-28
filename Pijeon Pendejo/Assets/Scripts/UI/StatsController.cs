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
        StatLevelText.SetText(Upgradeton.instance.GetCurrentUpgradeTier(TypeOfUpgrade));
        PriceText.SetText(Upgradeton.instance.GetUpgradeCostString(TypeOfUpgrade));
    }

    public void Refresh()
    {
        StatLevelText.SetText(Upgradeton.instance.GetCurrentUpgradeTier(TypeOfUpgrade));
        PriceText.SetText(Upgradeton.instance.GetUpgradeCostString(TypeOfUpgrade));
    }
}
