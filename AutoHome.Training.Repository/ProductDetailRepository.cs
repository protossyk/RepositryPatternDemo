using AutoHome.Training.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoHome.Training.Repository
{
    public class ProductDetailRepository : TrainingRepositoryBase<ProductDetailInfo, int>
    {
        public ProductDetailRepository(
            TraingRepositoryContext traingRepositoryContextBase
            ) : base(traingRepositoryContextBase)
        {
        }
    }
}
