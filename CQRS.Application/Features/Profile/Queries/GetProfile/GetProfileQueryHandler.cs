namespace StreamVibe.Application.Features.Profile.Queries.GetProfile
{
    public sealed class GetProfileQueryHandler(IStreamDb streamDb,IMapper mapper) : IRequestHandler<GetProfileQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user= await streamDb.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == request.UserId,cancellationToken)
            ?? throw new UnauthorizedAccessException("Invalid Token");
            
            return mapper.Map<UserDto>(user);
        }
    }
}
