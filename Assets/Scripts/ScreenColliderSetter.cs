using UnityEngine;

public class ScreenColliderSetter : MonoBehaviour
{
    private void Start()
    {
        SetScreenCollider();
    }

    private void SetScreenCollider()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;
        EdgeCollider2D screenEdgeCollider = GetComponent<EdgeCollider2D>();

        Vector2 topLeft = mainCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 topRight = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 bottomRight = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        Vector2 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));

        Vector2[] points = new Vector2[5]
        {
            topLeft,
            topRight,
            bottomRight,
            bottomLeft,
            topLeft
        };

        screenEdgeCollider.points = points;
    }
}