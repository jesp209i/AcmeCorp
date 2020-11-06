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

        }
    }

    public class EnterCompetitionResponse
    {
    }
}
