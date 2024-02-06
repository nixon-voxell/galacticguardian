using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        CameraFollow.Instance.PlayerTransform = this.transform;
        LevelManager.Instance.Player = this;

    }

    private void OnDisable()
    {
        LevelManager.Instance.Player = null;
    }
}
