using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Plans.COBRA;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Linq.Expressions;

namespace ClarityImplementation.API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ClarityDbContext _context;

        public GenericRepository(ClarityDbContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByCaseId(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<Api_Notification_Log>> GetEntitiesByCaseId(string caseId)
        {
            
            return await _context.Api_Notification_Logs.Where(log => log.CaseId == caseId).ToListAsync();
        }





        public async Task<bool> Delete(int id)
        {
            var result = await GetById(id);
            if(result is null)
            {
                return false;
            }
            _context.Set<T>().Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteByString(string id)
        {
            var result = await GetByCaseId(id);
            if (result is null)
            {
                return false;
            }
            _context.Set<T>().Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<T> Update(T entity, int id)
        {
            var existingEntity = await GetById(id);
            if(existingEntity is null)
            {
                return null;
            }
            var entityProperties = existingEntity.GetType().GetProperties();
            //foreach (var propertyInfo in entityProperties)
            //{
            //    var dtoValue = entity.GetType().GetProperty(propertyInfo.Name)?.GetValue(entity, null);
            //    if (dtoValue != null)
            //    {
            //        propertyInfo.SetValue(existingEntity, dtoValue, null);
            //    }
            //}
            foreach (var propertyInfo in entityProperties)
            {
                var currentValue = entity.GetType().GetProperty(propertyInfo.Name)?.GetValue(entity, null);
                if (currentValue is string && currentValue != null)
                {
                    if (currentValue.Equals("_blank"))
                    {
                        propertyInfo.SetValue(existingEntity, "", null);
                    }
                    else
                    {
                        propertyInfo.SetValue(existingEntity, currentValue, null);
                    }
                }
                if (currentValue is int intValue && intValue != 0)
                {
                    propertyInfo.SetValue(existingEntity, currentValue, null);
                }
                if (currentValue is DateTime dateValue && dateValue != DateTime.MinValue)
                {
                    propertyInfo.SetValue(existingEntity, currentValue, null);
                }
                if (currentValue is bool)
                {
                    propertyInfo.SetValue(existingEntity, currentValue, null);
                }

            }
            try
            {
                await _context.SaveChangesAsync();
                return existingEntity;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            

        }

        public async Task<T> UpdateByString(T entity, string id)
        {
            var existingEntity = await GetByCaseId(id);
            if (existingEntity is null)
            {
                return null;
            }
            var entityProperties = existingEntity.GetType().GetProperties();
            //foreach (var propertyInfo in entityProperties)
            //{
            //    var dtoValue = entity.GetType().GetProperty(propertyInfo.Name)?.GetValue(entity, null);
            //    if (dtoValue != null)
            //    {
            //        propertyInfo.SetValue(existingEntity, dtoValue, null);
            //    }
            //}
            foreach (var propertyInfo in entityProperties)
            {
                var currentValue = entity.GetType().GetProperty(propertyInfo.Name)?.GetValue(entity, null);
                if (currentValue is string && currentValue != null)
                {
                    if (currentValue.Equals("_blank"))
                    {
                        propertyInfo.SetValue(existingEntity, "", null);
                    }
                    else
                    {
                        propertyInfo.SetValue(existingEntity, currentValue, null);
                    }
                }
                if (currentValue is int intValue && intValue != 0)
                {
                    propertyInfo.SetValue(existingEntity, currentValue, null);
                }
                if (currentValue is DateTime dateValue && dateValue != DateTime.MinValue)
                {
                    propertyInfo.SetValue(existingEntity, currentValue, null);
                }
                if (currentValue is bool)
                {
                    propertyInfo.SetValue(existingEntity, currentValue, null);
                }

            }
            try
            {
                await _context.SaveChangesAsync();
                return existingEntity;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }


        }

        public async Task<T> GetByCompanyId(Expression<Func<T, bool>> predicate)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(predicate);
            return result;
        }

        public async Task<IEnumerable<T>> GetAllByCompanyId(Expression<Func<T, bool>> predicate)
        {
            var result = await _context.Set<T>().Where(predicate).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetAllByPageId(Expression<Func<T, bool>> predicate)
        {
            var result = await _context.Set<T>().Where(predicate).ToListAsync();
            return result;
        }

        public async Task<bool> DeleteByCompanyId(Expression<Func<T, bool>> predicate)
        {
            var entitiesToDelete = await _context.Set<T>().Where(predicate).ToListAsync();

            if (entitiesToDelete.Count == 0)
            {
                return false; // No matching entities found for deletion.
            }

            _context.Set<T>().RemoveRange(entitiesToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<T>> GetAllByStatus(Expression<Func<T, bool>> predicate)
        {
            var result = await _context.Set<T>().Where(predicate).ToListAsync();
            return result;
        }


        //public async Task<T> GetFirstAsync()
        //{
        //    return await _context.Set<T>().FirstOrDefaultAsync();
        //}

        public async Task<T> GetFirstByCOBRAPlanIdAsync(int cobraPlanId)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(e => EF.Property<int>(e, "COBRAPlanId") == cobraPlanId);
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }


    }
}
