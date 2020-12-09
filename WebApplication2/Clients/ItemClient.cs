using GeocachingAPI.Entities;
using GeocachingAPI.Models;

namespace GeocachingAPI.Clients
{
    public class ItemClient
    {
        public static ItemEntity Save(ItemRequest request)
        {
            using (var db = new GeocachingContext())
            {
                var item = new ItemEntity
                {
                    Name = request.Name,
                    CacheId = request.CacheId,
                    ActiveStartDate = request.ActiveStartDate,
                    ActiveEndDate = request.ActiveEndDate
                };

                db.Items.Add(item);
                db.SaveChanges();
                return item;
            }
        }
    }
}
