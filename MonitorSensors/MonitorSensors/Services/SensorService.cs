using Microsoft.EntityFrameworkCore;
using MonitorSensors.Data;
using MonitorSensors.Entities;
using MonitorSensors.Models.Sensor;
using MonitorSensors.Responses.Sensor;
using MonitorSensors.Services.Interfaces;

namespace MonitorSensors.Services;

public class SensorService : ISensorService
{
    private readonly ApplicationDbContext _context;

    public SensorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetIndexModel?> GetIndexModel()
    {
        var indexModel = new GetIndexModel();

        try
        {
            var types = await _context.Types.ToListAsync();
            var units = await _context.Units.ToListAsync();

            var typesModel = new List<TypeModel>();
            foreach (var type in types)
            {
                var typeModel = new TypeModel()
                {
                    Id = type.Id,
                    Name = type.Name,
                };
                typesModel.Add(typeModel);
            }

            var unitsModel = new List<UnitModel>();
            foreach (var unit in units)
            {
                var unitModel = new UnitModel()
                {
                    Id = unit.Id,
                    Name = unit.Name,
                };
                unitsModel.Add(unitModel);
            }

            indexModel.Types = typesModel;
            indexModel.Units = unitsModel;

            return indexModel;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<FilterSensorResponse> Filter(string value)
    {
        value ??= "";

        var response = new FilterSensorResponse();
        try
        {
            var sensors = await _context.Sensors
                .Include(x=> x.Type)
                .Include(x=> x.Unit)
                .Where(x => x.Title.Contains(value)
                            || x.Model.Contains(value)
                            || x.Description.Contains(value)
                            || x.Location.Contains(value)
                            || x.Type.Name.Contains(value)
                            || x.Unit.Name.Contains(value))
                .OrderBy(x => x.Title).ToListAsync();

            var contactsModel = new List<FilterSensorModel>();
            foreach (var sensor in sensors)
            {
                var contactModel = new FilterSensorModel()
                {
                    Id = sensor.Id,
                    Title = sensor.Title,
                    Model = sensor.Model,
                    Location = sensor.Location,
                    Type = sensor.Type.Name,
                    Range = new RangeModel()
                    {
                        From = sensor.RangeFrom,
                        To = sensor.RangeTo
                    },
                    Unit = sensor.Unit.Name
                };
                contactsModel.Add(contactModel);
            }

            response.Items = contactsModel;
            response.Result = FilterSensorResult.Success;
            return response;
        }
        catch (Exception e)
        {
            response.Result = FilterSensorResult.ServerError;
            return response;
        }
    }

    public async Task<GetSensorByIdResponse> GetSensorById(int id)
    {
        var response = new GetSensorByIdResponse();
        try
        {
            var sensor = await _context.Sensors.FirstOrDefaultAsync(x => x.Id == id);
            if (sensor == null)
            {
                response.Result = GetSensorByIdResult.ServerError;
                return response;
            }

            var sensorModel = new GetByIdSensorModel()
            {
                Id = sensor.Id,
                Title = sensor.Title,
                Model = sensor.Model,
                Description = sensor.Description,
                Location = sensor.Location,
                TypeId = sensor.TypeId,
                UnitId = sensor.UnitId,
                Range = new RangeModel()
                {
                    From = sensor.RangeFrom,
                    To = sensor.RangeTo
                },
            };
            response.Item = sensorModel;
            response.Result = GetSensorByIdResult.Success;
            return response;
        }
        catch (Exception e)
        {
            response.Result = GetSensorByIdResult.ServerError;
            return response;
        }
    }

    public async Task<CreateSensorResponse> Create(CreateSensorModel model)
    {
        var response = new CreateSensorResponse();

        if (model == null)
        {
            response.Result = CreateSensorResult.ServerError;
            return response;
        }

        var sensor = new Sensor
        {
            Title = model.Title,
            Model = model.Model,
            Description = model.Description,
            Location = model.Location,
            TypeId = model.TypeId,
            UnitId = model.UnitId,
            RangeFrom = model.Range.From,
            RangeTo = model.Range.To
        };

        try
        {
            _context.Add(sensor);
            await _context.SaveChangesAsync();

            response.Result = CreateSensorResult.Success;
            return response;
        }
        catch (Exception e)
        {
            response.Result = CreateSensorResult.ServerError;
            return response;
        }
    }

    public async Task<UpdateSensorResponse> Update(UpdateSensorModel model)
    {
        var response = new UpdateSensorResponse();
        if (model == null)
        {
            response.Result = UpdateSensorResult.ServerError;
            return response;
        }

        var sensor = new Sensor()
        {
            Id = model.Id,
            Title = model.Title,
            Model = model.Model,
            Description = model.Description,
            Location = model.Location,
            TypeId = model.TypeId,
            UnitId = model.UnitId,
            RangeFrom = model.Range.From,
            RangeTo = model.Range.To
        };

        try
        {
            _context.Update(sensor);
            await _context.SaveChangesAsync();

            response.Result = UpdateSensorResult.Success;
            return response;
        }
        catch (Exception e)
        {
            response.Result = UpdateSensorResult.ServerError;
            return response;
        }
    }

    public async Task<DeleteSensorResponse> Delete(int id)
    {
        var response = new DeleteSensorResponse();

        try
        {
            var sensor = await _context.Sensors.FirstOrDefaultAsync(x => x.Id == id);
            if (sensor == null)
            {
                response.Result = DeleteSensorResult.NotFound;
                return response;
            }

            _context.Remove(sensor);
            await _context.SaveChangesAsync();

            response.Result = DeleteSensorResult.Success;
            return response;
        }
        catch (Exception e)
        {
            response.Result = DeleteSensorResult.ServerError;
            return response;
        }
    }
}