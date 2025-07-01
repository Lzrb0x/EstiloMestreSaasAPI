using EstiloMestre.API.Attributes;
using EstiloMestre.API.Controllers.BaseController;
using EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHour.Set;
using EstiloMestre.Application.UseCases.Barbershop.Employee.BusinessHour.WorkingHourOverride.Set;
using EstiloMestre.Application.UseCases.Barbershop.Employee.Register.OwnerAsEmployee;
using EstiloMestre.Application.UseCases.Barbershop.Employee.Register.UserAsEmployee;
using EstiloMestre.Application.UseCases.Barbershop.Employee.ServiceEmployee.Register;
using EstiloMestre.Application.UseCases.Barbershop.Employee.Slots;
using EstiloMestre.Application.UseCases.Barbershop.Register;
using EstiloMestre.Application.UseCases.Barbershop.Service.Register.List;
using EstiloMestre.Application.UseCases.Barbershop.Service.Register.Single;
using EstiloMestre.Communication.DTOs;
using EstiloMestre.Communication.Requests;
using EstiloMestre.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EstiloMestre.API.Controllers;

public class BarbershopController : EstiloMestreBaseController
{
    [Owner]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredBarbershopJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBarbershop(
        [FromServices] IRegisterBarbershopUseCase useCase, [FromBody] RequestRegisterBarbershopJson request
    )
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [OwnerByBarbershop]
    [HttpPost]
    [Route("{barbershopId:long}/employees")]
    [ProducesResponseType(typeof(ResponseRegisteredEmployeeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterEmployee(
        [FromRoute] long barbershopId, [FromBody] RequestRegisterEmployeeJson request,
        [FromServices] IRegisterEmployeeUseCase useCase
    )
    {
        var response = await useCase.Execute(request, barbershopId);
        return Created(string.Empty, response);
    }

    [OwnerByBarbershop]
    [HttpPost]
    [Route("{barbershopId:long}/services/list")]
    [ProducesResponseType(typeof(ResponseRegisteredBarbershopServiceListJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterBarbershopServiceList(
        [FromRoute] long barbershopId, [FromBody] RequestRegisterBarbershopServiceListJson request,
        [FromServices] IRegisterBarbershopServiceListUseCase useCase
    )
    {
        var response = await useCase.Execute(request, barbershopId);
        return Created(string.Empty, response);
    }


    [OwnerByBarbershop]
    [HttpPost]
    [Route("{barbershopId:long}/services")]
    [ProducesResponseType(typeof(BarbershopServiceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterBarbershopService(
        [FromRoute] long barbershopId, [FromBody] RequestBarbershopServiceJson request,
        [FromServices] IRegisterBarbershopServiceUseCase useCase
    )
    {
        var response = await useCase.Execute(request, barbershopId);
        return Created(string.Empty, response);
    }


    [OwnerOrEmployeeByBarbershop]
    [HttpPost]
    [Route("{barbershopId:long}/employees/{employeeId:long}/services")]
    [ProducesResponseType(typeof(ResponseRegisteredServiceEmployeeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterEmployeeService(
        [FromRoute] long barbershopId, [FromRoute] long employeeId,
        [FromBody] RequestRegisterServiceEmployeeJson request, [FromServices] IRegisterServiceEmployeeUseCase useCase
    )
    {
        var response = await useCase.Execute(request, barbershopId, employeeId);
        return Created(string.Empty, response);
    }

    [OwnerByBarbershop]
    [HttpPost]
    [Route("{barbershopId:long}/employees/register-owner-as-employee")]
    [ProducesResponseType(typeof(ResponseRegisteredEmployeeJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterOwnerAsEmployee(
        [FromRoute] long barbershopId,
        [FromServices] IRegisterOwnerAsEmployeeUseCase useCase
    )
    {
        var response = await useCase.Execute(barbershopId);
        return Created(string.Empty, response);
    }

    [OwnerOrEmployeeByBarbershop]
    [HttpPost]
    [Route("{barbershopId:long}/employees/{employeeId:long}/working-hours")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EmployeeWorkingHours(
        [FromRoute] long barbershopId, [FromRoute] long employeeId,
        [FromBody] RequestEmployeeWorkingHourListJson request,
        [FromServices] ISetWorkingHourUseCase useCase
    )
    {
        await useCase.Execute(request, employeeId);
        return NoContent();
    }

    [OwnerOrEmployeeByBarbershop]
    [HttpPost]
    [Route("{barbershopId:long}/employees/{employeeId:long}/override-working-hours")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> OverrideEmployeeWorkingHours(
        [FromRoute] long barbershopId, [FromRoute] long employeeId,
        [FromBody] RequestWorkingHourOverrideJson request,
        [FromServices] ISetWorkingHourOverrideUseCase useCase
    )
    {
        await useCase.Execute(request, employeeId);
        return NoContent();
    }

    [PartialOrCompletedUser]
    [HttpGet]
    [Route("{barbershopId:long}/employees/{employeeId:long}/available-slots")]
    [ProducesResponseType(typeof(ResponseEmployeeSlotsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmployeeAvailableSlots(
        [FromRoute] long employeeId,
        [FromQuery] DateOnly date, [FromQuery] long barbershopServiceId,
        [FromServices] IGetEmployeeSlotsUseCase useCase
    )
    {
        var response = await useCase.Execute(employeeId, date, barbershopServiceId);
        return Ok(response);
    }
}