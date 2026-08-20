namespace StreamVibe.Application.Features.Faqs.Queries.GetAllFaqs
{
    public record GetAllFaqsQuery() : IRequest<List<FaqDto>>;
}
