using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using DataExplorerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DataExplorerApi
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }

    public class ActivityStep
    {
        public string Step { get; set; }
        public string StepName { get; set; }
    }

    public class EventInfo
    {
        public string Event { get; set; }
        public string EventName { get; set; }
    }

    public class ActivityInfo
    {
        public string Activity { get; set; }
        public string ActivityName { get; set; }
        public List<ActivityStep> Steps { get; set; } = new List<ActivityStep>();
        public List<EventInfo> Events { get; set; } = new List<EventInfo>();
    }

    public class StatementInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SubjectName { get; set; }
        public string PredicateName { get; set; }
        public string ObjectName { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class DataAccessController : ControllerBase
    {
        private readonly VastDataContext _context;

        public DataAccessController(VastDataContext context)
        {
            _context = context;
        }

        [HttpGet("concepts")]
        public IActionResult Concepts(string? query = null)
        {
            var tempResults = _context.PrimaryIndices
                .Where(pi => pi.Concept != null && pi.ConceptName != null);

            if (!string.IsNullOrEmpty(query))
            {
                tempResults = tempResults.Where(pi => pi.ConceptName.ToLower().Contains(query.ToLower()));
            }

            return Ok(tempResults
                .GroupBy(pi => pi.Concept)
                .Select(g => new
                {
                    Concept = g.Key,
                    ConceptName = g.First().ConceptName
                })
                .ToList());
        }

        // The route becomes: api/DataAccess/search?searchType={searchType}&conceptId={conceptId}
        [HttpGet("search")]
        public IActionResult Search(int searchType, string? conceptId = null, string? id = null)
        {
            switch (searchType)
            {
                case 0:
                    return Ok(GetActivitiesByConcept(conceptId, id));
                case 1:
                    return Ok(GetEventsByConcept(conceptId));
                case 2:
                    return Ok(GetProductsByConcept(conceptId));
                case 3:
                    return Ok(GetStatementsByConcept(conceptId));
                default:
                    return BadRequest("Invalid search type.");
            }
        }

        [HttpGet("product-images")]
        public IActionResult ProductImages([FromQuery] string? activityId,[FromQuery] string[] eventId = null,[FromQuery] string[] stepId = null)
        {
            var tempResult = _context.PrimaryIndices
                .Where(pi => pi.Activity == activityId);

            if (eventId.Length>0)
            {
                tempResult = tempResult.Where(pi => eventId.Contains(pi.Event));
            }

            if (stepId.Length>0)
            {
                tempResult = tempResult.Where(pi => stepId.Contains(pi.Step));
            }

            var tempResult2 = tempResult.Select(pi => pi.Product)
                .Distinct();

            return Ok(_context.ProductIndices
                .Where(p => tempResult2.Contains(p.Object))
                .Select(p => p.VastImageUriref)
                .Distinct()
                .Where(p => p != null)
                .ToList()
            );
        }

        private List<ActivityInfo> GetActivitiesByConcept(string? conceptId = null, string? id = null)
        {
            IQueryable<string> tempResult;

            if (id == null)
            {
                tempResult = _context.PrimaryIndices
                    .Where(pi => pi.Concept == conceptId || conceptId == null)
                    .Select(pi => pi.Activity)
                    .Distinct();

            }
            else
            {
                tempResult = _context.PrimaryIndices
                    .Where(pi => pi.Activity == id)
                    .Select(pi => pi.Activity)
                    .Distinct();
            }

            // 1. Fetch the activity data
            var activitiesData = _context.ActivityIndices
                .Where(a => tempResult.Contains(a.Activity))
                .Select(a => new
                {
                    a.Activity,
                    a.ActivityName,
                    a.Step,
                    a.StepName
                })
                .ToList();

            // 2. Process and shape the data in memory
            var groupedActivities = activitiesData
                .GroupBy(a => new { a.Activity, a.ActivityName })
                .Select(group => new ActivityInfo
                {
                    Activity = group.Key.Activity,
                    ActivityName = group.Key.ActivityName,
                    Steps = group.Select(g => new ActivityStep
                    {
                        Step = g.Step,
                        StepName = g.StepName
                    })
                        .DistinctBy(step => new { step.Step, step.StepName }) // Ensuring unique steps
                        .ToList()
                })
                .ToList();

            //Go through the grouped activities and add the events
            foreach (var activity in groupedActivities)
            {
                activity.Events = _context.EventIndices
                    .Where(e => e.Activity == activity.Activity)
                    .Select(e => new EventInfo
                    {
                        Event = e.Event,
                        EventName = e.EventName
                    })
                    .ToList();
            }

            return groupedActivities;
        }

        private List<string> GetEventsByConcept(string conceptId)
        {
            return _context.PrimaryIndices
                           .Where(pi => pi.Concept == conceptId)
                           .Select(pi => pi.Event)
                           .Distinct()
                           .ToList();
        }

        private List<ProductIndex> GetProductsByConcept(string? conceptId)
        {
            var uniqueMatchingProducts = _context.PrimaryIndices
                           .Where(pi => pi.Concept == conceptId || conceptId == null)
                           .Select(pi => pi.Product)
                           .Distinct();

            return _context.ProductIndices
                .Where(p => uniqueMatchingProducts.Contains(p.Object))
                .ToList();
        }

        private List<StatementInfo> GetStatementsByConcept(string? conceptId)
        {
            var tempResults = _context.PrimaryIndices
                           .Where(pi => pi.Concept == conceptId || conceptId == null)
                           .Select(pi => pi.Statement)
                           .Distinct()
                           .ToList();

            return _context.StatementIndices
                .Where(s => tempResults.Contains(s.Object))
                .Select(s => new StatementInfo
                {
                    Id = s.Object,
                    SubjectName = s.SubjectNameDescription,
                    PredicateName = s.PredicateNameDescription,
                    ObjectName = (s.ObjectRelationNameDescription ?? "") + (s.StatementObjectNameDescription ?? ""),
                    Name = s.Name
                })
                .ToList();
        }
    }
}
