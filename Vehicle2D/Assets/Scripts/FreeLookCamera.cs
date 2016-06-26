using UnityEngine;
using System.Collections;

public class FreeLookCamera : MonoBehaviour {
    private Camera _camera;
    private bool _dragInProgress;
    private Vector3 _dragStartPos;
    private Vector3 _cameraOriginalPos;

    public float MaxSize = 31.27f;
    public float MinSize = 6.0f;
    public float SizeDelta = 0.5f;
    public float DragPositionFactor = 0.1f;

    void Start () {
        _camera = GetComponent<Camera>();
        _camera.orthographic = true;
	}
	
	void Update () {
        var wheelDelta = Input.GetAxis("Mouse ScrollWheel");

        if (wheelDelta > 0f && _camera.orthographicSize > MinSize)
        {
            _camera.orthographicSize = Mathf.Max(MinSize, _camera.orthographicSize - SizeDelta);
        }

        if (wheelDelta < 0f && _camera.orthographicSize < MaxSize)
        {
            _camera.orthographicSize = Mathf.Min(MaxSize, _camera.orthographicSize + SizeDelta);
        }

        DetectDrag();

        if (_dragInProgress)
        {
            HandleDrag();
        }

    }

    private void DetectDrag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _dragInProgress = true;
            _dragStartPos = Input.mousePosition;
            _cameraOriginalPos = transform.position;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _dragInProgress = false;
        }
    }

    private void HandleDrag()
    {
        var currentPos = Input.mousePosition;
        var posDelta = currentPos - _dragStartPos;
        transform.position = _cameraOriginalPos - posDelta * DragPositionFactor;
    }
}
