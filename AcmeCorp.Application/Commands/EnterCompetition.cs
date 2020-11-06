using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcmeCorp.Application.Commands
{
    public class EnterCompetition : IRequest<EnterCompetitionResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool ConfirmsCorrectAge { get; set; }
        public bool AcceptsTerms { get; set; }
    }
    public class EnterCompetitionValidator : AbstractValidator<EnterCompetition>
    {
        public EnterCompetitionValidator()
        {
            RuleFor(x => x.ConfirmsCorrectAge).Equal(true).WithMessage("You must be 18 to enter");
            RuleFor(x => x.AcceptsTerms).Equal(true).WithMessage("You must accept the terms and condition to enter");
            RuleFor(x => x.Email).EmailAddress();
        }
    }

    public class EnterCompetitionResponse
    {
    }
}
