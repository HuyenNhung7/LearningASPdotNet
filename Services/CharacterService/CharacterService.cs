global using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningASPdotNet.Data;

namespace LearningASPdotNet.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        // cái trong dấu { ... } là 2 cái đối tướng mới khởi tạo trong list á
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character {Id = 1, Name = "Sam"}
        };
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            var dbCharacter =  _context.Characters;
            await dbCharacter.AddAsync(character);
            await _context.SaveChangesAsync();
            // var dbCharacter = await _context.Characters.AddAsync(character);
            Console.WriteLine($"dbCharacter là {dbCharacter}");
            character.Id = characters.Max(c => c.Id) + 1;
            // characters.Add(character);
            // serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(characters);
            var Characters = await _context.Characters.ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(Characters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
             var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                // var character = characters.FirstOrDefault(c => c.Id == id);
                var dbCharacter = _context.Characters;
                var character = await dbCharacter.FirstOrDefaultAsync(c => c.Id == id);
                if(character is null)
                    throw new Exception($"Character with Id '{id}' not found.");
                // characters.Remove(character);
                dbCharacter.Remove(character);
                await _context.SaveChangesAsync();
                var Characters = await dbCharacter.ToListAsync();
                serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(Characters);
                // serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            // serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(characters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
            // throw new Exception("Character not found");
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
                var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                if(character is null)
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");
                // _mapper.Map<Character>(updatedCharacter);
                // _mapper.Map(updatedCharacter, character);
                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }
    }
}