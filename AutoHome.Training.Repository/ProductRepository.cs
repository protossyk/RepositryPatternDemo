using AutoHome.Training.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Repository
{
    public class ProductRepository: TrainingRepositoryBase<ProductInfo,int>
    {
        public ProductRepository(
            TraingRepositoryContext traingRepositoryContextBase
            ) : base(traingRepositoryContextBase)
        {
        }

    }
}
