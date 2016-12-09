namespace Password_Generator
{
	/// <summary>
	/// Contains information about the password and it's associated site.
	/// </summary>
	public class PasswordInfo
	{
		public string Site { get; set; }
		public string Password { get; set; }
		public bool Has_Changed { get; set; }
		public System.DateTime Creation_Date { get; set; }
		public System.DateTime Changed_Date { get; set; }
	}
}
