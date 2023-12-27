using UnityEngine;
public class Fractal : MonoBehaviour
{
    private struct FractalPart
    {
        public Vector3 Direction;
        public Quaternion Rotation;
        public Vector3 WorldPosition;
        public Quaternion WorldRotation;
        public float SpinAngle;
    }

    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;
    [SerializeField, Range(1, 8)] private int _depth = 4;
    [SerializeField, Range(1, 360)] private int _rotationSpeed;
    private FractalPart[][] _parts;
    private Matrix4x4[][] _matrices;
    private const float _positionOffset = 1.5f;
    private const float _scaleBias = .5f;
    private const int _childCount = 5;

    private static Vector3[] _directions = new Vector3[]
    {
        Vector3.up,
        Vector3.left,
        Vector3.right,
        Vector3.forward,
        Vector3.back,
    };

    private static Quaternion[] _rotations = new Quaternion[]
    {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, 0f),
    };

    private void OnEnable()
    {
        _parts = new FractalPart[_depth][];
        _matrices = new Matrix4x4[_depth][];
        for (int i = 0, length = 1; i < _parts.Length; i++, length *=
        _childCount)
        {
            _parts[i] = new FractalPart[length];
            _matrices[i] = new Matrix4x4[length];
        }
        _parts[0][0] = CreatePart(0);
        for (var li = 1; li < _parts.Length; li++)
        {
            var levelParts = _parts[li];
            for (var fpi = 0; fpi < levelParts.Length; fpi += _childCount)
            {
                for (var ci = 0; ci < _childCount; ci++)
                {
                    levelParts[fpi + ci] = CreatePart(ci);
                }
            }
        }
    }
    private void Update()
    {
        var scale = 1f;
        var spinAngleDelta = _rotationSpeed * Time.deltaTime;
        var rootPart = _parts[0][0];
        rootPart.SpinAngle += spinAngleDelta;
        rootPart.WorldRotation = rootPart.Rotation * Quaternion.Euler(0f,
        rootPart.SpinAngle, 0f);
        _parts[0][0] = rootPart;
        _matrices[0][0] = Matrix4x4.TRS(rootPart.WorldPosition,
        rootPart.WorldRotation, scale * Vector3.one);
        for (var li = 1; li < _parts.Length; li++)
        {
            scale *= _scaleBias;
            var parentParts = _parts[li - 1];
            var levelParts = _parts[li];
            for (var fpi = 0; fpi < levelParts.Length; fpi++)
            {
                var parent = parentParts[fpi / _childCount];
                var part = levelParts[fpi];
                part.SpinAngle += spinAngleDelta;
                part.WorldRotation =
                parent.WorldRotation * (part.Rotation * Quaternion.Euler(0f,
                part.SpinAngle, 0f));
                part.WorldPosition = parent.WorldPosition + parent.WorldRotation
                *
            (_positionOffset * scale * part.Direction);
                levelParts[fpi] = part;
            }
        }
    }
    private FractalPart CreatePart(int childIndex) => new FractalPart
    {
        Direction = _directions[childIndex],
        Rotation = _rotations[childIndex],
    };
}