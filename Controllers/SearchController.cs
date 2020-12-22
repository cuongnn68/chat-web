using System.Linq;
using System.Threading.Tasks;
using DiscordRipoff.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiscordRipoff.Controllers {
    [ApiController]
    [Route("/api/search")]
    public class SearchController : Controller {
        private UserServices userServices;
        private SearchService searchService;

        public SearchController(
            UserServices userServices,
            SearchService searchService
        ) {
            this.userServices = userServices;
            this.searchService = searchService;
        }

        // [HttpGet("user")]
        // public IActionResult SearchUser([FromQuery] SearchModel model) {
        //     var users = userServices.SearchAsync(model.Id, model.Name);
        //     return Ok(users);
        // }

        //TODO: this
        // [HttpGet("")] 
        // public IActionResult SearchRoom([FromQuery] SearchModel model) {
        //     var rooms = 
        // }

        [HttpGet("user")]
        public async Task<IActionResult> SearchUser([FromQuery] string keyword) {
            var users = await searchService.SearchAllUserAsync(keyword);
            return Ok(users);
        }

        [HttpGet("room")]
        public async Task<IActionResult> SearchRoom([FromQuery] string keyword) {
            var rooms = await searchService.SearchAllRoomAsync(keyword);
            return Ok(rooms);
        }


    }

    public class SearchModel {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}