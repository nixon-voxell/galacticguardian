using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        CameraFollow.Instance.PlayerTransform = this.transform;
        GameManager.Instance.Player = this;
    }

    private void OnDisable()
    {
        GameManager.Instance.Player = null;
    }
}
