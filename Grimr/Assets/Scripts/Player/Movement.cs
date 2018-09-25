using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _tileWidth;
        [SerializeField] private float _inputDelay;
        [SerializeField, Range(0, 1f)] private float _speed = .1f;

        private float _currentDelay;
        private Vector3 _targetPosition;

        private void Start()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            // Get input.
            var horizontal = (int)-Input.GetAxisRaw("Horizontal");
            var vertical = (int)Input.GetAxisRaw("Vertical");
            
            if (_currentDelay >= _inputDelay && Mathf.Abs(horizontal) + Mathf.Abs(vertical) != 0)
            {
                var newPosition = new Vector3(vertical, 0, horizontal) * _tileWidth;
                _targetPosition += newPosition;

                _currentDelay = 0;
                
                // Change rotation.
                if (horizontal == 1)
                {
                    transform.eulerAngles = Vector3.up * 90;
                }
                else if (horizontal == -1)
                {
                    transform.eulerAngles = Vector3.up * -90;
                }
                else if (vertical == 1)
                {
                    transform.eulerAngles = Vector3.up * 180;
                }
                else if (vertical == -1)
                {
                    transform.eulerAngles = Vector3.zero;
                }
            }
            else if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) == 0)
            {
                _currentDelay = _inputDelay;
            }

            transform.position = Vector3.LerpUnclamped(transform.position, _targetPosition, _speed);

            _currentDelay += Time.unscaledDeltaTime;
            _currentDelay = Mathf.Clamp(_currentDelay, 0, _inputDelay);
        }
    }
}
