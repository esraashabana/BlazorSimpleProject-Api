using System.ComponentModel.DataAnnotations;

namespace ApiConsumer.Models
{
	public class Dept
	{
		[Required]
		public int	DeptId { get; set; }
		[Required]
		[MinLength(3)]
        public string DeptName { get; set; }
		[MinLength(3)]
		public string DeptDesc { get; set; }
		[Required]
		[MinLength(3)]
		public string DeptLocation {  get; set; }
		[Required]
		public string DeptManager { get; set; }
		[Required]
        public string ManagerHiredate { get; set; }
    }
}
