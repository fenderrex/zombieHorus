using System;

namespace SFS2XExamples.FirstPersonShooter {
	public static class OptionsManager {
	
		private static bool invertMouseY = false;
		
		public static bool InvertMouseY {
			get {
				return invertMouseY;
			}
			set {
				invertMouseY = value;
			}
		}
	}
}