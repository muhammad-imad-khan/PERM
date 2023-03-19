using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Perm.Common.APIModel;
using Perm.DataAccessLayer.Database.SqlServer;
using Perm.DataAccessLayer.DataRepository.Core.Abstraction;
using Perm.DataAccessLayer.DataRepository.Core.Model;
using Perm.Model.Abstraction;
using Perm.Model.Abstraction.Enum;
using System.Reflection;

namespace Perm.DataAccessLayer.DataRepository.Core
{
    public class EntityWithCustomFieldRepository<T> : Repository<T>, IEntityWithCustomFieldRepository<T> where T : CustomFieldModelBase
    {
        private readonly ICustomFieldMetaRepository _customFieldMetaRepository;
        private readonly ICustomFieldRepository _customFieldRepository;

        /// <inheritdoc />
        public EntityWithCustomFieldRepository(PermDataContext dataContext, ICustomFieldRepository customFieldRepository,
            ICustomFieldMetaRepository customFieldMetaRepository, IHttpContextAccessor httpContextAccessor) : base(dataContext, httpContextAccessor)
        {
            _customFieldRepository = customFieldRepository;
            _customFieldMetaRepository = customFieldMetaRepository;
        }

        /// <inheritdoc />
        public async Task<EntityWithCustomFieldList<T>> GetAllWithCustomFieldAsync(EnumEntityType entityType, Func<T, long> primaryKey)
        {
            List<CustomFieldMetaModel> customFieldMeta = await _customFieldMetaRepository.GetMetaByEntityTypeAsync(entityType).ToListAsync();

            List<CustomFieldModel> customFields = _customFieldRepository.GetByEntityTypeIDAsync(entityType).ToList();

            PaginationList<T> recordsWithPaginationApplied = await GetAllWithPagination(applyFilter: true).ToPaginationListAsync();
            List<T> extractedList = recordsWithPaginationApplied.List;

            if (extractedList != null && extractedList.FirstOrDefault() is not null)
            {
                foreach (T record in extractedList)
                {
                    record.CustomFields = new List<ReqCustomFieldModel>();
                    CustomFieldModel customFieldRow = customFields.FirstOrDefault(s => s.EntityID == primaryKey(record));

                    if (customFieldRow != null)
                    {
                        PropertyInfo[] properties = customFieldRow.GetType().GetProperties();
                        foreach (CustomFieldMetaModel meta in customFieldMeta)
                        {
                            PropertyInfo customProperty = properties.FirstOrDefault(s => s.Name == meta.ColumnName);

                            if (customProperty is not null)
                            {
                                object value = customProperty.GetValue(customFieldRow, null);
                                ReqCustomFieldModel reqCustomFieldModel = GetReqCustomFieldModel(meta, value);
                                record.CustomFields.Add(reqCustomFieldModel);
                            }
                        }
                    }
                    else
                    {
                        foreach (ReqCustomFieldModel reqCustomFieldModel in customFieldMeta.Select(meta => GetReqCustomFieldModel(meta, null)))
                        {
                            record.CustomFields.Add(reqCustomFieldModel);
                        }
                    }
                }
            }

            EntityWithCustomFieldList<T> returnValue = new()
            {
                List = extractedList,
                CustomFieldMeta = customFieldMeta,
                PaginationMeta = recordsWithPaginationApplied.PaginationMeta
            };

            return returnValue;
        }

        /// <inheritdoc />
        public async Task AddWithCustomFieldAsync(T entity, Func<T, long> entityID, EnumEntityType entityType)
        {
            entity.CustomField = MapCustomFieldFromModel(entity.CustomFields);

            await AddAsync(entity, entityID: entityID);

            if (entity.CustomField != null)
            {
                entity.CustomField.ParamEntityTypeID = (int)entityType;
                await _customFieldRepository.SaveAsync(entity.CustomField, entityID(entity), entityType);
            }
        }

