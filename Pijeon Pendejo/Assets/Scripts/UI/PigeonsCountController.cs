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
        int count = PigeonManager.AvailablePigeonFollowers;
        count = count > 0 ? count + 1 : 0;
        countText.SetText(count.ToString()); 
    }
}
