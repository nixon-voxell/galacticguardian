using UnityEngine;

public class Essence : MonoBehaviour
{
    public int Count;

    private void OnEnable()
    {
        GameManager.Instance.Essence = this;
    }
}
