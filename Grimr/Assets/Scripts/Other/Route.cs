using UnityEngine;

namespace Other
{
    public class Route : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _movementSpeed;
        [Space, SerializeField] private Tile.Direction[] _route;

        private int _index;
        private Vector3 _targetPosition;

        private void Start()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _movementSpeed);
        }

        public void Move()
        {
            // Move once their script is enabled.
            var newPosition = Vector3.zero;
            
            switch (_route[_index])
            {
                case Tile.Direction.Up:
                    transform.eulerAngles = Vector3.up * 180;
                    newPosition = Vector3.right * 1.5f;
                    break;
                case Tile.Direction.Right:
                    transform.eulerAngles = Vector3.up * -90;
                    newPosition = Vector3.back * 1.5f;
                    break;
                case Tile.Direction.Down:
                    transform.eulerAngles = Vector3.zero;
                    newPosition = Vector3.right * -1.5f;
                    break;
                case Tile.Direction.Left:
                    transform.eulerAngles = Vector3.up * 90;
                    newPosition = Vector3.forward * 1.5f;
                    break;
            }

            _targetPosition += newPosition;
            _index = _index == _route.Length - 1 ? 0 : _index + 1;
        }
    }
}
