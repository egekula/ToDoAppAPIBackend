﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Abstract;
using ToDoApp.Business.Repositories;
using ToDoApp.Core.Results;
using ToDoApp.Entities.Concrate;
using ToDoApp.Entities.DTOs;

namespace ToDoApp.Business.Concrate
{
    public class ToDoItemManager : IToDoItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ToDoItemManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<ToDoItemInsertDto>> AddAsync(ToDoItemInsertDto dto)
        {
            await _unitOfWork.ToDoItemDal.AddAsync(_mapper.Map<ToDoItem>(dto));
            await _unitOfWork.CommitAsync();
            return new SuccessDataResult<ToDoItemInsertDto>(dto,"İşlem Başarılı");
        }

        public async Task<IDataResult<List<ToDoItemDto>>> GetAllAsync()
        {
            var data = await _unitOfWork.ToDoItemDal.GetAllAsync();
            var toDoItemDto = _mapper.Map<List<ToDoItemDto>>(data);
            return new SuccessDataResult<List<ToDoItemDto>>(toDoItemDto,"İşlem Başarılı");
        }

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

        public async Task<IResult> UpdateAsync(ToDoItemDto dto)
        {
            _unitOfWork.ToDoItemDal.Update(_mapper.Map<ToDoItem>(dto));
            await _unitOfWork.CommitAsync();
            return new SuccessResult("Ürün başarıyla güncellendi");
        }
    }
}
