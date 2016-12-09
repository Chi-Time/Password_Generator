using System;

namespace Password_Generator
{
	/// <summary>
	/// Generates Passwords and it's info.
	/// </summary>
	public class PasswordGenerator
	{
		private Random _Random = null;
		private char[] _Characters = null;
		private char[] _Numerics = null;

		public PasswordGenerator ()
		{
			_Random = new Random ();
			_Characters = new char[] {
			//'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
			//'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
			'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
			};
			_Numerics = new char[] {
			'1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
			};
		}

		/// <summary>
		/// Generates a set password with random numerics and characters.
		/// </summary>
		/// <returns>The password.</returns>
		public PasswordInfo GeneratePasswordInfo (string site)
		{
			var info = FileSystem.ReadFromFile (site);

			if(info != null)
			{
				var modifiedInfo = new PasswordInfo
				{
					Site = site,
					Password = GeneratePassword (),
					Creation_Date = info.Creation_Date,
					Changed_Date = DateTime.Now,
					Has_Changed = true
				};

				return modifiedInfo;
			}

			var newInfo = new PasswordInfo
			{
				Site = site,
				Password = GeneratePassword (),
				Creation_Date = DateTime.Now,
				Changed_Date = new DateTime (1111, 11, 1, 11, 11, 11),
				Has_Changed = false
			};

			return newInfo;
		}

		string GeneratePassword ()
		{
			string password = "";

			for (int i = 0; i < 8; i++)
			{
				if (i == 1)
					password += _Numerics[_Random.Next (0, _Numerics.Length)];
				else if (i == 2)
					password += "-";
				else
					password += _Characters[_Random.Next (0, _Characters.Length)];
			}

			return password;
		}
	}
}
