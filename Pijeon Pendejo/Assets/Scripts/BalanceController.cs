using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalanceController : MonoBehaviour
{
    public TextMeshProUGUI BalanceText;
    void Start()
    {
        BalanceText.SetText(PlayerBanketon.instance.GetPlayerPigeonsString());   
    }
    
    public void Refresh()
    {
        BalanceText.SetText(PlayerBanketon.instance.GetPlayerPigeonsString());    }
}
