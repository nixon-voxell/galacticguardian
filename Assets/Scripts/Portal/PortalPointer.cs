using UnityEngine;

public class PortalPointer : MonoBehaviour
{
    [SerializeField] private Transform m_PortalTransform;
    [SerializeField] private GameObject m_Pointer;

    private void Update()
    {
        this.m_Pointer.SetActive(UserInput.Instance.Build);
        this.transform.right = (m_PortalTransform.position - this.transform.position).normalized;
    }
}
