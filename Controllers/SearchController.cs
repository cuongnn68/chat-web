using DiscordRipoff.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiscordRipoff.Controllers {
    public class SearchController : Controller {
        private UserServices userServices;

        public SearchController(
            UserServices userServices
        ) {
            this.userServices = userServices;
        }

        [HttpGet("user")]
        public IActionResult SearchUser([FromQuery] SearchModel model) {
            var users = userServices.SearchAsync(model.Id, model.Name);
            return Ok(users);
        }

        //TODO: this
        // [HttpGet("")] 
        // public IActionResult SearchRoom([FromQuery] SearchModel model) {
        //     var rooms = 
        // }


    }

    public class SearchModel {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}