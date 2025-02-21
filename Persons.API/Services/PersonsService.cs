using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persons.API.Constants;
using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Persons;
using Persons.API.Services.Interfaces;

namespace Persons.API.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly PersonsDbContext _context;
        private readonly IMapper _mapper;

        public PersonsService(PersonsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<PersonDto>>> GetListAsync()
        {
            var personsEntity = await _context.Persons.ToListAsync();

            //var personsDto = new List<PersonDto>();
            
            //foreach (var person in personsEntity)
            //{
            //    personsDto.Add(new PersonDto          ... ya no se usa esto, se usa automapper
            //    {
            //        Id = person.Id,
            //        FirstName = person.FirstName,
            //        LastName = person.LastName,
            //        DNI = person.DNI,
            //        Gender = person.Gender,
            //    });

            //}

            var personsDto = _mapper.Map<List<PersonDto>>(personsEntity);

            return new ResponseDto<List<PersonDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = personsEntity.Count() > 0 ? "Registros encontrados" : "No se encontraron registros",
                Data = personsDto
            };
        }

        public async Task<ResponseDto<PersonDto>> GetOneByIdAsync(Guid id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (personEntity == null)
            {
                return new ResponseDto<PersonDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }
            return new ResponseDto<PersonDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro encontrado",
                Data = _mapper.Map<PersonDto>(personEntity) // automapper 
                //Data = new PersonDto
                //{
                //    Id = personEntity.Id,
                //    FirstName = personEntity.FirstName,    ....ya no se hace esto, se usa automapper
                //    LastName = personEntity.LastName,   
                //    DNI = personEntity.DNI,
                //    Gender = personEntity.Gender
                //}
            };
        }

        public async Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto dto)
        {
            //var personEntity = new PersonEntity
            //{
            //    Id = Guid.NewGuid(),
            //    FirstName = dto.FirstName,
            //    LastName = dto.LastName,
            //    DNI = dto.DNI,
            //    Gender = dto.Gender,
            //};

            var personEntity = _mapper.Map<PersonEntity>(dto); // automapper

            _context.Persons.Add(personEntity);
            await _context.SaveChangesAsync();

            //var response = new PersonActionResponseDto
            //{
            //    Id = personEntity.Id,                 :: ::
            //    FirstName = personEntity.FirstName,
            //    LastName = personEntity.LastName,
            //};


            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado Correctamente",
                Data = _mapper.Map<PersonActionResponseDto>(personEntity) //:: :: Automapper 
            };
        }
        
        public async Task<ResponseDto<PersonActionResponseDto>> EditAsync(PersonEditDto dto, Guid id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

            if (personEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            //personEntity.FirstName = dto.FirstName;
            //personEntity.LastName = dto.LastName;         :: :: 
            //personEntity.DNI = dto.DNI;
            //personEntity.Gender = dto.Gender;

            _mapper.Map<PersonEditDto, PersonEntity>(dto, personEntity); //:: :: Automapper

            _context.Persons.Update(personEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro modificado correctamente",
                //Data = new PersonActionResponseDto
                //{
                //    Id = personEntity.Id,         :: :: 
                //    FirstName = dto.FirstName,
                //    LastName = dto.LastName,
                //}
                Data = _mapper.Map<PersonActionResponseDto>(personEntity)  // :: :: Automapper
            };

        }
        
        public async Task<ResponseDto<PersonActionResponseDto>> DeleteAsync(Guid id)
        {
            var personEntity = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);
            if (personEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            _context.Persons.Remove(personEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro borrado correctamente",
                Data = _mapper.Map<PersonActionResponseDto>(_context)
                //Data = new PersonActionResponseDto
                //{
                //    Id = personEntity.Id,
                //    FirstName = personEntity.FirstName,
                //    LastName = personEntity.LastName,
                //}
            };
        }
    }
}
