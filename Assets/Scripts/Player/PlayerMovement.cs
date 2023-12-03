using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private int moveDistance = 5;
    private bool _dragging = false;
    private Vector3 _mousePos;
    private Tile _tileBeforeMove;
    private GameObject _ghostToken;
    private bool _validToken = false;

    [Header("References")]
    private DungeonManager _dungeonManager;
    private Pathfinding _pathfinding;
    [SerializeField] private CameraController _cameraController;


    private void Update()
    {
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(_mousePos, -Vector2.up);

        // Detect player start dragging
        if (Input.GetMouseButtonDown(0))
        {
            _validToken = IsValidToken(hit);
            if (!_validToken) return;
            // Set this tile as the start position for dragging
            _tileBeforeMove = _dungeonManager.GetTileFromCoords(hit.collider.transform.position.x, hit.collider.transform.position.y);
            TokenDragStart(hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite, hit.collider.gameObject.transform);
        }

        if (_dragging && HasMouseMoved()) TokenDrag();

        if (Input.GetMouseButtonUp(0) && _validToken) TokenDragEnd(hit);

        if (Input.GetKeyDown(KeyCode.Space)) ResetPlayerCameraPos();
    }

    private void TokenDragStart(Sprite sprite, Transform transform)
    {
        _dragging = true;
        GameObject obj = new GameObject("GhostToken");
        _ghostToken = Instantiate(obj, transform.position, Quaternion.identity);
        SpriteRenderer spriteRenderer = _ghostToken.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = 3;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Destroy(obj);
    }

    private void TokenDrag()
    {
        RaycastHit2D hit = Physics2D.Raycast(_mousePos, -Vector2.up);
        if (!hit || !IsValidMovement(hit.collider.GetComponent<Surface>(), hit)) return;

        Vector3 hitPosition = hit.collider.transform.position;
        float x = hitPosition.x;
        float y = hitPosition.y;

        if (_dungeonManager.DistanceBetweenTwoPoints(transform, hit.collider.gameObject.transform) > moveDistance)
        {
            float minX = transform.position.x - moveDistance;
            float minY = transform.position.y - moveDistance;
            x = Mathf.Clamp(x, minX, transform.position.x + moveDistance);
            y = Mathf.Clamp(y, minY, transform.position.y + moveDistance);
        }

        if (_dungeonManager.DistanceBetweenTwoPoints(transform, hit.collider.transform) > moveDistance)
        {
            _ghostToken.transform.SetParent(_dungeonManager.GetTileFromCoords(x, y).transform);
            _ghostToken.transform.localPosition = Vector3.zero;
        } else
        {
            _ghostToken.transform.SetParent(hit.collider.gameObject.transform);
            _ghostToken.transform.localPosition = Vector3.zero;
        }
    }

    private void TokenDragEnd(RaycastHit2D hit)
    {
        _dragging = false;
        _validToken = false;
        _pathfinding.GetTargetPath(_tileBeforeMove, _ghostToken.gameObject.GetComponentInParent<Tile>());
        MoveToTile();
        Destroy(_ghostToken);
    }

    private void MoveToTile()
    {
        Vector3 prevCameraPos = _cameraController.gameObject.transform.position;
        transform.position = _ghostToken.transform.position;
        if (!CameraController.CameraFollowToken) _cameraController.gameObject.transform.position = prevCameraPos;
    }

    public bool IsValidToken(RaycastHit2D hit)
    {
        if (hit && hit.collider.gameObject.CompareTag("Player"))
            return true;
        else
            return false;
    }

    private bool IsValidMovement(Surface surface, RaycastHit2D hit)
    {
        if (!surface || !surface.IsWalkable)
            return false;
        else
            return true;
    }

    private bool HasMouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }

    private void ResetPlayerCameraPos()
    {
        CameraController.CameraFollowToken = true;
        _cameraController.ResetCameraPos();
    }
}
