using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerMain _player;
    [SerializeField][Range(0, 10)] private float _smoothSpeed;
    [SerializeField] private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
       _player = FindObjectOfType<PlayerMain>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPosition = _player.transform.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
