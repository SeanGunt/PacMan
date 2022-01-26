using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManController : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    [SerializeField] float _speed = 3.0f;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }
    void FixedUpdate()
    {
        Vector2 _position = _rigidbody2D.position;
        _rigidbody2D.MovePosition(_position);
    }
}
