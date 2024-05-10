using Day2.DTO;
using Day2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day2.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		private readonly ITIContext context;
		public DepartmentController(ITIContext _context)
		{
			context = _context;
		}
		[HttpGet]
		public IActionResult Get()
		{

			var AllDepts = context.Departments.ToList();
			var deptDto = new List<DeptDataDTO>();
			foreach (var dept in AllDepts)
			{
				var deptData = new DeptDataDTO()
				{
					DeptId = dept.DeptId,
					DeptName = dept.DeptName,
					DeptDesc = dept.DeptDesc,
					DeptLocation = dept.DeptLocation,
					ManagerHiredate=dept.ManagerHiredate,
					StudentsNum=dept.Students.Count(),
				};
				deptDto.Add(deptData);
			}
			if (AllDepts.Count == 0)
			{
				return NotFound();
			}
			else
			{
				return Ok(deptDto);
			}
		}
		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id)
		{
			var dpt = context.Departments.FirstOrDefault(c => c.DeptId == id);
			if (dpt == null)
			{
				return NotFound();
			}
			else
			{
				context.Departments.Remove(dpt);
				context.SaveChanges();
				return Ok("deleted successfully");
			}
		}
		[HttpPut("{id:int}")]
		public IActionResult Update(Department dpt, int id)
		{
			var oldDpt = context.Departments.FirstOrDefault(c => c.DeptId == id);
			if (oldDpt == null)
			{
				return NotFound();
			}
			if (dpt.DeptId != oldDpt.DeptId)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				oldDpt.DeptName = dpt.DeptName;
				oldDpt.DeptDesc = dpt.DeptDesc;
				oldDpt.DeptLocation = dpt.DeptLocation;
				oldDpt.DeptManager = dpt.DeptManager;
				oldDpt.ManagerHiredate = dpt.ManagerHiredate;
				context.SaveChanges();
				return Ok("updated successfully");
			}
			return BadRequest(ModelState);
		}
		[HttpPost]
		public IActionResult Add(Department dpt)
		{
			if (dpt == null)
			{
				return BadRequest();//400
			}
			if (ModelState.IsValid)
			{
				context.Departments.Add(dpt);
				context.SaveChanges();
				return Created();
			}
			return BadRequest(ModelState);
		}
		[HttpGet("{id:int}")]
		public IActionResult GetDeptById(int? id)
		{
			if (id != null)
			{
				var dept = context.Departments.Include(d => d.Students).FirstOrDefault(c => c.DeptId == id);
				DeptDataDTO deptDataDTO = new DeptDataDTO();
				deptDataDTO.DeptName = dept?.DeptName;
				deptDataDTO.DeptId = dept.DeptId;
				deptDataDTO.DeptDesc = dept?.DeptDesc;
				deptDataDTO.DeptManager = dept?.DeptManager.ToString();
				deptDataDTO.ManagerHiredate = dept?.ManagerHiredate;
				deptDataDTO.DeptLocation=dept?.DeptLocation;
				deptDataDTO.StudentsNum = dept.Students.Count();
				return Ok(deptDataDTO);
			}
			else
			{
				return NotFound();
			}
		}
		[HttpGet("{name:alpha}")]
		public IActionResult GetDeptByName(string name)
		{
			var dept = context.Departments.FirstOrDefault(c => c.DeptName == name);
			if (dept == null)
			{
				return NotFound();
			}
			return Ok(dept);
		}
	}
}