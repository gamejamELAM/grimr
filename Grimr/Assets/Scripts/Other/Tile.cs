using UnityEngine;

namespace Other
{
	public class Tile : MonoBehaviour
	{
		public Direction[] Directions;
		
		public enum Direction
		{
			Up, Right, Down, Left
		}
	}
}
