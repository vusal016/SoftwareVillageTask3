namespace StreamVibe.Application.Features.Profile.Queries.GetProfile
{
    public sealed class GetProfileQueryHandler(IStreamDb streamDb,IMapper mapper) : IRequestHandler<GetProfileQuery, ProfileDto>
    {
        public async Task<ProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
           var user= await streamDb.Users.Where(u=>u.Id==request.UserId).AsNoTracking()
                .Include(u => u.UserSubscriptions)
                .ThenInclude(u=>u.PricingPlan).FirstOrDefaultAsync(cancellationToken);
            if(user is null) throw new KeyNotFoundException($"User not found");

            return mapper.Map<ProfileDto>(user);
        }
    }
}
