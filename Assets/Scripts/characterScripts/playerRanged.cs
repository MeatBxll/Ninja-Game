using System.Collections;
using UnityEngine;

public class playerRanged : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float secondsBetweenShots = 0.5f;

    private bool canShoot = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
            StartCoroutine(HandleFire());
    }

    private IEnumerator HandleFire()
    {
        canShoot = false;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;

        Vector3 projDirection = (mouseWorldPos - transform.position).normalized;
        Quaternion projRotation = Quaternion.FromToRotation(Vector3.right, projDirection);

        GameObject projClone = Instantiate(projectile, transform.position, projRotation);
        projClone.GetComponent<Rigidbody2D>().velocity = projDirection * projectileSpeed;
        projClone.GetComponent<Bulletv2>().player = gameObject;

        yield return new WaitForSeconds(secondsBetweenShots);
        canShoot = true;
    }
}
