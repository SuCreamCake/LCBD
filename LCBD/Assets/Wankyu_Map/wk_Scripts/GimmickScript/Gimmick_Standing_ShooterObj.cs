using System.Collections;
using TMPro;
using UnityEngine;

public class Gimmick_Standing_ShooterObj : MonoBehaviour
{
    bool isShooting;
    bool isClear;

    [SerializeField] private GameObject bullet;

    private void Awake()
    {
        gameObject.SetActive(true);

        isShooting = false;
        isClear = false;
    }

    private void Update()
    {
        if (!isClear)
        {
            if (!isShooting)
            {
                StartCoroutine(ShootCoroutine());
            }
        }
    }

    private IEnumerator ShootCoroutine()
    {
        isShooting = true;

        float waitSec = UnityEngine.Random.Range(3f, 6f);
        yield return new WaitForSeconds(waitSec);

        GameObject shootedBullet = Instantiate(bullet, transform.position, transform.rotation);
        shootedBullet.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));

        isShooting = false;

        yield break;
    }
}
