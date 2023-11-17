using Microsoft.AspNetCore.Mvc;
using MonitorSensors.Models;
using MonitorSensors.Responses;
using MonitorSensors.Services.Interfaces;

namespace MonitorSensors.Controllers;

[Route("[controller]/[action]")]
public class SensorController: Controller
{
    private ISensorService _sensorService;
    
    public SensorController( ISensorService sensorService)
    {
        _sensorService = sensorService;
    }

    [Route("/")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = await _sensorService.GetIndexModel();
        if(response != null)
            return RedirectToAction("Error");
        return View(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> Error()
    {
        return View();
    }

    [HttpGet]
    public async Task<FilterSensorResponse> Filter([FromQuery] string value)
    {
        return await _sensorService.Filter(value);
    }
    
    [HttpGet]
    public async Task<GetSensorByIdResponse> GetSensorById([FromQuery]int id)
    {
        return await _sensorService.GetSensorById(id);
    }

    [HttpPost]
    public async Task<CreateSensorResponse> Create([FromBody] CreateSensorModel model)
    {
        return await _sensorService.Create(model);
    }
    
    [HttpPost]
    public async Task<UpdateSensorResponse> Update([FromBody] UpdateSensorModel model)
    {
        return await _sensorService.Update(model);
    }
    
    [HttpDelete]
    public async Task<DeleteSensorResponse> Delete([FromQuery]int id)
    {
        return await _sensorService.Delete(id);
    }

}