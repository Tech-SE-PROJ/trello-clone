using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using trello_clone.Server.Interfaces;
using trello_clone.Server.Services;
using trello_clone.Shared;
using trello_clone.Shared.Classes;

namespace trello_clone.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private IBoardService _boardService = new BoardService();

        private IEnumerable<Board> boards => _boardService.GetBoards();

        public BoardsController() 
        {
            _boardService = new BoardService();
        }

        [HttpGet("BoardList")]
        public IEnumerable<Board> Get()
        {
            return _boardService.GetBoards();
        }

        [HttpGet("AddBoard/{boardName}")]
        public IEnumerable<Board> AddBoard(string boardName)
        {
            _boardService.AddBoard(boardName);
            return boards;
        }

        [HttpPost("UpdateBoard")]
        public Board UpdateBoard([FromBody] Board board)
        {
            _boardService.UpdateBoard(board);

            var updatedBoard = _boardService.GetBoard(board.Id);

            return updatedBoard;
        }
    }
}
