using System.Text;
using System.Text.Json;
namespace Coflo.Abstractions.Steps.Models;

public class ActivityToken
{
    public long SubscriptionId { get; set; }
    public string ActivityName { get; set; }
    public string Nonce { get; set; }

    public string Encode()
    {
        var json = JsonSerializer.Serialize(this);

        return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
    }

    public static ActivityToken Create(long subscriptionId, string activityName)
    {
        return new ActivityToken
        {
            SubscriptionId = subscriptionId,
            ActivityName = activityName,
            Nonce = Guid.NewGuid().ToString()
        };
    }

    public static ActivityToken Decode(string encodedToken)
    {
        var raw = Convert.FromBase64String(encodedToken);
        var json = Encoding.UTF8.GetString(raw);

        return JsonSerializer.Deserialize<ActivityToken>(json)
               ?? throw new Exception("Invalid token");
    }
}