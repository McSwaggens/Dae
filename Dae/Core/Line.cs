namespace Dae
{
	internal class Line
	{
		public string text = "";

		public Line ( string text )
		{
			this.text = text;
		}

		/// <summary>
		/// Finds the start of the line without tabs or spaces
		/// </summary>
		/// <returns>Start of the line</returns>
		public int GetStartOfLine ()
		{
			// Check if we actually have text to work with
			if (text.Length > 0)
			{
				// Trim the spaces and tabs, return the length of the resulting string.
				int afterTrimSize = text.TrimStart (' ', '\t').Length;

				// Return the size of the trim.
				return text.Length - afterTrimSize;
			}
			else
			{
				// Otherwise, return 0
				return 0;
			}
		}
	}
}