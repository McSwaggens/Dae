using System;

namespace Dae
{
	public struct CUnit
	{
		private char character;

		public byte ascii
		{
			private set;
			get;
		}

		public char Character
		{
			get
			{
				return character;
			}
			set
			{
				character = value;
				ascii = Convert.ToByte (value);
			}
		}

		public Color3 backgroundColor;
		public Color3 foregroundColor;

		public CUnit ( char character, Color3 backgroundColor, Color3 foregroundColor )
		{
			this.character = character;
			this.ascii = ascii = Convert.ToByte (character);

			this.backgroundColor = backgroundColor;
			this.foregroundColor = foregroundColor;
		}

		public CUnit ( char character )
		{
			this.character = character;
			this.ascii = Convert.ToByte (character);

			backgroundColor = Color3.black;
			foregroundColor = Color3.white;
		}

		/// <summary>
		/// Creates an array of CharUnits from a string
		/// </summary>
		/// <param name="Input string"></param>
		/// <param name="Background Color"></param>
		/// <param name="Foreground Color"></param>
		/// <returns>Array of units</returns>
		public static CUnit[] ToCUnitArray ( string str, Color3 backgroundColor, Color3 foregroundColor )
		{
			CUnit[] units = new CUnit[str.Length];

			for (int i = 0; i < str.Length; i++)
			{
				units[i] = new CUnit (str[i], backgroundColor, foregroundColor);
			}

			return units;
		}

		public void SetCharacter ( char c )
		{
			character = c;
			ascii = Convert.ToByte (c);
		}
	}
}