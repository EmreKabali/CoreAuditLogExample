using System;
namespace CoreAuditLogExample.Models
{
	public class BaseEntity
	{

		public int Id { get; set; }
		public DateTime Created { get; set; }
		public int CreatedBy { get; set; }
		public DateTime? LastModified { get; set; }
		public int? LastModifiedBy { get; set; }

	}
}

