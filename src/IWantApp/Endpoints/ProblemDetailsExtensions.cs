using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;

namespace IWantApp.Endpoints;

public static class ProblemDetailsExtensions
{
    public static Dictionary<string, string[]> ConvertToProblemDetail(this IReadOnlyCollection<Notification> notifications) 
    {
        return notifications
                .GroupBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
    }

    public static Dictionary<string, string[]> ConvertToProblemDetail(this IEnumerable<IdentityError> errors)
    {
        var dictionary = new Dictionary<string, string[]>();
        dictionary.Add("Errors", errors.Select(e => e.Description).ToArray());
        return dictionary;
        //return errors
        //        .GroupBy(g => g.Code)
        //        .ToDictionary(g => g.Key, g => g.Select(x => x.Description).ToArray());
    }
}
