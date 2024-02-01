using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.Player = this;
    }

    private void OnDisable()
    {
        GameManager.Instance.Player = null;
    }
}
