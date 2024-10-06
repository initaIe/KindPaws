namespace KindPaws.Application.Volunteers.UpdateMainInfo.DTOs;

public record UpdateVolunteerMainInfoRequest(
    Guid ModuleId,
    string Description,
    AddressDTO Address,
    int? Experience,
    IEnumerable<SocialNetworkDTO> SocialNetworks,
    IEnumerable<RequisiteDTO> Requisites);