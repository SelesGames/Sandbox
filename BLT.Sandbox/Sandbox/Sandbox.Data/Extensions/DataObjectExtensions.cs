using System;
using System.Linq;

namespace Sandbox.Data
{
    public static class DataObjectExtensions
    {
        public static IQueryable<T> WithId<T>(this IQueryable<T> entities, Guid id)
            where T : EntityBase
        {
            return entities.Where(o => o.Id == id);
        }

        public static IQueryable<T> WithId<T>(this IQueryable<T> entities, string id)
            where T : EntityBase
        {
            return WithId(entities, Guid.Parse(id));
        }

        public static IQueryable<Group> GetAccessibleGroups(this IQueryable<User> users)
        {
            return users
                .SelectMany(o => o.AccessibleProjects)
                .Select(o => o.Project)
                .Select(o => o.Campaign)
                .Select(o => o.Group);
        }
    }
}
