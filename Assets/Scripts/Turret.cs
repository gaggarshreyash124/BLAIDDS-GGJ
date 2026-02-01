using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    float Counter = 0f;
    public float ShootInterval = 1f;

    void Awake()
    {
        
    }
    public void Fire()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    void Update()
    {
        Counter += Time.deltaTime;

        if (Counter >= ShootInterval)
        {
            Fire();
            Counter = 0f;
        }
    }

}
