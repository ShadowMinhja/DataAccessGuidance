//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Sql.StateProvince
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using DataAccess.Domain;
    using DataAccess.Repo.Impl.Sql.Resources;
    using DataAccess.Repository;
    using DE = DataAccess.Domain.Person;

    public class StateProvinceRepository : BaseRepository, IStateProvinceRepository
    {
        public DE.StateProvince GetStateProvince(int stateProvinceId)
        {
            try
            {
                using (var context = new StateProvinceContext())
                {
                    StateProvince stateProvince = null;
                    using (var transactionScope = this.GetTransactionScope())
                    {
                        stateProvince = context.StateProvinces.SingleOrDefault(s => s.StateProvinceId == stateProvinceId);

                        transactionScope.Complete();
                    }

                    if (stateProvince == null)
                    {
                        return null;
                    }

                    var result = new DE.StateProvince();

                    Mapper.Map(stateProvince, result);

                    return result;
                }
            }
            catch (System.Exception e)
            {
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingStateProvinceById, stateProvinceId),
                    e);
            }
        }

        public ICollection<DE.StateProvince> GetStateProvinces()
        {
            try
            {
                using (var context = new StateProvinceContext())
                {
                    var result = new List<DE.StateProvince>();
                    using (var transactionScope = this.GetTransactionScope())
                    {
                        Mapper.Map(context.StateProvinces, result);
                        transactionScope.Complete();
                    }

                    return result;
                }
            }
            catch (System.Exception e)
            {
                throw new RepositoryException(Strings.ErrorRetrievingAllStateProvinces, e);
            }
        }
    }
}