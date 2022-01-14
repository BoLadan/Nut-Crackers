using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject bullet;
    public GameObject explosion;

    [Space(10)]
    public Transform barrelEnd;

    [Space(10)]
    float direction = 1;
    public float minTime;
    public float maxTime;

    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        direction = gameObject.transform.localScale.x;

        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            StartCoroutine(DelayShoot());
        }
    }

    private void OnBecameVisible()
    {
        Shoot();
    }

    void Shoot()
    {
        GameObject b = Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);
        b.GetComponent<BulletScript>().speed = b.GetComponent<BulletScript>().speed * direction;
    }

    public float CalculateDelayTime()
    {
        float randomDelay;
        randomDelay = Random.Range(minTime, maxTime);

        return randomDelay;
    }

    private IEnumerator DelayShoot()
    {
        canShoot = false;
        float delayTime = CalculateDelayTime();
        yield return new WaitForSeconds(delayTime);
        Shoot();
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
        }
    }
}
