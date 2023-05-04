using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trello_clone.Server.Interfaces;
using trello_clone.Server.Services;
using trello_clone.Shared.Classes;

namespace trello_clone.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private IColumnService _columnService = new ColumnService();

        [HttpGet("AddColumn/{boardId}/{columnName}/{columnIndex}")]
        public Column AddColumn(Guid boardId, string columnName, int columnIndex)
        {
            var newColumnId = Guid.NewGuid();

            _columnService.AddColumn(boardId, newColumnId, columnName, columnIndex);

            var newColumn = _columnService.GetColumn(newColumnId);
            
            return newColumn;
        }

        [HttpGet("DeleteColumn/{columnId}")]
        public void DeleteColumn(Guid columnId)
        {
            _columnService.DeleteColumn(columnId);
        }
    }
}
