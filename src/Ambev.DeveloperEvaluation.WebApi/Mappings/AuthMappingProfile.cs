using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUserFeature;
using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();
        CreateMap<AuthenticateUserResult, AuthenticateUserResponse>();
    }
}
