using AutoMapper;
using Entity.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.IService;

namespace API.Controllers;
[ApiController]
public class AdminController:ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly ICommunicationService _communicationService;
    private APIResponse _response;
    private readonly IMapper _mapper;

    public AdminController(IAdminService adminService, ICommunicationService communicationService, IMapper mapper)
    {
         _adminService = adminService;
        _communicationService = communicationService;
        _mapper = mapper;
        _response = new();
    }

    
}
