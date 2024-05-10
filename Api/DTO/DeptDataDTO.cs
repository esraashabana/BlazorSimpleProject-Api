namespace Day2.DTO
{
	public class DeptDataDTO
	{
		public int DeptId { get; set; }

		public string? DeptName { get; set; }

		public string? DeptDesc { get; set; }

		public string? DeptLocation { get; set; }

		public string? DeptManager { get; set; }

		public DateOnly? ManagerHiredate { get; set; }
		public int StudentsNum { get; set; }	
	}
}
