using TMPro;
using UnityEngine;

public class HandPointer : MonoBehaviour
{
    public OVRHand rightHand;
    public GameObject CurrentTarget { get; private set; }
    public TextMeshPro debugTextMesh;

    [SerializeField] private bool showCast = true;
    [SerializeField] private Color highlightColor = Color.white;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LineRenderer lineRenderer;

    private Color _originColor;
    private Renderer _currentRenderer;

    void Update()
    {
        CheckHandPointer(rightHand);
    }

    void CheckHandPointer(OVRHand hand)
    {
        if (Physics.Raycast(hand.PointerPose.position, hand.PointerPose.forward, out RaycastHit hit, Mathf.Infinity, targetLayer))
        {
            if (CurrentTarget != hit.transform.gameObject)
            {
                CurrentTarget = hit.transform.gameObject;
                _currentRenderer = hit.transform.GetComponent<Renderer>();
                _originColor = _currentRenderer.material.color;
                _currentRenderer.material.color = highlightColor;
                debugTextMesh.text = CurrentTarget.name + "sss";
            }

            UpdateRayVisualization(hand.PointerPose.position, hit.point, true);
        }
        else
        {
            if (CurrentTarget != null)
            {
                _currentRenderer.material.color = _originColor;
                CurrentTarget = null;
            }
            UpdateRayVisualization(hand.PointerPose.position, hand.PointerPose.position + hand.PointerPose.forward * 1000, false);
        }
    }

    private void UpdateRayVisualization(Vector3 startPosition, Vector3 endPosition, bool hitSomething)
    {
        if (showCast && lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);
            lineRenderer.material.color = hitSomething ? Color.red : Color.white;
        }
        else if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }
}
