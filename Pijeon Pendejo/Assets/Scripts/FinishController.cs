using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    public GameObject EndPoint;
    public GameObject PijeonPendejo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MainPigeon"))
        {
            collision.gameObject.SetActive(false);
            PigeonUnit.MasterPigeon = EndPoint;
            StartCoroutine(SetPijeonPendejoActive());
        }
    }

    private IEnumerator SetPijeonPendejoActive()
    {
        yield return new WaitForSeconds(1);
        PijeonPendejo.SetActive(true);
    }
}
