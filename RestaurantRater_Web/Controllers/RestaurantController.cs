using RestaurantRater_Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater_Web.Controllers
{
    public class RestaurantController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> CreateRestaurant([FromBody] Restaurant model)
        {
            if (model == null)
                return BadRequest("Request body cannot be empty.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var db = new ApplicationDbContext())
            {
                db.Restaurants.Add(model);
                var wasSaved = await db.SaveChangesAsync() == 1;

                if (!wasSaved)
                    return InternalServerError();
            }


            return Ok("Restaurant successfully added to db.");
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAllRestaurants()
        {
            using (var db = new ApplicationDbContext())
            {
                var restaurants = await db.Restaurants.ToListAsync();

                return Ok(restaurants);
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetRestaurantById([FromUri] int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var restaurant = await db.Restaurants.FindAsync(id);

                if (restaurant == null)
                    return NotFound();

                return Ok(restaurant);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri] int id, [FromBody] Restaurant updatedRestaurant)
        {
            if (id != updatedRestaurant?.Id)
                return BadRequest("Id mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using(var db = new ApplicationDbContext())
            {
                var restaurant = await db.Restaurants.FindAsync(id);

                if (restaurant == null)
                    return NotFound();

                restaurant.Name = updatedRestaurant.Name;
                restaurant.Address = updatedRestaurant.Address;
                restaurant.Rating = updatedRestaurant.Rating;

                await db.SaveChangesAsync();

                return Ok("Restaurant successfully updated.");
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant([FromUri] int id)
        {
            using(var db = new ApplicationDbContext())
            {
                var restaurant = await db.Restaurants.FindAsync(id);

                if (restaurant == null)
                    return NotFound();

                db.Restaurants.Remove(restaurant);

                var wasRemoved = await db.SaveChangesAsync() == 1;

                if (!wasRemoved)
                    return InternalServerError();

                return Ok("Restaurant successfully removed from db.");
            }
        }
    }
}
