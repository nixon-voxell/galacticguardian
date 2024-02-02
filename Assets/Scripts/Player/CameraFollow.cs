using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform player;

    private void Update()
    {
        Vector3 newPos = new Vector3(player.position.x, player.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position,newPos,FollowSpeed * Time.deltaTime);
    }



}
