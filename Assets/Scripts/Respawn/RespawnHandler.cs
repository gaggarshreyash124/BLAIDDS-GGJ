using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    public Vector3 CurrentRespawnCords;
    public GameObject CurrentRespawnPoint;
    public GameObject Player;

    void Start()
    {
        CurrentRespawnCords = Player.transform.position;
    }

    public void SetRespawnPoint(Vector3 cords, GameObject CheckPoint)
    {
        CurrentRespawnCords = cords;
        CurrentRespawnPoint = CheckPoint;
    }
}
