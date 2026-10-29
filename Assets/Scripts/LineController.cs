using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class LineController : MonoBehaviour
{
    [SerializeField]
    private Transform _anchorOne;
    [SerializeField]
    private Transform _anchorTwo;
    [SerializeField]
    [Min(2)] private int _segments = 24;
    [SerializeField]
    [Min(0f)] private float _maxSag = 2f;
    [SerializeField]
    [Min(0.01f)] private float _slackStart = 0.1f;

    private LineRenderer _lr;
    private EdgeCollider2D _ec;
    void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        _ec = GetComponent<EdgeCollider2D>();
    }

    void Start()
    {
        _lr.positionCount = _segments + 1;
        _lr.SetPosition(0, _anchorOne.position);
        _lr.SetPosition(1, _anchorTwo.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 a = _anchorOne.position;
        Vector2 b = _anchorTwo.position;

        // Current distance
        float distance = Vector2.Distance(a, b);

        // Original distance
        float ropeLength = GetComponentInParent<DistanceJoint2D>().distance;

        float slack = Mathf.Max(0f, ropeLength - distance);

        // How far down on the parabola we go. 
        // Delta rope / slackStart
        // larger slackStart to make line not straight sooner, if smaller it'll clamp to 1
        // maxSag just increases distance down
        float sag = _maxSag * Mathf.Clamp01(slack / _slackStart);

        Vector2[] colliderPoints = new Vector2[_segments + 1];

        for (int i = 0; i <= _segments; i++)
        {
            float t = (float)i / _segments;

            Vector2 point = Vector2.Lerp(a, b, t);

            // Zero at both ends, maximum in the middle.
            // Use t * 1f - t so values rise toward middle, otherwise one point will just go downward
            // Because of our t^2 portion right hand will only be multiplied by value <= 0.25f, 
            // multiply by 4 so if sag says drop 2 then it will actually drop 2
            point.y -= 4f * sag * t * (1f - t);

            _lr.SetPosition(i, point);
            // EdgeCollider2D points are local to its Transform. 
            colliderPoints[i] = transform.InverseTransformPoint(point);
        }

        _ec.points = colliderPoints;
    }
}
