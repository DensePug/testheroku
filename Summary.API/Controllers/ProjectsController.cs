using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Summary.API.Models;
using Summary.API.Services;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Summary.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly SummaryContext dbContext;

        public ProjectsController(SummaryContext dbcontext)
        {
            this.dbContext = dbcontext;
        }

        /// <summary>
        /// Returns project by its id.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{projectId}")]
        public IActionResult GetProjectById(Guid projectId)
        {
            var project = dbContext.Projects.Find(projectId);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        /// <summary>
        /// Returns all projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllProjects()
        {
            var projects = dbContext.Projects.ToList();
            return Ok(projects);
        }

        /// <summary>
        /// Created project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateProject(Project project)
        {
            dbContext.Projects.Add(project);
            dbContext.SaveChanges();

            return Created(Url.RouteUrl(project.ProjectId), project.ProjectId);
        }
    }
}
