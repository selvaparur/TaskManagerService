using TaskManager.BL;
using System.Web.Http;

namespace TaskManager.API
{
    [RoutePrefix("v1/tasks")]
    public class TasksController : ApiController
    {
        readonly ITaskBusinessBL _taskBusiness;
        public TasksController(ITaskBusinessBL taskBusiness)
        {
            _taskBusiness = taskBusiness;
        }

        [HttpGet]
        [Route("parent-tasks")]
        public IHttpActionResult GetAllParentTasks()
        {
            var models = _taskBusiness.GetAllParentTasksBL();
            return Ok(models);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            var task = _taskBusiness.GetByIdBL(id);
            return Ok(task);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var tasks = _taskBusiness.GetAllTasksBL();
            return Ok(tasks);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Save(TaskViewModel model)
        {
            _taskBusiness.SaveBL(model);
            return Ok();
        }

        [HttpPost]
        [Route("complete")]
        public IHttpActionResult Complete(TaskViewModel model)
        {
            _taskBusiness.CompleteBL(model);

            var tasks = _taskBusiness.GetAllTasksBL();
            return Ok(tasks); ;
        }
    }
}
