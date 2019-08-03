using UnityEngine;

namespace SRMLExtras.Debug
{
	public static class DebugGUIConstants
	{
		// Base Things
		public readonly static Rect title = new Rect(120, 15, Screen.width / 2, 25);

		// Player Info
		public readonly static Rect playerPos = new Rect(120, 45, Screen.width / 2, 25);
		public readonly static Rect playerZone = new Rect(120, 75, Screen.width / 2, 25);
		public readonly static Rect playerMapLock = new Rect(120, 105, Screen.width / 2, 25);
		public readonly static Rect playerEndGame = new Rect(120, 135, Screen.width / 2, 25);
		public readonly static Rect playerAmmoMode = new Rect(120, 165, Screen.width / 2, 25);
	}
}
