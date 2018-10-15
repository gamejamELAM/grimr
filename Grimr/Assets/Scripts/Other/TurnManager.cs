using System.Collections;
using Player;
using UnityEngine;

namespace Other
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField, Range(0, 4)] private float _enemyTime;
        public bool IsPlayerTurn = true;
        private Movement _player;
        private Route[] _enemies;

        private void Awake()
        {
            _player = GetComponentInChildren<Movement>();
            _enemies = GetComponentsInChildren<Route>();
        }

        public void ChangeTurns()
        {            
            IsPlayerTurn = !IsPlayerTurn;
            
            _player.InputEnabled = IsPlayerTurn;

            if (!IsPlayerTurn)
            {
                foreach (var enemy in _enemies)
                {
                    enemy.Move();
                }

                StartCoroutine(Pause());
            }
        }

        IEnumerator Pause()
        {
            yield return new WaitForSecondsRealtime(_enemyTime);
            ChangeTurns();
        }
    }
}
