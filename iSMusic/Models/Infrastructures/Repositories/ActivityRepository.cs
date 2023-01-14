using Antlr.Runtime.Tree;
using isMusic.Infrastructures.Extensions;
using isMusic.Models.DTOs;
using isMusic.Services.Interfaces;
using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace isMusic.Infrastructures.Repositories
{

    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _db;
        public ActivityRepository(AppDbContext db)
        {
            _db = db;
        }


        public Activity IsExistForCreate(ActivityCreateDTO entity)
        {
            Activity model;
            if (entity.id == 0)
            {
                model = _db.Activities.SingleOrDefault(x => x.activityName == entity.activityName);
            }
            else
            {
                model = _db.Activities.SingleOrDefault(x => x.id != entity.id && x.activityName == entity.activityName);
            }

            return model;

        }

        public Activity IsExistForEdit(ActivityEditDTO entity)
        {
            Activity model;
            if (entity.id == 0)
            {
                model = _db.Activities.SingleOrDefault(x => x.activityName == entity.activityName);
            }
            else
            {
                model = _db.Activities.SingleOrDefault(x => x.id != entity.id && x.activityName == entity.activityName);
            }

            return model;

        }



        public IEnumerable<ActivityDTO> GetAll()
        {
            IEnumerable<Activity> data = _db.Activities;
            return data.Select(x => x.ToActivityDTO());
        }

        public ActivityDTO GetById(int id)
        {
            return _db.Activities.SingleOrDefault(x => x.id == id).ToActivityDTO();
        }

        public ActivityDTO GetByActivityName(string activityName)
        {
            return _db.Activities.SingleOrDefault(x => x.activityName == activityName).ToActivityDTO();
        }

        public IEnumerable<ActivityDTO> Search(int? activityId, string activityName, bool? publishedStatus)
        {
            IEnumerable<Activity> query = _db.Activities;
            //if (activityId.HasValue) query = query.Where(x => x.id == activityId);
            //if (!string.IsNullOrEmpty(activityName)) query = query.Where(x => x.activityName.Contains(activityName));
            //if (publishedStatus.HasValue) query = query.Where(x => x.publishedStatus == publishedStatus);
            query = query.OrderBy(x => x.activityName);

            return query.Select(x => x.ToActivityDTO());
        }

        public void Create(ActivityCreateDTO dto)
        {
            _db.Activities.Add(dto.ToActivityEntity());
            _db.SaveChanges();
        }

        public void Edit(ActivityEditDTO dto)
        {
            var data = _db.Activities.Find(dto.id);

            data.activityName = dto.activityName;
            data.publishedStatus = dto.publishedStatus;
            data.activityTypeId = dto.activityTypeId;
            data.activityEndTime = dto.activityEndTime;
            data.activityImagePath = dto.activityImagePath;
            data.checkedById = dto.checkedById;
            data.activityStartTime = dto.activityStartTime;
            data.activityOrganizerId = dto.activityOrganizerId;
            data.activityLocation = dto.activityLocation;
            data.activityInfo = dto.activityInfo;

            //_db.Entry(data).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var activityDelete = _db.Activities.Find(id);
            _db.Activities.Remove(activityDelete);
            _db.SaveChanges();
        }
    }
}