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

namespace AlatorX.Server.Management.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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

        public async ValueTask<UserForResultDto> RetrieveByIdAsync(long id)
        {
            // TODO: check for exist
            var user = await _userRepository.SelectAll()
                .FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
                throw new AlatorException(404, "User not found");

            return _mapper.Map<UserForResultDto>(user);    
        }
    }
}