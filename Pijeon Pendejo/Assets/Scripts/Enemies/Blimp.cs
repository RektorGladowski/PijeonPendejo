using System.Collections;
using UnityEngine;

public class Blimp : Enemy
{
    public float FireThreshold = 1;
    public float ShootPower = 50;
    public GameObject Bullet;
    public GameObject Gun;
    public Transform BulletSpawnPoint;

    private bool pigeonsInSight = true;
    private bool shootingAllowed = true;
    private Vector2 vectorTowardsPigeon = new Vector2(0, 0);
    private Vector2 gunDirectionVector = new Vector2(1, 0);

    private void Update()
    {
        if (pigeonsInSight)
        {
            vectorTowardsPigeon = -(PigeonUnit.GetMasterPigeonPosition() - Gun.transform.position).normalized;
            Gun.transform.up = vectorTowardsPigeon;
            if(shootingAllowed)
            {
                StartCoroutine(Shoot(FireThreshold));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pigeon") || collision.gameObject.CompareTag("MainPigeon"))
        {
            pigeonsInSight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pigeon") || collision.gameObject.CompareTag("MainPigeon"))
        {
            pigeonsInSight = false;
        }
    }

    private IEnumerator Shoot(float threshold)
    {
        shootingAllowed = false;
        yield return new WaitForSeconds(threshold);
        GameObject bullet = Instantiate(Bullet, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = -vectorTowardsPigeon * ShootPower;
        shootingAllowed = true;
    }
}
