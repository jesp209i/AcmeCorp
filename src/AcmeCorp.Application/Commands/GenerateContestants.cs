using AcmeCorp.Infrastructure.Interfaces;
using AcmeCorp.Persistence;
using AcmeCorp.Persistence.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeCorp.Application.Commands
{
    public class GenerateContestants : IRequest<GenerateContestantsResponse>
    {
        public int Amount { get; set; }
    }

    public class GenerateContestantsHandler : IRequestHandler<GenerateContestants, GenerateContestantsResponse>
    {
        private readonly AcmeCorpContext _context;
        private readonly IProductService _productService;
        Random random = new Random(21);
        string[] names = { "Peter", "Michael", "Britney", "Tiffany", "Angela", "Tim", "Cathy", "Maggie", "Daniel", "Albus" };
        string[] surnames = { "Hansen", "Jensen", "Voigh", "Brody", "Kierkegaard", "Goethe", "Nietzche", "Noller" };
        string[] endings = { ".dk", ".com", ".net" }; 

        public GenerateContestantsHandler(AcmeCorpContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        public async Task<GenerateContestantsResponse> Handle(GenerateContestants request, CancellationToken cancellationToken)
        {
            var contestants = await _context.Contestants.ToListAsync();
            var usedNumbers = contestants.Select(x => x.SerialNumber).ToList();
            var products = await _productService.GetProducts();
            var availableNumbers = products.Select(x => x.SerialNumber).ToList();
            usedNumbers.ForEach(x => availableNumbers.Remove(x));
            var newCount = contestants.Count + request.Amount;
            if (newCount <= 100)
            {
                for (int i = 0; i < request.Amount; i++)
                {
                    Contestant contestant = CreateContestant(availableNumbers[i]);
                    _context.Contestants.Add(contestant);
                }
                await _context.SaveChangesAsync();
                return new GenerateContestantsResponse(100 - newCount);
            }
            return new GenerateContestantsResponse(100 - contestants.Count);
        }

        private Contestant CreateContestant(string serialNumber)
        {
            var name = names[random.Next(names.Length)];
            var surname = surnames[random.Next(surnames.Length)];
            var email = $"{name}@{surname}{endings[random.Next(endings.Length)]}";

            return Contestant.Create(name, surname, email, serialNumber, true, true);
        }
    }
    public class GenerateContestantsResponse
    {
        public GenerateContestantsResponse(int remaining)
        {
            Remaining = remaining;
        }
        public int Remaining { get; set; }
    }
}
