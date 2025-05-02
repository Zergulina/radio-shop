using System;
using AuthService.BLL.Dtos;
using AuthService.DAL.Models;

namespace AuthService.BLL.Mappers;

internal static class UserMapper
    {
        public static UserDto ToDto(this User model)
        {
            return new UserDto
            {
                Id = model.Id,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Email = model.Email
            };
        }

        public static UserDto ToDto(this User model, string password)
        {
            return new UserDto
            {
                Id = model.Id,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Email = model.Email,
                Password = password
            };
        }

        public static User ToModel(this UserDto dto)
        {
            return new User
            {
                Id = string.IsNullOrEmpty(dto.Id) ? Guid.NewGuid().ToString() : dto.Id,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                Email = dto.Email
            };
        }
    }