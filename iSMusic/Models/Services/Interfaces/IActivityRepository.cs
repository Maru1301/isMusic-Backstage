using isMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isMusic.Services.Interfaces
{
    public interface IActivityRepository
    {
        //bool IsExist(string activityName);
        //void Create(ActivityDTO dto);
        //IEnumerable<ActivityVM> GetAll();
        IEnumerable<ActivityDTO> Search(int? activityId, string activityName, bool? publishedStatus);

        ActivityDTO GetByActivityName(string activityName);

        ActivityDTO GetById(int id);

        Activity IsExistForCreate(ActivityCreateDTO entity);

        Activity IsExistForEdit(ActivityEditDTO entity);
        IEnumerable<ActivityDTO> GetAll();

        void Create(ActivityCreateDTO entity);

        void Edit(ActivityEditDTO entity);

        void Delete(int id);

    }
}
