using Domain.IRepository;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        [HttpGet("GetTeams")]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Teams.GetAll());
        }


        [HttpGet("{id:int}")]
        [Route("GetSpecificTeam")]
        public IActionResult GetSpecificTeam(int id)
        {
            var result = _unitOfWork.Teams.GetById(id); 
            if(result == null)
            {
                return NotFound("This team does not exist.");
            }
            return Ok(result);
        }


        [HttpPost("AddingTeam")]
        public IActionResult CreateTeam(Team team)
        {
            if (team == null)
            {
                return BadRequest("We can't insert a null Team.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Teams.Add(team);
                _unitOfWork.save();
            }
            return Ok(team);
        }
        [HttpDelete("RemovingTeam")]
        public IActionResult DeleteTeam(int id)
        {
            var result = _unitOfWork.Teams.GetById(id);
            if (result == null)
            {
                return NotFound("This team does not exist");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Teams.Delete(result);
                _unitOfWork.save();
                return Ok("Deleted...");
            }
            return BadRequest();
        }


    }
}