        /// <inheritdoc />
        public async Task UpdateWithCustomFieldAsync(T entity, long entityID, EnumEntityType entityType)
        {
            entity.CustomField = MapCustomFieldFromModel(entity.CustomFields);

            Update(entity, entityID: entityID);

            if (entity.CustomField != null)
            {
                entity.CustomField.ParamEntityTypeID = (int)entityType;
                await _customFieldRepository.SaveAsync(entity.CustomField, entityID, entityType);
            }
        }

        /// <inheritdoc />
        public async Task DeleteWithCustomFieldAsync(T entity, long entityID, EnumEntityType entityType)
        {
            await _customFieldRepository.DeleteAsync(entityID, entityType);

            Delete(entity, entityID: entityID);
        }

        #region Private Methods

        /// <summary>
        /// Maps the Custom Field List to Custom Field Model, helps setting custom field columns.
        /// </summary>
        /// <param name="reqCustomFields"></param> RequestCustomFields List
        /// <returns></returns>
        private static CustomFieldModel MapCustomFieldFromModel(List<ReqCustomFieldModel> reqCustomFields)
        {
            PropertyInfo udfProperty;
            CustomFieldModel udfModel = new();

            if (reqCustomFields != null)
            {
                PropertyInfo[] properties = udfModel.GetType().GetProperties();
                for (int i = 0; i < reqCustomFields.Count; i++)
                {
                    var item = reqCustomFields[i];
                    if (item != null && !string.IsNullOrEmpty(item.ColumnName))
                    {
                        udfProperty = properties.FirstOrDefault(s => s.Name == reqCustomFields[i].ColumnName);

                        if (udfProperty is not null)
                        {
                            if (item.ColumnName.ToLower().Contains("stringfield"))
                            {
                                udfProperty.SetValue(udfModel, item.StringField, null);
                            }
                            else if (item.ColumnName.ToLower().Contains("numberfield"))
                            {
                                udfProperty.SetValue(udfModel, item.NumberField, null);
                            }
                            else if (item.ColumnName.ToLower().Contains("amountfield"))
                            {
                                udfProperty.SetValue(udfModel, item.AmountField, null);
                            }
                            else if (item.ColumnName.ToLower().Contains("datefield"))
                            {
                                udfProperty.SetValue(udfModel, item.DateField, null);
                            }
                            else if (item.ColumnName.ToLower().Contains("boolfield"))
                            {
                                udfProperty.SetValue(udfModel, item.BoolField, null);
                            }
                            else if (item.ColumnName.ToLower().Contains("listfield"))
                            {
                                udfProperty.SetValue(udfModel, item.ListField, null);
                            }
                        }
                    }
                }
            }

            return udfModel;
        }

        private static ReqCustomFieldModel GetReqCustomFieldModel(CustomFieldMetaModel meta, object value)
        {
            ReqCustomFieldModel reqCustomFieldModel = new()
            {
                ColumnName = meta.ColumnName
            };

            if (meta.ColumnName.ToLower().Contains("stringfield"))
            {
                reqCustomFieldModel.StringField = (string)value;
            }
            else if (meta.ColumnName.ToLower().Contains("numberfield"))
            {
                reqCustomFieldModel.NumberField = (decimal?)value;
            }
            else if (meta.ColumnName.ToLower().Contains("amountfield"))
            {
                reqCustomFieldModel.AmountField = (decimal?)value;
            }
            else if (meta.ColumnName.ToLower().Contains("datefield"))
            {
                reqCustomFieldModel.DateField = (DateTime?)value;
            }
            else if (meta.ColumnName.ToLower().Contains("boolfield"))
            {
                reqCustomFieldModel.BoolField = (bool?)value;
            }
            else if (meta.ColumnName.ToLower().Contains("listfield"))
            {
                reqCustomFieldModel.ListField = (string)value;
            }

            return reqCustomFieldModel;
        }

        #endregion
    }
}