using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Abstract;
using ToDoApp.Business.Mapping;
using ToDoApp.Business.Repositories;
using ToDoApp.Core.Results;
using ToDoApp.Entities.Concrate;
using ToDoApp.Entities.DTOs;

namespace ToDoApp.Business.Concrate
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<UserDto>> AddAsync(UserDto dto)
        {
            await _unitOfWork.UserDal.AddAsync(_mapper.Map<User>(dto));
            await _unitOfWork.CommitAsync();

            return new SuccessDataResult<UserDto>("İşlem Başarılı");
        }

        public async Task<IDataResult<UserDto>> GetByIdAsync(Guid id)
        {
            var data = await _unitOfWork.UserDal.GetByIdAsync(id);
            var userDto = _mapper.Map<UserDto>(data);
            if(userDto != null)
            {
                return new SuccessDataResult<UserDto>(userDto,"İşlem Başarılı");
            }
            return new ErrorDataResult<UserDto>("Böyle bir ürün yok");
        }
    }
}
