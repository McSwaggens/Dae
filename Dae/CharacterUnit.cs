using System.Collections.Generic;

namespace Dae
{
	public struct CharUnit
	{
		public char character;
		public Color backgroundColor;
		public Color foregroundColor;

		public CharUnit (char character, Color backgroundColor, Color foregroundColor)
		{
			this.character = character;
			this.backgroundColor = backgroundColor;
			this.foregroundColor = foregroundColor;
		}

		/// <summary>
		/// Creates an array of CharUnits from a string
		/// </summary>
		/// <param name="Input string"></param>
		/// <param name="Background Color"></param>
		/// <param name="Foreground Color"></param>
		/// <returns>Array of units</returns>
		public static CharUnit[] ToCharUnitArray (string str, Color backgroundColor = null, Color foregroundColor = null)
		{
			CharUnit[] units = new CharUnit[str.Length];

			for (int i = 0; i < str.Length; i++)
			{
				units[i] = new CharUnit (str[i], backgroundColor, foregroundColor);
			}

			return units;
		}
	}
}