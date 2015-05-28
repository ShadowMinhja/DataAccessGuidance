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
    using System.Data.Entity;

    public interface IStateProvinceContext
    {
        IDbSet<StateProvince> StateProvinces { get; set; }
    }
}