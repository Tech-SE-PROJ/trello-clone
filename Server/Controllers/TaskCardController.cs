using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using trello_clone.Client.Components;
using trello_clone.Client.Pages;
using trello_clone.Server.Interfaces;
using trello_clone.Server.Services;
using trello_clone.Shared;
using trello_clone.Shared.Classes;

namespace trello_clone.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskCardController : Controller
    {
        private ITaskCardService _taskCardService = new TaskCardService();
        private readonly AppDbContext db;

        public TaskCardController(AppDbContext DB)
        {
            db = DB;
        }


        [HttpGet("AddTaskCard/{boardId}/{taskCardName}/{columnId}/{cardIndex}")]
        public IEnumerable<TaskCard> AddTaskCard(Guid boardId, string taskCardName, Guid columnId, int cardIndex)
        {
            _taskCardService.AddTaskCard(boardId, columnId, taskCardName, cardIndex);

            return _taskCardService.GetTaskCards(columnId);
        }

        [HttpGet("get-task-card/{boardId}/{cardId}")]
        public TaskCard? GetTaskCardById(Guid boardId, Guid cardId)
        {
            return db.board_cards.FirstOrDefault(taskCard => taskCard.BoardId == boardId && taskCard.Id == cardId);
        }

        [HttpGet("get-task-cards/{boardId}")]
        public IEnumerable<TaskCard> GetTaskCards(Guid boardId)
        {
            return db.board_cards.ToList().Where(taskCard => taskCard.BoardId == boardId);
        }

        [HttpPut("AssignUserToCard/user/{userId}")]
        public async Task<TaskCard>? AssignUserToCard([FromBody] Guid cardId, Guid userId) 
        {
            TaskCard? cardToEdit = await db.board_cards.FindAsync(cardId);
            if (cardToEdit is null)
                return null;
            using (db)
            {
                cardToEdit!.AssignedUserId = userId;
                await db.SaveChangesAsync();
            }
            return cardToEdit; //save these changes to localStorage
        }

        [HttpPut("store-date/{cardId}")]
        public async Task<TaskCard?> StoreDate([FromBody] DateTime date, Guid cardId)
        {
            TaskCard? cardToEdit = await db.board_cards.FindAsync(cardId);
            if(cardToEdit is null)
                return null;
            using (db)
            {
                cardToEdit.EndDate = date;
                await db.SaveChangesAsync();
            }
            return cardToEdit;
        }
    }
}
