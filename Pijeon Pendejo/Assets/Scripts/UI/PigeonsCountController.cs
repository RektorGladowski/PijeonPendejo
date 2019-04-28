using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PigeonsCountController : MonoBehaviour
{
    private TextMeshProUGUI countText;
    void Start()
    {
        countText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        countText.SetText((PigeonManager.AvailablePigeonFollowers  + 1).ToString()); 
    }
}
