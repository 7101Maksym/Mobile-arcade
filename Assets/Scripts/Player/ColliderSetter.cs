using System.Collections.Generic;
using UnityEngine;

public class ColliderSetter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private PolygonCollider2D _collider;

    private void Awake()
    {
        if (_collider != null && _renderer != null)
        {
            GenerateCollider();
        }
    }
    
    public void GenerateCollider()
    {
        _collider.pathCount = _renderer.sprite.GetPhysicsShapeCount();

        for (int i = 0; i < _renderer.sprite.GetPhysicsShapeCount(); i++)
        {
            List<Vector2> colliders = new List<Vector2>();

            _renderer.sprite.GetPhysicsShape(i, colliders);

            _collider.SetPath(i, colliders);
        }
    }
}
