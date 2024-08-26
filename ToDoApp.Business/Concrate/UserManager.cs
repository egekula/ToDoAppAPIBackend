using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Abstract;
using ToDoApp.Business.BusinessAspect.Autofac;
using ToDoApp.Business.Mapping;
using ToDoApp.Business.Repositories;
using ToDoApp.Core.Entities.Concrate;
using ToDoApp.Core.Utilities.Results;
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

        //public async Task<IDataResult<User>> AddAsync(User dto)
        //{
        //    await _unitOfWork.UserDal.AddAsync(dto);
        //    await _unitOfWork.CommitAsync();

        //    return new SuccessDataResult<User>("İşlem Başarılı");
        //}

        //public async Task<IDataResult<User>> GetByMail(string mail)
        //{
        //    var data = await _unitOfWork.UserDal.GetAllAsync(u=>u.Email == mail);
        //    var converter = data.FirstOrDefault();
        //    if (converter != null)
        //    {
        //        return new SuccessDataResult<User>(converter,"Success");
        //    }
        //    //var data = await _unitOfWork.UserDal.GetByIdAsync(id);
        //    //var userDto = _mapper.Map<UserDto>(data);
        //    //if(userDto != null)
        //    //{
        //    //    return new SuccessDataResult<UserDto>(userDto,"İşlem Başarılı");
        //    //}
        //    return new ErrorDataResult<User>("Böyle bir ürün yok");
        //}

        //public async Task<IDataResult<List<OperationClaim>>> GetClaims(User user)
        //{
        //    var data = await _unitOfWork.UserDal.GetClaims(user);
        //    return new SuccessDataResult<List<OperationClaim>>(data,"claim");
        //}

        public async Task<IDataResult<User>> AddAsync(User dto)
        {
            await _unitOfWork.UserDal.AddAsync(dto);
            await _unitOfWork.CommitAsync();
            return new SuccessDataResult<User>(dto,"Success");
        }

        public async Task<IDataResult<User>> GetByMail(string mail)
        {
            var data = await _unitOfWork.UserDal.Get(u=>u.Email == mail);
            
            return new SuccessDataResult<User>(data, "mesahj");
        }

        public async Task<IDataResult<List<OperationClaim>>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(await _unitOfWork.UserDal.GetClaims(user), "Success");
        }
    }
}
