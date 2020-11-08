using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeCorp.Persistence.Models
{
    public class Contestant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SerialNumber { get; set; }
        public bool ConfirmedAgeRequirement { get; set; }
        public bool AcceptedTerms { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public static Contestant Create(string firstname, string lastName, string email, string serialNumber, bool confirmedAgeRequirement, bool acceptedTerms)
        => new Contestant
        {
            FirstName = firstname,
            LastName = lastName,
            Email = email,
            SerialNumber = serialNumber,
            ConfirmedAgeRequirement = confirmedAgeRequirement,
            AcceptedTerms = acceptedTerms,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }
}
