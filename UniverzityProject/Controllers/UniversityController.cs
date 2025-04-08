using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniverzityProject.Model;

namespace UniverzityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly UniversityContext _context = new();

        [HttpGet("/student/email/{email}/name")]
        public async Task<IActionResult> GetNameWhitEmail(string email)
        {
            return Ok(await _context.Students
                .Where(s => s.Email == email)
                .Select(s => new { s.Name })
                .ToListAsync());
        }

        [HttpGet("/student/name/{name}/enrollment")]
        public async Task<IActionResult> GetStudentEnrollment(string name)
        {
            return Ok(await _context.Students
                .Where(s => s.Name == name)
                .Select(s => new { s.Enrolled })
                .ToListAsync());
        }

        [HttpGet("/student/department")]
        public async Task<IActionResult> GetNumberOfStudent()
        {
            return Ok(await _context.Students
                .GroupBy(S => S.DepartmentId)
                .Select(s => new { DepartmentId = s.Key, NumberOfStudent = s.Count()})
                .ToListAsync());
        }

        [HttpGet("GET /api/teachers/with_department")]
        public async Task<IActionResult> GetTeachersDepartment()
        {
            var query =
                from teacher in _context.Teachers
                from department in _context.Departments
                where teacher.DepartmentId == department.Id
                select new {Name = teacher.Name, Department = department.Name};
            return Ok(await query.ToListAsync());
        }


        [HttpGet("GET /api/teachers/with_department2")]
        public async Task<IActionResult> GetTeachersDepartment2()
        {
            var query =
                from teacher in _context.Teachers
                join department in _context.Departments
                on teacher.DepartmentId equals department.Id
                select new { Name = teacher.Name, Department = department.Name };
            return Ok(await query.ToListAsync());
        }

        [HttpGet("GET /api/teachers/with_department3")]
        public async Task<IActionResult> GetTeachersDepartment3()
        {
            var query = await _context.Teachers
                .Join(
                    _context.Departments,teacher => teacher.DepartmentId, department => department.Id, (teacher, department) => new { Name = teacher.Name, Department = department.Name}
                ).ToListAsync();
            return Ok(query);
        }
    }
}
