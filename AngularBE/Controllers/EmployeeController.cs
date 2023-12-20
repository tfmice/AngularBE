using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> logger;
        private readonly SharingAngularContext ctx;

        public EmployeeController(ILogger<EmployeeController> logger, SharingAngularContext ctx)
        {
            this.logger = logger;
            this.ctx = ctx;
        }

        [HttpGet("GetAllTeams")]
        public ActionResult<List<TeamViewModel>> GetAllTeams() 
        {
            List<TeamViewModel> queryResult = ctx.Teams.Select((team) => new TeamViewModel()
            {
                Id = team.Id,
                Name = team.Name
            }).ToList();
            return queryResult;
        }

        [HttpGet("GetTeamMembersByTeamId")]
        public ActionResult<List<EmployeeViewModel>> GetEmployeesOfTeamByTeamId(int teamId)
        {
            List<EmployeeViewModel> queryResult = ctx.TeamMembers
                .Where(tm => tm.TeamId == teamId)
                .Select((tm) => new EmployeeViewModel()
                {
                    Id = tm.Employee.Id,
                    Name = tm.Employee.Name,
                    Birthdate = tm.Employee.Birthdate.ToString(),
                    Hobby = tm.Employee.Hobby,
                    Opinion = tm.Employee.Opinion,
                    Quote = tm.Employee.Quote,
                    Role = tm.Employee.Role
                }).ToList();

            return queryResult;
        }

        [HttpGet("GetSections")]
        public ActionResult<List<SectionViewModel>> GetSections()
        {
            List<SectionViewModel> queryResult = ctx.Sections.Select((section) => new SectionViewModel()
            {
                Id = section.Id,
                Name = section.Name
            }).ToList();
            return queryResult;
        }

        [HttpPost("AddEmployee")]
        public EmployeeViewModel AddEmployee([FromBody] AddEmployeeModel form)
        {
            Employee emp = new Employee()
            {
                Birthdate = DateOnly.Parse(form.Birthdate),
                Hobby = form.Hobby,
                Name = form.Name,
                Opinion = form.Opinion,
                Quote = form.Quote,
                Role = form.Role,
            };
            TeamMember teamMember = new TeamMember()
            {
                TeamId = form.TeamId,
                Employee = emp
            };
            ctx.TeamMembers.Add(teamMember);
            ctx.SaveChanges();

            return new EmployeeViewModel()
            {
                Id = emp.Id,
                Name = emp.Name,
                Birthdate = emp.Birthdate.ToString(),
                Hobby = emp.Hobby,
                Opinion = emp.Opinion,
                Quote = emp.Quote,
                Role = emp.Role
            };
        }

        [HttpDelete("DeleteEmployeeById")]
        public void DeleteEmployeeById([FromBody] int employeeId)
        {
            TeamMember teamMember = ctx.TeamMembers.Where(tm => tm.EmployeeId == employeeId).FirstOrDefault();
            Employee emp = ctx.Employees.Where(emp => emp.Id == employeeId)
                .FirstOrDefault();
            ctx.TeamMembers.Remove(teamMember);
            ctx.Employees.Remove(emp);
            ctx.SaveChanges();
        }

        [HttpPost("UpdateEmployeeById")]
        public void UpdateEmployeeById([FromBody] UpdateEmployeeModel form)
        {
            Employee emp = ctx.Employees.Where(emp => emp.Id == form.Id)
                .FirstOrDefault();

            emp.Name = form.Name;
            emp.Birthdate = DateOnly.Parse(form.Birthdate);
            emp.Hobby = form.Hobby;
            emp.Opinion = form.Opinion;
            emp.Quote = form.Quote;
            emp.Role = form.Role;
            ctx.SaveChanges();
        }

    }

    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hobby { get; set; }
        public string Quote { get; set; }
        public string Opinion { get; set; }
        public string Birthdate { get; set; }
        public string Role { get; set; }
    }

    public class SectionViewModel
    { 
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateEmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Hobby { get; set; }
        public string Quote { get; set; }
        public string Opinion { get; set; }
        public string Birthdate { get; set; }
        public string Role { get; set; }
    }

    public class AddEmployeeModel
    {
        public string Name { get; set; }
        public string Hobby { get; set; }
        public string Quote { get; set; }
        public string Opinion { get; set; }
        public string Birthdate { get; set; }
        public string Role { get; set; }
        public int TeamId { get; set; }
    }
}
