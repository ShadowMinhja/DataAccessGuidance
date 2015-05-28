//===============================================================================
// Microsoft patterns & practices
//  Data Access Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://dataguidance.codeplex.com/license)
//===============================================================================


namespace DataAccess.Repo.Impl.Neo4j.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using DataAccess.Domain;
    using DataAccess.Domain.Catalog;
    using DataAccess.Repo.Impl.Neo4j.Resources;
    using DataAccess.Repository;
    using Neo4jClient;
    using Neo4jClient.Cypher;

    public class ProductRecommendationRepository : IProductRecommendationRepository
    {
        private static object syncRoot = new object();
        private static GraphClient client;

        public ProductRecommendationRepository(Uri databaseUri)
        {
            if (databaseUri == null)
            {
                throw new ArgumentNullException("databaseUri");
            }

            try
            {
                ProductRecommendationRepository.EnsureGraphClient(databaseUri);
            }
            catch (Exception e)
            {
                throw new RepositoryException(Strings.ErrorCreatingGraphClient, e);
            }
        }

        public ICollection<RecommendedProduct> GetProductRecommendations(int productId)
        {
            try
            {
                var recommendedProducts = new List<RecommendedProduct>();

                var graphResults = ProductRecommendationRepository.client
                    .Cypher
                    .Start(new { product = Node.ByIndexLookup("product_id_index", "productId", productId) })
                    .Match("product-[r:PRODUCT_RECOMMENDATION]->recommended")
                    .Return<ProductGraphEntity>("recommended").Results;

                foreach (var graphProduct in graphResults)
                {
                    recommendedProducts.Add(new RecommendedProduct()
                    {
                        Name = graphProduct.Name,
                        Percentage = graphProduct.Percentage,
                        ProductId = graphProduct.ProductId
                    });
                }

                return recommendedProducts;
            }
            catch (Exception e)
            {               
                throw new RepositoryException(
                    string.Format(CultureInfo.CurrentCulture, Strings.ErrorRetrievingRecommendationsForProductId, productId),
                    e);
            }
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The GraphClient needs to be a singleton to avoid a CLR pinned memory issue.")]
        private static void EnsureGraphClient(Uri databaseUri)
        {
            if (ProductRecommendationRepository.client == null)
            {
                lock (ProductRecommendationRepository.syncRoot)
                {
                    if (ProductRecommendationRepository.client == null)
                    {
                        ProductRecommendationRepository.client = new GraphClient(
                            databaseUri,
                            new HttpClientWrapper(
                                new HttpClient()
                                {
                                    Timeout = System.TimeSpan.FromMilliseconds(15000.0d)
                                }));
                        client.Connect();
                    }
                }
            }
        }
    }
}