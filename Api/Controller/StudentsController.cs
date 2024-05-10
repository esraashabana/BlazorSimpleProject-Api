using Day2.DTO;
using Day2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day2.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		private readonly ITIContext context;
		public StudentsController(ITIContext _context)
		{
			context = _context;
		}
		/// <summary>
		/// This is a method to get all students
		/// </summary>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			var query = context.Students.ToList();
			List<StudentDataDTO> stds = new List<StudentDataDTO>();
			foreach (var item in query)
			{
				StudentDataDTO studentDataDTO = new StudentDataDTO()
				{
					StId = item.StId,
					StFname = item?.StFname,
					StLname = item?.StLname,
					StAddress = item?.StAddress,
					StAge = item?.StAge,
					DeptName = item?.Dept?.DeptName,
					SuperName = item?.StSuperNavigation?.StFname

				};
				stds.Add(studentDataDTO);

			}
			var totalCount = stds.Count();
			var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
			stds = stds.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			return Ok(stds);
		}
		/// <summary>
		/// Method to delete student
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("{id:int}")]
		public IActionResult DeleteStudent(int id)
		{
			var std = context.Students.FirstOrDefault(c => c.StId == id);
			if (std == null)
			{
				return NotFound();
			}
			else
			{
				context.Students.Remove(std);
				context.SaveChanges();
				return Ok("deleted successfully");
			}
		}
		/// <summary>
		/// Method to update student
		/// </summary>
		/// <param name="std"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPut("{id:int}")]
		public IActionResult UpdateStudent(Student std, int id)
		{
			StudentDataDTO studentDataDTO = new StudentDataDTO();
			var oldStd = context.Students.Include(s => s.Dept).FirstOrDefault(c => c.StId == id);
			if (oldStd == null)
			{
				return NotFound();
			}
			if (std.StId != oldStd.StId)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				oldStd.StId = std.StId;
				oldStd.StLname = std.StLname;
				oldStd.StFname = std.StFname;
				oldStd.StLname = std.StLname;
				oldStd.StAddress = std.StAddress;
				oldStd.StSuper = std?.StSuper;
				oldStd.StAge = std?.StAge;
				oldStd.DeptId = std?.DeptId;
				context.SaveChanges();
				return Ok("updated successfully");
			}
			return BadRequest(ModelState);
		}
		/// <summary>
		/// To add new Student .
		/// </summary>
		/// <param name="std"></param>
		/// <returns></returns>
		[HttpPost]
		[Consumes("application/json")]
		[Produces("application/json")]

		public IActionResult AddStudent(Student std)
		{
			if (std == null)
			{
				return BadRequest();//400
			}
			context.Students.Add(std);
			context.SaveChanges();
			return Created();
		}
		/// <summary>
		/// Get Student data by Id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id:int}")]
		public IActionResult GetStudentById(int? id)
		{
			if (id != null)
			{
				var std = context.Students
			.Include(s => s.Dept)
			.Include(s => s.StSuperNavigation)
			.FirstOrDefault(c => c.StId == id);
				StudentDataDTO studentDataDTO = new StudentDataDTO();
				studentDataDTO.StId = std.StId;
				studentDataDTO.StFname = std?.StFname;
				studentDataDTO.StLname = std?.StLname;
				studentDataDTO.StAddress = std?.StAddress;
				studentDataDTO.StAge = std?.StAge;
				studentDataDTO.DeptName = std?.Dept?.DeptName;
				studentDataDTO.SuperName = std?.StSuperNavigation?.StFname;

				return Ok(studentDataDTO);
			}
			else
			{
				return NotFound();
			}
		}
		/// <summary>
		/// Get Student DaTA By Name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		[HttpGet("{name:alpha}")]
		public IActionResult GetStudentByName(string name)
		{
			var course = context.Students.FirstOrDefault(c => c.StFname == name);
			if (course == null)
			{
				return NotFound();
			}
			var std = context.Students.Include(s => s.Dept).Include(s => s.StSuperNavigation).FirstOrDefault(c => c.StFname == name);
			StudentDataDTO studentDataDTO = new StudentDataDTO();
			studentDataDTO.StId = std.StId;
			studentDataDTO.StFname = std?.StFname;
			studentDataDTO.StLname = std?.StLname;
			studentDataDTO.StAddress = std?.StAddress;
			studentDataDTO.StAge = std?.StAge;
			studentDataDTO.DeptName = std?.Dept?.DeptName;
			studentDataDTO.SuperName = std?.StSuperNavigation?.StFname;
			return Ok(studentDataDTO);
		}
	}
}

