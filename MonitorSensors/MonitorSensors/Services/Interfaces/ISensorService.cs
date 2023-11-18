using MonitorSensors.Models.Sensor;
using MonitorSensors.Responses.Sensor;

namespace MonitorSensors.Services.Interfaces;

public interface ISensorService
{
    public Task<GetIndexModel?> GetIndexModel();
    public Task<FilterSensorResponse> Filter(string value);
    public Task<GetSensorByIdResponse> GetSensorById(int id);
    public Task<CreateSensorResponse> Create(CreateSensorModel model);
    public Task<UpdateSensorResponse> Update(UpdateSensorModel model);
    public Task<DeleteSensorResponse> Delete(int id);
}