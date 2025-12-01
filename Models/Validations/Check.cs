using Airbb.Models.DataLayer;

namespace Airbb.Models.Validations
{
    public static class Check
    {
        public static string OwnerExists(AirbbContext ctx, string ownerId)
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(ownerId))
            {
                // Try to parse the ownerId to int (assuming UserId is int)
                if (int.TryParse(ownerId, out int userId))
                {
                    var owner = ctx.User.FirstOrDefault(u => u.UserId == userId);

                    if (owner == null)
                    {
                        msg = $"User with ID {ownerId} does not exist.";
                    }
                    else if (owner.UserType?.ToLower() != "owner")
                    {
                        msg = $"User with ID {ownerId} is not registered as an owner.";
                    }
                }
                else
                {
                    msg = "Invalid Owner ID format.";
                }
            }
            else
            {
                msg = "Owner ID is required.";
            }
            return msg;
        }
    }
}