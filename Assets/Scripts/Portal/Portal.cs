using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Tile")
        {
            GameManager.Instance.ToEnd(true);
        }
    }
}
