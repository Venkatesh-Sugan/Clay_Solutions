using System;
namespace Clays.app.DataAccess.Entities
{
	public class DoorHistory
	{
		public int DoorHistoryId { get; set; }

		public int DoorId { get; set; }

		public string UserId { get; set; }

		public string UserName { get; set; }

		public string DoorName { get; set; }

		public string IsDoorAccessed { get; set; }

		public DateTime DoorAccessedTime { get; set; }
	}
}

