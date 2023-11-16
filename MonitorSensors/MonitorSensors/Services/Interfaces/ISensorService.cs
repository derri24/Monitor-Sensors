using MonitorSensors.Models;
using MonitorSensors.Responses;

namespace MonitorSensors.Services.Interfaces;

public interface ISensorService
{
    public Task<FilterSensorResponse> Filter(string value);
    public Task<GetSensorByIdResponse> GetContactById(int id);
    public Task<CreateSensorResponse> Create(CreateSensorModel model);
    public Task<UpdateSensorResponse> Update(UpdateSensorModel model);
    public Task<DeleteSensorResponse> Delete(int id);
}