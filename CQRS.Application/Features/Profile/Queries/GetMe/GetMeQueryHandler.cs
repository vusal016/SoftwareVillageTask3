namespace StreamVibe.Application.Features.Profile.Queries.GetProfile
{
    public sealed class GetMeQueryHandler(IStreamDb streamDb,IMapper mapper) : IRequestHandler<GetMeQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetMeQuery request, CancellationToken cancellationToken)
        {
            var user= await streamDb.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == request.UserId,cancellationToken)
            ?? throw new UnauthorizedAccessException("Invalid Token");
            
            return mapper.Map<UserDto>(user);
        }
    }
}
