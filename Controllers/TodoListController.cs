using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TodoListAPI.Model;

namespace TodoListAPI.Controllers
{
    public class TodoListController : ControllerBase
    {
        private readonly TodoListContext _context;
        public TodoListController(TodoListContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetTodo")]
        public IActionResult GetTodo()
        {
            return Ok(_context.TodoItems.Where(x => x.IsCompleted != true).ToList());
        }

        [HttpPost]
        [Route("AddTodo")]
        public ActionResult<TodoItem> AddTodo( string item)
        {
            if (ModelState.IsValid)
            {
                if (!_context.TodoItems.Any(x => x.Title.ToLower() == item.ToLower()))
                {
                    TodoItem todo = new TodoItem();

                    todo.Title = item;
                    todo.IsActive = true;
                    var addTodo = _context.TodoItems.Add(todo);
                    if (addTodo != null)
                    {
                        _context.SaveChanges();
                    }
                    return Ok(todo);
                }
                return BadRequest("Item already Exists");
                
            }
            return BadRequest(ModelState.Values.ToString());
        }


        [HttpGet]
        [Route("GetTodoItemById/{todoId}")]
        public IActionResult GetTodoItemById(int todoId)
        {
            return Ok(_context.TodoItems.Where(x => x.Id == todoId).FirstOrDefault());
        }


        [HttpPatch]
        [Route("UpdateTodo")]
        public IActionResult UpdateTodo(int id, string title, bool isCompleted)
        {

            var todo = _context.TodoItems.Where(x => x.Id == id).FirstOrDefault();
            if(todo != null)
            {
                todo.Title = title;
                todo.IsCompleted = isCompleted;
                _context.TodoItems.Update(todo);
                _context.SaveChanges();
                return Ok(todo);
            }
            return NotFound("Item not found");

        }

        [HttpDelete]
        [Route("DeleteTodoItem/{todoId}")]
        public IActionResult DeleteTodoItem(int todoId)
        {
            var todoitem = _context.TodoItems.Where(x => x.Id == todoId).FirstOrDefault();
            if (todoitem != null)
            {
                todoitem.IsDeleted = true;
                todoitem.IsActive = false;
                _context.Update(todoitem);
                _context.SaveChanges();
                return Ok(todoitem);
            }
            return NotFound("No Todo Item found");
        }
    }
}
