using GeocachingAPI.Entities;
using GeocachingAPI.Models;

namespace GeocachingAPI.Clients
{
    public class GeocacheClient
    {
        public static GeocacheEntity Save(GeocacheRequest request)
        {
            using (var db = new GeocachingContext())
            {
                var geocache = new GeocacheEntity
                {
                    Name = request.Name,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude
                };

                db.Geocaches.Add(geocache);
                db.SaveChanges();
                return geocache;
            }
        }
    }
}
