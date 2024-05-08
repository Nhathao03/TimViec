using System.ComponentModel;

namespace TimViec.Helpers
{
	public class Constants
	{
		public enum StatusJob
		{
			[Description("Đang chờ duyệt")]
			Inprogress = 1,
			[Description("Đã duyệt")]
			Completed = 2
		}
	}
}
