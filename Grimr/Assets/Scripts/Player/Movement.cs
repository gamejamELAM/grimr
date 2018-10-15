using Other;
using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        public bool InputEnabled = true;
        [SerializeField] private TurnManager _turnManager;
        [SerializeField] private Tile _targetTile;
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
            var horizontal = (int)Input.GetAxisRaw("Horizontal");
            var vertical = (int)Input.GetAxisRaw("Vertical");
            
            if (_currentDelay >= _inputDelay && Mathf.Abs(horizontal) + Mathf.Abs(vertical) != 0 && InputEnabled)
            {
                _currentDelay = 0;
                
                // Check if you can move in the desired direction.
                foreach (var direction in _targetTile.Directions)
                {
                    if (horizontal == 1 && direction == Tile.Direction.Right ||
                        horizontal == -1 && direction == Tile.Direction.Left ||
                        vertical == 1 && direction == Tile.Direction.Up ||
                        vertical == -1 && direction == Tile.Direction.Down)
                    {
                        // Work out where the new tile should be.
                        var newPosition = new Vector3(1.5f * vertical, 0, 1.5f * -horizontal);
                        var newTargetTile = Physics.OverlapSphere(_targetTile.transform.position + newPosition, .6f);
                        // If the collider hits a tile, set that as a new target.
                        if (newTargetTile.Length > 0)
                        {
                            _targetTile = newTargetTile[0].GetComponent<Tile>();
                        }
                        
                        // Change the turns.
                        _turnManager.ChangeTurns();
                    }
                }
                
                // Change rotation based on input.
                if (horizontal == 1) transform.eulerAngles = Vector3.up * -90;
                else if (horizontal == -1) transform.eulerAngles = Vector3.up * 90;
                else if (vertical == 1) transform.eulerAngles = Vector3.up * 180;
                else if (vertical == -1) transform.eulerAngles = Vector3.zero;
            }
            else if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) == 0)
            {
                _currentDelay = _inputDelay;
            }
            
            // Update the current target position.
            _targetPosition = _targetTile.transform.position + Vector3.up * .5f;
            transform.position = Vector3.LerpUnclamped(transform.position, _targetPosition, _speed);

            _currentDelay += Time.unscaledDeltaTime;
            _currentDelay = Mathf.Clamp(_currentDelay, 0, _inputDelay);
        }
    }
}
