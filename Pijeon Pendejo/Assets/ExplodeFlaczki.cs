using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeFlaczki : MonoBehaviour
{
    public GameObject bleedPS;

    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            //child is your child transform
            Rigidbody2D rb = child.gameObject.AddComponent<Rigidbody2D>();
            child.transform.localPosition = Vector3.zero;
            rb.AddRelativeForce(new Vector2(Random.Range(-1.0f,1f) * 3, Random.Range(-1.0f, 1f)) *3, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-1.0f, 1f)*10, ForceMode2D.Impulse);
            GameObject clone2 = Instantiate(bleedPS, child);

            clone2.transform.SetParent(child);
            clone2.transform.localPosition = Vector2.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
