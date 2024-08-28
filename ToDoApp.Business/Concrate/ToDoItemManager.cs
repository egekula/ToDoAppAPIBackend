using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Abstract;
using ToDoApp.Business.BusinessAspect.Autofac;
using ToDoApp.Business.Repositories;
using ToDoApp.Business.ValidationRules.FluentValidation;
using ToDoApp.Core.Aspects.Autofac.Caching;
using ToDoApp.Core.Aspects.Autofac.Validation;
using ToDoApp.Core.Entities.Concrate;
using ToDoApp.Core.Utilities.Results;
using ToDoApp.Entities.DTOs;

namespace ToDoApp.Business.Concrate
{
    [ValidationAspect(typeof(ToDoItemInsertValidator))]

    public class ToDoItemManager : IToDoItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ToDoItemManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [SecuredOperation("todo.add,admin")]
        [CacheRemoveAspect("IToDoItemService.Get")]
        public async Task<IDataResult<ToDoItemInsertDto>> AddAsync(ToDoItemInsertDto dto)
        {
            await _unitOfWork.ToDoItemDal.AddAsync(_mapper.Map<ToDoItem>(dto));
            await _unitOfWork.CommitAsync();
            return new SuccessDataResult<ToDoItemInsertDto>(dto,"İşlem Başarılı");
        }
        [CacheAspect]

        public async Task<IDataResult<List<ToDoItemDto>>> GetAllAsync()
        {
            var data = await _unitOfWork.ToDoItemDal.GetAllAsync();
            var toDoItemDto = _mapper.Map<List<ToDoItemDto>>(data);
            return new SuccessDataResult<List<ToDoItemDto>>(toDoItemDto,"İşlem Başarılı");
        }

        [CacheAspect]
        public async Task<IDataResult<ToDoItemDto>> GetByIdAsync(Guid id)
        {
            var data = await _unitOfWork.ToDoItemDal.GetByIdAsync(id);
            var toDoItemDto = _mapper.Map<ToDoItemDto>(data);
            return new SuccessDataResult<ToDoItemDto>(toDoItemDto, "İşlem Başarılı");
        }

        public async Task<IResult> RemoveAsync(Guid id)
        {
            var data = await _unitOfWork.ToDoItemDal.GetByIdAsync(id);
            if (data == null)
            {
                return new ErrorResult("Bu idye sahip ürün bulunamadı");
            }
            _unitOfWork.ToDoItemDal.Delete(data);
            await _unitOfWork.CommitAsync();
            return new SuccessResult("Ürün Başarıyla kaldırıldı.");
        }
        [CacheRemoveAspect("IToDoItemService.Get")]

        public async Task<IResult> UpdateAsync(ToDoItemDto dto)
        {
            _unitOfWork.ToDoItemDal.Update(_mapper.Map<ToDoItem>(dto));
            await _unitOfWork.CommitAsync();
            return new SuccessResult("Ürün başarıyla güncellendi");
        }
    }
}
