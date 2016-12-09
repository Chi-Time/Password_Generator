using System;

namespace Password_Generator
{
	public class Program
	{
		private PasswordGenerator _PasswordGenerator;

		public static void Main (string[] args) => new Program ().Entry ();

		void Entry ()
		{
			_PasswordGenerator = new PasswordGenerator ();

			StartProgram ();
		}

		void StartProgram ()
		{
			DisplayGreeting ();
			PerformAction (ParseInput (Console.ReadLine ()));
		}

		void DisplayGreeting ()
		{
			Console.Clear ();
			Console.WriteLine ("Welcome to the password Generator.");
			Console.WriteLine ("What do you wish to do today?\n");
			Console.WriteLine ("1: Generate a password.");
			Console.WriteLine ("2: Find a password.");
			Console.WriteLine ("3: Delete a password.");
			Console.WriteLine (" ");
		}

		int ParseInput (string key)
		{
			int choice;

			if(int.TryParse (key, out choice))
				return choice;

			return 0;
		}

		void PerformAction (int actionType)
		{
			switch (actionType)
			{
				case 1:
					WritePassword ();
					break;
				case 2:
					ReadPassword ();
					break;
				case 3:
					DeletePassword ();
					break;
				default:
					Console.WriteLine ("\nIncorrect choice type.");
					EndProgram ();
					break;
			}
		}

		string GetSite ()
		{
			Console.WriteLine ("\nPlease name the site the password is associated to:");
			string site = Console.ReadLine ();

			return site;
		}

		void WritePassword ()
		{
			string site = GetSite ();

			if (CanWrite (site))
				GetNewPassword (site);
			else
				EndProgram ();
		}

		bool CanWrite (string site)
		{
			if (FileSystem.CheckFile (site))
			{
				if (CanOverwrite (site))
					return true;
				
				return false;
			}

			return true;
		}

		bool CanOverwrite (string site)
		{
			Console.WriteLine ("Password exists! Do you wish to overwrite?");
			Console.WriteLine ("1: Yes.");
			Console.WriteLine ("Any Key: No.");
			int choice = ParseInput (Console.ReadLine ());

			if (choice == 1)
				return true;

			return false;
		}

		void GetNewPassword (string site)
		{
			Console.WriteLine ("\nGenerating a new password...");
			PasswordInfo info = _PasswordGenerator.GeneratePasswordInfo (site);

			if (info != null)
				Console.WriteLine ("Site: " + info.Site + "\nNew Password: " + info.Password);
			else
				Console.WriteLine ("Sorry! An error occured, please try again.");

			FileSystem.WriteToFile (info);
			EndProgram ();
		}

		void ReadPassword ()
		{
			string site = GetSite ();

			Console.WriteLine ("\nRetrieving password...");
			PasswordInfo info = FileSystem.ReadFromFile (site);

			if (info == null)
				Console.WriteLine ("There is no password for that site.");
			else
				Console.WriteLine ("Site: " + info.Site + "\nNew Password: " + info.Password + "\nCreated at: " + info.Creation_Date);

			EndProgram ();
		}

		void DeletePassword ()
		{
			string site = GetSite ();

			if (FileSystem.DeleteFile (site))
				Console.WriteLine ("\nPassword successfully removed.");
			else
				Console.WriteLine ("\nNo file to delete.");

			EndProgram ();
		}

		void EndProgram ()
		{
			Console.WriteLine ("\nPress any key to continue.\n");
			Console.ReadKey ();
			StartProgram ();
		}
	}
}
