using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    public GameObject EndPoint;
    public GameObject PijeonPendejo;
    private int pigeonsReceived = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MainPigeon"))
        {
            collision.gameObject.SetActive(false);
            PigeonUnit.MasterPigeon = EndPoint;
            StartCoroutine(SetPijeonPendejoActive());

            ++pigeonsReceived;
        }
        else if(collision.gameObject.CompareTag("Pigeon"))
        {
            ++pigeonsReceived;
        }
    }

    private IEnumerator SetPijeonPendejoActive()
    {
        PijeonPendejo.SetActive(true);
        yield return new WaitForSeconds(3);
        PlayerBanketon.instance.AddPigeons(pigeonsReceived);
        GameManager.PigeonsReceived = pigeonsReceived;
        GameManager.GameEnded = true;
    }
}
