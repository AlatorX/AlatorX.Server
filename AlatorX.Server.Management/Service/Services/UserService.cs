using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Data.IRepositories;
using AlatorX.Server.Management.Domain.Entities;
using AlatorX.Server.Management.Service.DTOs.Users;
using AlatorX.Server.Management.Service.Exceptions;
using AlatorX.Server.Management.Service.Helpers;
using AlatorX.Server.Management.Service.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AlatorX.Server.Management.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<UserToken> _userTokenRepository;
        private readonly IRepository<UserWebsite> _userWebsiteRepository;
        private readonly IRepository<WebsiteSetting> _websiteSettingRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper,
            IRepository<UserToken> userTokenRepository, 
            IRepository<UserWebsite> userWebsiteRepository, 
            IRepository<WebsiteSetting> websiteSettingRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userTokenRepository = userTokenRepository;
            _userWebsiteRepository = userWebsiteRepository;
            _websiteSettingRepository = websiteSettingRepository;
        }

        public async ValueTask<UserForResultDto> AddAsync(UserForCreationDto dto)
        {
            // TODO: check for exist
            var isExist = _userRepository.SelectAll()
                .Any(u => u.Email.ToLower() == dto.Email.ToLower());
            if(isExist)
                throw new AlatorException(400, "User is already exist in this mail");
            
            var mappedUser = _mapper.Map<User>(dto);
            mappedUser.Password = PasswordHelper.Hash(mappedUser.Password);

            var user = await _userRepository.InsertAsync(mappedUser);

            return _mapper.Map<UserForResultDto>(user);
        }

        public async ValueTask<UserToken> GenerateApiKeyAsync()
        {
            var id = HttpContextHelper.UserId ?? throw new UnauthorizedAccessException();
            var user = await _userRepository.SelectByIdAsync(id);
            if(user == null)
                throw new AlatorException(404, "User not found");

            byte[] randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            var apiKey = Convert.ToBase64String(randomBytes);

            // insert api key
            var userToken = await _userTokenRepository.SelectAll()
                .FirstOrDefaultAsync(ut => ut.UserId == id);
            if(userToken == null)    
            {
                return await _userTokenRepository.InsertAsync(new UserToken
                {
                    ApiKey = apiKey,
                    UserId = id
                });
            }
            else
            {
                userToken.ApiKey = apiKey;
                userToken.UpdatedAt = DateTime.UtcNow;
                await _userTokenRepository.SaveChangesAsync();

                return userToken;
            }
        }

        public async ValueTask<IEnumerable<UserWebsite>> GetAllWebsitesAsync()
        {
            var userId = HttpContextHelper.UserId ?? throw new UnauthorizedAccessException();
            
            return await _userWebsiteRepository.SelectAll()
                .Where(uw => uw.UserId == userId)
                .Include(uw => uw.WebsiteSetting)
                .ThenInclude(ws => ws.Website)
                .ToListAsync();
        }

        public async ValueTask<string> GetApiTokenByUserIdAsync(long userId)
        {
            var userToken = await _userTokenRepository.SelectAll()
                .FirstOrDefaultAsync(ut => ut.UserId == userId);
            if(userToken == null)
                throw new AlatorException(404, "Api key not found, please generate it");
            
            return userToken.ApiKey;
        }

        public async ValueTask<UserForResultDto> GetMeAsync()
        {
            var user = await _userRepository.SelectByIdAsync(HttpContextHelper.UserId ?? throw new UnauthorizedAccessException());
            if(user == null)
                throw new AlatorException(404, "User not found");

            return _mapper.Map<UserForResultDto>(user);
        }

        public async ValueTask<UserForResultDto> RetrieveByIdAsync(long userId)
        {
            // TODO: check for exist
            var user = await _userRepository.SelectAll()
                .FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null)
                throw new AlatorException(404, "User not found");

            return _mapper.Map<UserForResultDto>(user);    
        }
    }
}