using System;
namespace Clays.app.DataAccess.Entities
{
	public class Door 
	{
		public Door()
		{
		}

		public int DoorId { get; set; }

		public int DoorType { get; set; }

		public string DoorName { get; set; }

		public string DoorNumber { get; set; }

		public virtual DoorType Type { get; set; }
	}
}

