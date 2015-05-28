//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Sql
{
    using System.Transactions;

    public abstract class BaseRepository
    {
        private static TransactionOptions transactionOptions = new TransactionOptions()
        {
            // by default, EF use IsolationLevel.Serializable even for reads and transaction is always used. 
            // To solve the deadlock in read operation, we need to change the isolation level to ReadCommited and explicitly
            // perform transaction for all the database access
            IsolationLevel = IsolationLevel.ReadCommitted
        };

        static BaseRepository()
        {
            AutoMapperConfig.SetAutoMapperConfiguration();
        }

        protected virtual TransactionScope GetTransactionScope()
        {
            return new TransactionScope(TransactionScopeOption.Required, transactionOptions);
        }
    }
}
