using GeocachingAPI.Entities;
using GeocachingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeocachingAPI.Clients
{
    public class GeocacheClient
    {
        private readonly GeocachingContext _context;
        public GeocacheClient(GeocachingContext context)
        {
            _context = context;
        }

        // Create and add a new Geocache to the DB
        public GeocacheEntity Save(GeocacheRequest request)
        {
            using (var db = _context)
            {
                var geocache = new GeocacheEntity
                {
                    Name = request.Name,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude
                };

                db.Geocache.Add(geocache);
                db.SaveChanges();
                return geocache;
            }
        }

        // Update an existing Geocache in the DB
        public GeocacheEntity Save(uint id, GeocacheRequest request)
        {
            using (var db = _context)
            {
                var geocache = db.Geocache.First(x => x.Id == id);

                geocache.Name = request.Name;
                geocache.Latitude = request.Latitude;
                geocache.Longitude = request.Longitude;

                db.SaveChanges();
                return geocache;
            }
        }

        // Return a list of Geocaches based on query parameters
        // If no query parameters are given, the method will return all geocaches in the database
        public List<GeocacheEntity> Get(GeocacheQuery query)
        {
            using (var db = _context)
            {
                var result = db.Geocache.AsNoTracking();

                // If a name is given in the query, the database will return geocaches with matching names
                if (!string.IsNullOrWhiteSpace(query.Name))
                {
                    result = result.Where(x => x.Name.ToLower() == query.Name.ToLower());
                }

                // Latitude and longitude are included in the query object but not used here
                // With further time and research a query could be built, but as of right now
                // the proper search method for finding an object via latitude/longitude is unknown

                return result.ToList();
            }
        }

        // Return a single Geocache based on specified unique identifier
        public GeocacheEntity Get(uint id)
        {
            using (var db = _context)
            {
                var result = db.Geocache.FirstOrDefault(x => x.Id == id);

                if (result == null)
                {
                    // throw error message if a Geocache was not found with specified ID
                    throw new ArgumentException("Geocache with id: " + id + " was not found.");
                }

                return result;
            }
        }
    }
}
