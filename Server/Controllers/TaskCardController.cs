using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trello_clone.Client.Pages;
using trello_clone.Server.Interfaces;
using trello_clone.Server.Services;
using trello_clone.Shared.Classes;

namespace trello_clone.Server.Controllers
{
    public class TaskCardController : Controller
    {
        private ITaskCardService _taskCardService = new TaskCardService();



        [HttpGet("AddTaskCard/{taskCardName}/{columnId}/{cardIndex}")]
        public IEnumerable<TaskCard> AddTaskCard(string taskCardName, Guid columnId, int cardIndex)
        {
            _taskCardService.AddTaskCard(columnId, taskCardName, cardIndex);
            
            return _taskCardService.GetTaskCards(columnId);
        }
    }
}
