using Antlr.Runtime.Tree;
using isMusic.Models.DTOs;
using isMusic.Services.Interfaces;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace isMusic.Services
{
    public class ActivityService
    {
        private readonly IActivityRepository _repository;

        //這個最後要拿掉
        private AppDbContext db = new AppDbContext();

        public ActivityService(IActivityRepository repo)
        {
            this._repository = repo;
        }
        //public (bool IsSuccess, string ErrorMessage) CreateNewActivity(ActivityDTO dto)
        //{
        //    if (_repository.IsExist(dto.activityName))
        //    {
        //        return (false, "活動已存在");
        //    }

        //    _repository.Create(dto);
        //    return (true, null);
        //}

        //public IEnumerable<ActivityVM> GetAll()
        //{
        //    return _repository.GetAll();
        //}
        public IEnumerable<ActivityDTO> Search(int? activityId, string activityName)
            => _repository.Search(activityId, activityName, true);

        public ActivityDTO GetByActivityName(string currentActivityAccount)
            => _repository.GetByActivityName(currentActivityAccount);

        public ActivityDTO GetById(int id)
            => _repository.GetById(id);

        public void Create(ActivityCreateDTO dto)
        {
            if (ActivityExistsCheckForCreate(dto) == true)
                throw new Exception("此活動已存在");

            _repository.Create(dto);
        }

        public void Edit(ActivityEditDTO dto)
        {

            if (ActivityExistsCheckForEdit(dto) == true) throw new Exception("此活動已存在");

            _repository.Edit(dto);
        }

        private bool ActivityExistsCheckForCreate(ActivityCreateDTO dto)
        {
            var model = _repository.IsExistForCreate(dto);
            return model != null;
        }

        private bool ActivityExistsCheckForEdit(ActivityEditDTO dto)
        {
            var model = _repository.IsExistForEdit(dto);
            return model != null;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}