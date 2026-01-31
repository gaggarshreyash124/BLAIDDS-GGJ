using UnityEngine;

public class Respawn_Points : MonoBehaviour
{
    public RespawnHandler respawnHandler;
    public Transform playerDetector;
    public LayerMask playerMask;
    public float detectRadius = 1f;
    public GameObject respawnPoint;

    void Update()
    {
        if(Physics2D.OverlapCircle(playerDetector.position, detectRadius, playerMask))
        {
            respawnPoint.SetActive(true);
            respawnHandler.SetRespawnPoint(transform.position, this.gameObject);
        }
    }
}
