using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;


    public void Fire()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

}
