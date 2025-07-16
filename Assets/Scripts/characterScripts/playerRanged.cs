using System.Collections;
using UnityEngine;

public class playerRanged : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float secondsBetweenShots = 0.5f;

    private bool canShoot = true;
    public bool isSpreadShot = false;
    public float spreadAngle;

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
        projClone.GetComponent<Rigidbody2D>().linearVelocity = projDirection * projectileSpeed;
        projClone.GetComponent<Bulletv2>().player = gameObject;

        if (isSpreadShot)
        {
            float[] angles = new float[] { -spreadAngle, spreadAngle };
            GameObject[] projCloneSpread = new GameObject[angles.Length];

            for (int i = 0; i < angles.Length; i++)
            {
                Quaternion spreadRotation = Quaternion.Euler(0, 0, angles[i]) * projRotation;
                Vector3 spreadDirection = spreadRotation * Vector3.right;

                projCloneSpread[i] = Instantiate(projectile, transform.position, spreadRotation);
                projCloneSpread[i].GetComponent<Rigidbody2D>().linearVelocity = spreadDirection * projectileSpeed;
                projCloneSpread[i].GetComponent<Bulletv2>().player = gameObject;
            }
        }


        yield return new WaitForSeconds(secondsBetweenShots);
        canShoot = true;
    }
}
