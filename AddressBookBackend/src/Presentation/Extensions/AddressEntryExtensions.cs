using AddressBook.src.Application.Filters;
using AddressBook.src.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.src.Presentation.Extensions
{
    public static class AddressEntryExtensions
    {
        public static IQueryable<AddressEntry> ApplyFilters(this IQueryable<AddressEntry> query, AddressEntryQueryParameters parameters)
        {
            // Filtering
            if (!string.IsNullOrEmpty(parameters.Search))
                query = query.Where(e => e.FullName.Contains(parameters.Search) || e.Email.Contains(parameters.Search));

            if (parameters.JobId.HasValue)
                query = query.Where(e => e.JobId == parameters.JobId);

            if (parameters.DepartmentId.HasValue)
                query = query.Where(e => e.DepartmentId == parameters.DepartmentId);

            if (parameters.FromDate.HasValue)
                query = query.Where(e => e.DateOfBirth >= parameters.FromDate.Value);

            if (parameters.ToDate.HasValue)
                query = query.Where(e => e.DateOfBirth <= parameters.ToDate.Value);

            return query;
        }

        public static IQueryable<AddressEntry> ApplySorting(this IQueryable<AddressEntry> query, string sortBy, string sortOrder)
        {
            return sortOrder.ToLower() == "desc"
                ? query.OrderByDescending(e => EF.Property<object>(e, sortBy))
                : query.OrderBy(e => EF.Property<object>(e, sortBy));
        }
    }

}

